# import re
# from typing import List, TypedDict
# from langchain_core.runnables import RunnableLambda
# from langchain_core.prompts import PromptTemplate
# from langgraph.graph import END, StateGraph

# # 1. MÁY CHÉM VĂN PHONG (Giữ nguyên của bạn để dọn dẹp rác từ LLM nhỏ)
# def may_chem_van_phong(text: str) -> str:
#     # 1. Dọn dẹp thẻ HTML và ký tự đặc biệt của mô hình
#     clean_text = text.replace("<|im_end|>", "").replace("<|im_start|>", "").strip()
    
#     # 2. XỬ LÝ LỖI HIỆN \n: Biến chuỗi "\\n" (2 ký tự) thành dấu xuống dòng thật (\n)
#     # Điều này giải quyết việc thỉnh thoảng Bot trả về text chứa chữ \n thay vì xuống dòng
#     clean_text = clean_text.replace('\\n', '\n')
    
#     # 3. Xóa sạch mọi ký tự Tiếng Trung/Nhật/Hàn (giữ nguyên logic của bạn)
#     clean_text = re.sub(r'[\u4e00-\u9fff]+', '', clean_text).strip()
    
#     # 4. QUÉT SẠCH LANH CHANH (Kể cả khi web bị stream)
#     pattern = r"^(Đúng rồi.*?|Bạn nói đúng.*?|Đúng vậy.*?|Chính xác.*?|Câu hỏi.*?|Dạ đúng.*?)(?:,|\.|!|\n|\s)+"
#     while re.match(pattern, clean_text, re.IGNORECASE):
#         clean_text = re.sub(pattern, "", clean_text, count=1, flags=re.IGNORECASE).strip()
    
#     # 5. Gọt nốt cái đuôi ngáo ngơ (giữ nguyên logic của bạn)
#     duoi_ngao_ngo = "không cần trả lời thêm bất kỳ thông tin nào khác"
#     if duoi_ngao_ngo in clean_text.lower():
#         clean_text = re.sub(r'(?i)[.,]?\s*' + duoi_ngao_ngo + r'.*$', '.', clean_text).strip()

#     # 6. DỌN DẸP XUỐNG DÒNG THỪA: 
#     # Thay thế 3 dấu xuống dòng trở lên bằng 2 dấu xuống dòng để văn bản gọn gàng
#     clean_text = re.sub(r'\n{3,}', '\n\n', clean_text).strip()

#     # 7. Viết hoa chữ cái đầu tiên
#     if len(clean_text) > 0:
#         clean_text = clean_text[0].upper() + clean_text[1:]
        
#     return clean_text

# # 2. KHAI BÁO TRẠNG THÁI (STATE) CỦA ĐỒ THỊ CRAG
# class GraphState(TypedDict):
#     question: str
#     documents: List[any]
#     generation: str
#     fallback: bool


# # 3. CLASS HỆ THỐNG CRAG
# class Offline_RAG:
#     def __init__(self, llm) -> None:
#         self.llm = llm

#     def get_chain(self, retriever):
        
#         # ==========================================
#         # KHAI BÁO CÁC TRẠM XỬ LÝ (NODES) TRONG CRAG
#         # ==========================================
        
#         def retrieve_node(state: GraphState):
#             """Trạm 1: Tìm kiếm tài liệu từ VectorDB"""
#             question = state["question"]
#             print(f"\n[CRAG] 🔎 TRẠM 1: Tìm kiếm tài liệu cho câu hỏi: '{question}'")
#             docs = retriever.invoke(question)
#             return {"documents": docs, "question": question}

#         def grade_documents_node(state: GraphState):
#             """Trạm 2: LLM làm Giám khảo đánh giá tài liệu (Corrective)"""
#             print("[CRAG] ⚖️ TRẠM 2: Đánh giá độ chính xác của tài liệu...")
#             question = state["question"]
#             documents = state["documents"]
            
#             # SỬA PROMPT: Rõ ràng hơn, cho phép "yes" nếu tài liệu có liên quan dù chỉ một phần
#             prompt = PromptTemplate(
#                 template="""Bạn là một giám khảo khách quan và cẩn thận. Nhiệm vụ của bạn là đánh giá xem TÀI LIỆU có liên quan hoặc hữu ích để trả lời CÂU HỎI hay không.
                
# TÀI LIỆU: {document}
# CÂU HỎI: {question}

# Hãy suy nghĩ kỹ. Nếu tài liệu chứa bất kỳ từ khóa, ngữ cảnh hoặc thông tin nào có thể giúp trả lời dù chỉ một phần câu hỏi, hãy chấm là 'yes'. Nếu hoàn toàn lạc đề, chấm là 'no'.
# Chỉ xuất ra MỘT TỪ DUY NHẤT (yes hoặc no), không giải thích gì thêm:""",
#                 input_variables=["document", "question"],
#             )
            
#             grader_chain = prompt | self.llm
#             filtered_docs = []

#             for doc in documents:
#                 # Ép kiểu và bắt lỗi tốt hơn cho chuỗi trả về
#                 score_response = grader_chain.invoke({"question": question, "document": doc.page_content}).content
#                 score = score_response.strip().lower()
                
#                 # Nới lỏng kiểm tra: Chỉ cần LLM có thốt ra chữ "yes" trong output
#                 if "yes" in score:
#                     print("   -> 🟢 Tài liệu hợp lệ: Giữ lại")
#                     filtered_docs.append(doc)
#                 else:
#                     print(f"   -> 🔴 Tài liệu rác (Điểm: {score}): Vứt bỏ")

#             fallback = len(filtered_docs) == 0
#             return {"documents": filtered_docs, "fallback": fallback}

#         def generate_node(state: GraphState):
#             """Trạm 3A: Tạo câu trả lời nếu tài liệu tốt"""
#             print("[CRAG] 📝 TRẠM 3A: Tổng hợp câu trả lời...")
#             question = state["question"]
#             documents = state["documents"]
            
#             context = "\n\n".join([f"[Nguồn: {d.metadata.get('dia_diem', 'Chung')}]\n{d.page_content}" for d in documents])
            
#             # SỬA PROMPT: Bê nguyên cấu trúc chặt chẽ của Offline_RAG sang
#             prompt = PromptTemplate(
#                 template="""Bạn là một trợ lý AI cung cấp thông tin. BẠN BỊ CẤM SỬ DỤNG KIẾN THỨC BÊN NGOÀI.
# Tài liệu duy nhất bạn được phép dùng:
# <ngu_canh>
# {context}
# </ngu_canh>

# Câu hỏi của khách: {question}

# QUY TẮC BẮT BUỘC:
# 1. Nếu <ngu_canh> có thông tin, hãy tóm tắt CHÍNH XÁC thông tin đó. TUYỆT ĐỐI KHÔNG tự ý suy diễn, không tự bịa thêm mốc thời gian, không thêm thắt bình luận cá nhân.
# 2. Nếu <ngu_canh> KHÔNG đủ thông tin để trả lời hoàn chỉnh, BẮT BUỘC trả lời đúng nguyên văn: 'Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác'.
# Trả lời:""",
#                 input_variables=["context", "question"],
#             )
            
#             rag_chain = prompt | self.llm
#             response = rag_chain.invoke({"context": context, "question": question}).content
#             return {"generation": response}

#         def refuse_node(state: GraphState):
#             """Trạm 3B: Từ chối trả lời nếu tài liệu sai (Luật Zero-Hallucination)"""
#             print("[CRAG] 🛑 TRẠM 3B: Kích hoạt luật Zero-Hallucination (Từ chối trả lời)")
#             tu_choi = "Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác."
#             return {"generation": tu_choi}

#         # ==========================================
#         # ĐỊNH NGHĨA RẼ NHÁNH (CONDITIONAL EDGES)
#         # ==========================================
#         def decide_to_generate(state: GraphState):
#             if state["fallback"]:
#                 return "refuse"
#             return "generate"

#         # ==========================================
#         # XÂY DỰNG VÀ ĐÓNG GÓI ĐỒ THỊ CRAG
#         # ==========================================
#         workflow = StateGraph(GraphState)
        
#         workflow.add_node("retrieve", retrieve_node)
#         workflow.add_node("grade", grade_documents_node)
#         workflow.add_node("generate", generate_node)
#         workflow.add_node("refuse", refuse_node)
        
#         workflow.set_entry_point("retrieve")
#         workflow.add_edge("retrieve", "grade")
#         workflow.add_conditional_edges("grade", decide_to_generate, {
#             "generate": "generate",
#             "refuse": "refuse"
#         })
#         workflow.add_edge("generate", END)
#         workflow.add_edge("refuse", END)
        
#         crag_app = workflow.compile()

#         # ==========================================
#         # BỌC LẠI THÀNH RUNNABLE ĐỂ KHÔNG PHÁ VỠ APP.PY
#         # ==========================================
#         def run_crag_flow(inputs: dict) -> str:
#             question = inputs.get("question", "")
#             # Chạy đồ thị
#             final_state = crag_app.invoke({"question": question})
#             # Lấy text thô từ đồ thị
#             raw_generation = final_state["generation"]
#             # Cho qua máy chém văn phong trước khi xuất ra
#             final_generation = may_chem_van_phong(raw_generation)
#             return final_generation

#         # Trả về một chuỗi tương thích hoàn toàn với LangServe / FastAPI
#         return RunnableLambda(run_crag_flow)
import re
from typing import List, TypedDict
from langchain_core.runnables import RunnableLambda
from langchain_core.prompts import PromptTemplate
from langgraph.graph import END, StateGraph

# 1. MÁY CHÉM VĂN PHONG (Giữ nguyên để dọn dẹp rác từ LLM nhỏ)
def may_chem_van_phong(text: str) -> str:
    clean_text = text.replace("<|im_end|>", "").replace("<|im_start|>", "").strip()
    clean_text = clean_text.replace('\\n', '\n')
    clean_text = re.sub(r'[\u4e00-\u9fff]+', '', clean_text).strip()
    
    pattern = r"^(Đúng rồi.*?|Bạn nói đúng.*?|Đúng vậy.*?|Chính xác.*?|Câu hỏi.*?|Dạ đúng.*?)(?:,|\.|!|\n|\s)+"
    while re.match(pattern, clean_text, re.IGNORECASE):
        clean_text = re.sub(pattern, "", clean_text, count=1, flags=re.IGNORECASE).strip()
    
    duoi_ngao_ngo = "không cần trả lời thêm bất kỳ thông tin nào khác"
    if duoi_ngao_ngo in clean_text.lower():
        clean_text = re.sub(r'(?i)[.,]?\s*' + duoi_ngao_ngo + r'.*$', '.', clean_text).strip()

    clean_text = re.sub(r'\n{3,}', '\n\n', clean_text).strip()

    if len(clean_text) > 0:
        clean_text = clean_text[0].upper() + clean_text[1:]
        
    return clean_text

# 2. KHAI BÁO TRẠNG THÁI (STATE) CỦA ĐỒ THỊ CRAG
class GraphState(TypedDict):
    question: str
    documents: List[any]
    generation: str
    fallback: bool
    retry_count: int # Thêm biến đếm số lần cứu hộ


# 3. CLASS HỆ THỐNG CRAG
class Offline_RAG:
    def __init__(self, llm) -> None:
        self.llm = llm

    def get_chain(self, retriever):
        
        # ==========================================
        # KHAI BÁO CÁC TRẠM XỬ LÝ (NODES) TRONG CRAG
        # ==========================================
        
        def retrieve_node(state: GraphState):
            """Trạm 1: Tìm kiếm tài liệu từ VectorDB"""
            question = state["question"]
            print(f"\n[CRAG] 🔎 TRẠM 1: Tìm kiếm tài liệu cho câu hỏi: '{question}'")
            docs = retriever.invoke(question)
            
            # Khởi tạo retry_count = 0 nếu là lần chạy đầu
            retry_count = state.get("retry_count", 0) 
            return {"documents": docs, "question": question, "retry_count": retry_count}

        def grade_documents_node(state: GraphState):
            """Trạm 2: Đánh giá và LỌC tài liệu (Dùng Few-Shot cho Model 1.5B)"""
            print("[CRAG] ⚖️ TRẠM 2: Đánh giá độ chính xác của tài liệu...")
            question = state["question"]
            documents = state["documents"]
            retry_count = state.get("retry_count", 0)
            
            # PROMPT ĐƯỢC THIẾT KẾ RIÊNG CHO MODEL 1.5B (CÓ VÍ DỤ MẪU)
            prompt = PromptTemplate(
                template="""Bạn là một trợ lý kiểm tra dữ liệu. Hãy xem tài liệu có chứa thông tin để trả lời câu hỏi hay không.
Chỉ trả lời "yes" hoặc "no". Không giải thích gì thêm.

Ví dụ 1:
TÀI LIỆU: Hồ Gươm nằm ở trung tâm thủ đô Hà Nội.
CÂU HỎI: Hồ Gươm ở đâu?
TRẢ LỜI: yes

Ví dụ 2:
TÀI LIỆU: Phở là món ăn truyền thống của Việt Nam.
CÂU HỎI: Ai là người xây dựng Văn Miếu?
TRẢ LỜI: no

Bây giờ đến lượt bạn:
TÀI LIỆU: {document}
CÂU HỎI: {question}
TRẢ LỜI:""",
                input_variables=["document", "question"],
            )
            
            grader_chain = prompt | self.llm
            filtered_docs = [] # Mảng chứa tài liệu SẠCH

            for doc in documents:
                score_response = grader_chain.invoke({"question": question, "document": doc.page_content}).content
                score = score_response.strip().lower()
                
                if "yes" in score:
                    print("   -> 🟢 Đạt: Có liên quan -> Giữ lại")
                    filtered_docs.append(doc)
                else:
                    print(f"   -> 🔴 Loại bỏ rác (LLM output: {score})")

            # Nếu không còn tài liệu nào sau khi lọc -> Kích hoạt Fallback
            fallback = len(filtered_docs) == 0
            
            if fallback:
                print("   => ⚠️ KHÔNG có tài liệu tốt. Chuẩn bị cứu hộ!")
            else:
                print("   => ✅ Đã lọc xong, chuyển tài liệu sạch lên Trạm 3A!")

            # TRẢ VỀ DANH SÁCH ĐÃ LỌC
            return {"documents": filtered_docs, "fallback": fallback, "retry_count": retry_count}

        def rewrite_node(state: GraphState):
            """Trạm Cứu Hộ: Viết lại câu hỏi (Đơn giản hóa cho model 1.5B)"""
            print("[CRAG] 🔄 TRẠM CỨU HỘ: Viết lại câu hỏi để tìm kiếm lại...")
            question = state["question"]
            retry_count = state.get("retry_count", 0) + 1 # Tăng biến đếm
            
            prompt = PromptTemplate(
                template="""Hãy trích xuất từ khóa quan trọng nhất từ câu hỏi dưới đây để tìm kiếm. Chỉ in ra từ khóa, không in gì thêm.
Câu hỏi gốc: {question}
Từ khóa:""",
                input_variables=["question"],
            )
            
            better_question = (prompt | self.llm).invoke({"question": question}).content.strip()
            print(f"   -> Câu hỏi mới (Từ khóa): {better_question}")
            
            new_docs = retriever.invoke(better_question)
            
            return {"documents": new_docs, "question": better_question, "retry_count": retry_count}

        def generate_node(state: GraphState):
            """Trạm 3A: Tạo câu trả lời nhập vai NPC"""
            print("[CRAG] 📝 TRẠM 3A: Tổng hợp câu trả lời...")
            question = state["question"]
            documents = state["documents"]
            
            context = "\n\n".join([f"[Nguồn: {d.metadata.get('dia_diem', 'Chung')}]\n{d.page_content}" for d in documents])
            
            # PROMPT NHẬP VAI NPC HƯỚNG DẪN VIÊN
            prompt = PromptTemplate(
                template="""Bạn là một NPC hướng dẫn viên du lịch ảo nhiệt tình, am hiểu sâu sắc về văn hóa, lịch sử và địa danh Hà Nội.
Nhiệm vụ của bạn là giải đáp thắc mắc cho du khách một cách tự nhiên dựa trên <ngu_canh> dưới đây.

<ngu_canh>
{context}
</ngu_canh>

<câu_hỏi_của_du_khách>
{question}
</câu_hỏi_của_du_khách>

Hướng dẫn trả lời:
- Hãy trả lời ngắn gọn, lịch sự và chính xác những gì có trong <ngu_canh>.
- TUYỆT ĐỐI KHÔNG tự ý suy diễn hay bịa đặt thông tin không có trong <ngu_canh>.
- Nếu <ngu_canh> không có thông tin, bạn BẮT BUỘC trả lời: 'Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác'.
Trả lời:""",
                input_variables=["context", "question"],
            )
            
            rag_chain = prompt | self.llm
            response = rag_chain.invoke({"context": context, "question": question}).content
            return {"generation": response}

        def refuse_node(state: GraphState):
            """Trạm 3B: Từ chối trả lời (Luật Zero-Hallucination)"""
            print("[CRAG] 🛑 TRẠM 3B: Kích hoạt luật Zero-Hallucination (Từ chối trả lời)")
            tu_choi = "Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác."
            return {"generation": tu_choi}

        # ==========================================
        # ĐỊNH NGHĨA RẼ NHÁNH (CONDITIONAL EDGES)
        # ==========================================
        def decide_to_generate(state: GraphState):
            if state["fallback"]:
                # Nếu đã cứu hộ 1 lần mà VẪN fallback -> ĐẦU HÀNG, KHÔNG LẶP VÔ HẠN
                if state.get("retry_count", 0) >= 1:
                    print("   => ❌ Cứu hộ thất bại, từ chối trả lời!")
                    return "refuse"
                return "rewrite"
            return "generate"

        # ==========================================
        # XÂY DỰNG VÀ ĐÓNG GÓI ĐỒ THỊ CRAG
        # ==========================================
        workflow = StateGraph(GraphState)
        
        workflow.add_node("retrieve", retrieve_node)
        workflow.add_node("grade", grade_documents_node)
        workflow.add_node("rewrite", rewrite_node)
        workflow.add_node("generate", generate_node)
        workflow.add_node("refuse", refuse_node)
        
        workflow.set_entry_point("retrieve")
        workflow.add_edge("retrieve", "grade")
        
        workflow.add_conditional_edges("grade", decide_to_generate, {
            "generate": "generate",
            "rewrite": "rewrite",
            "refuse": "refuse" # Thêm nhánh nối đến Trạm 3B
        })
        
        # SỬA QUAN TRỌNG: Viết lại câu hỏi xong phải quay lại đánh giá tài liệu mới
        workflow.add_edge("rewrite", "grade") 
        workflow.add_edge("generate", END)
        workflow.add_edge("refuse", END)
        
        crag_app = workflow.compile()

        # ==========================================
        # BỌC LẠI THÀNH RUNNABLE
        # ==========================================
        def run_crag_flow(inputs: dict) -> str:
            question = inputs.get("question", "")
            final_state = crag_app.invoke({"question": question})
            raw_generation = final_state["generation"]
            final_generation = may_chem_van_phong(raw_generation)
            return final_generation

        return RunnableLambda(run_crag_flow)