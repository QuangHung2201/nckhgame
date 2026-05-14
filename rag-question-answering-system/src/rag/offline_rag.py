import re
from operator import itemgetter
from langchain_core.runnables import RunnablePassthrough, RunnableLambda
from langchain_core.output_parsers import StrOutputParser
from langchain_core.prompts import ChatPromptTemplate

# 1. TÁCH MÁY CHÉM THÀNH MỘT HÀM ĐỘC LẬP
def may_chem_van_phong(text: str) -> str:
    # 1. Dọn dẹp thẻ HTML thừa
    clean_text = text.replace("<|im_end|>", "").replace("<|im_start|>", "").strip()
    
    # 2. Xóa sạch mọi ký tự Tiếng Trung/Nhật/Hàn
    clean_text = re.sub(r'[\u4e00-\u9fff]+', '', clean_text).strip()
    
    # 3. QUÉT SẠCH LANH CHANH (Kể cả khi web bị stream)
    pattern = r"^(Đúng rồi.*?|Bạn nói đúng.*?|Đúng vậy.*?|Chính xác.*?|Câu hỏi.*?|Dạ đúng.*?)(?:,|\.|!|\n|\s)+"
    while re.match(pattern, clean_text, re.IGNORECASE):
        clean_text = re.sub(pattern, "", clean_text, count=1, flags=re.IGNORECASE).strip()
    
    # 4. Gọt nốt cái đuôi ngáo ngơ
    duoi_ngao_ngo = "không cần trả lời thêm bất kỳ thông tin nào khác"
    if duoi_ngao_ngo in clean_text.lower():
        clean_text = re.sub(r'(?i)[.,]?\s*' + duoi_ngao_ngo + r'.*$', '.', clean_text).strip()

    if len(clean_text) > 0:
        clean_text = clean_text[0].upper() + clean_text[1:]
        
    return clean_text

class Offline_RAG:
    def __init__(self, llm) -> None:
        self.llm = llm
        
        system_prompt = (
            "Bạn là một trợ lý AI cung cấp thông tin. BẠN BỊ CẤM SỬ DỤNG KIẾN THỨC BÊN NGOÀI.\n"
            "Chỉ được phép trích xuất thông tin CÓ SẴN trong phần <ngu_canh>.\n"
            "TUYỆT ĐỐI KHÔNG tự ý suy diễn, không tự bịa thêm mốc thời gian, không thêm thắt bình luận cá nhân."
        )
        
        human_template = (
            "Tài liệu duy nhất bạn được phép dùng:\n"
            "<ngu_canh>\n{context}\n</ngu_canh>\n\n"
            "Câu hỏi của khách: {question}\n\n"
            "QUY TẮC BẮT BUỘC:\n"
            "1. Nếu <ngu_canh> có thông tin, hãy tóm tắt CHÍNH XÁC thông tin đó, không thêm bớt.\n"
            "2. Nếu <ngu_canh> KHÔNG chứa thông tin để trả lời, BẮT BUỘC trả lời đúng nguyên văn: "
            "'Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác'."
        )
        
        self.prompt = ChatPromptTemplate.from_messages([
            ("system", system_prompt),
            ("human", human_template)
        ])

    def get_chain(self, retriever):
        input_data = {
            "context": itemgetter("question") | retriever | self.format_docs, 
            "question": itemgetter("question")
        }
        
        rag_chain = (
            input_data
            | self.prompt
            | self.llm
            | StrOutputParser() # Bước 1: Cho AI trả lời tự nhiên (dù stream hay invoke)
            | RunnableLambda(may_chem_van_phong) # Bước 2: Bắt buộc đi qua máy chém này trước khi lên Web
        )
        return rag_chain

    def format_docs(self, docs):
        print(f"\n[DEBUG] Số lượng đoạn văn tìm thấy: {len(docs)}")
        if len(docs) > 0:
            print("[DEBUG] Nội dung đoạn đầu tiên:")
            print("-" * 50)
            print(docs[0].page_content[:300])
            print("-" * 50)
        else:
            print("[DEBUG] ⚠️ KHÔNG TÌM THẤY TÀI LIỆU NÀO KHỚP CÂU HỎI!")
        
        formatted_texts = []
        for doc in docs:
            dia_diem = doc.metadata.get("dia_diem", "Không rõ")
            chu_de = doc.metadata.get("chu_de", "Thông tin chung")
            doan_van_hoan_chinh = f"[Địa điểm: {dia_diem} | Chủ đề: {chu_de}]\n{doc.page_content}"
            formatted_texts.append(doan_van_hoan_chinh)
            
        return "\n\n".join(formatted_texts)