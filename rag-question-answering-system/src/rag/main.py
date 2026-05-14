import re
from pydantic import BaseModel, Field
from langchain_core.runnables import RunnableLambda
from src.rag.file_loader import Loader
from src.rag.vectorstore import VectorDB

# 1. IMPORT CRAG THAY VÌ NAIVE RAG
from src.rag.crag import Offline_RAG as CRAG 

class InputQA(BaseModel):
    question: str = Field(..., title="Question to ask the model")

class OutputQA(BaseModel):
    answer: str = Field(..., title="Answer from the model")

# ==========================================
# 2. BỘ LỌC TỪ ĐỒNG NGHĨA (QUERY REWRITING)
# ==========================================
def normalize_query(inputs: dict) -> dict:
    question = inputs.get("question", "")
    
    # Từ điển đồng nghĩa (Bạn có thể thêm bớt tùy ý cho dự án)
    synonyms = {
        r"(?i)\bhồ gươm\b": "Hồ Hoàn Kiếm",
        r"(?i)\blăng bác\b": "Lăng Chủ tịch Hồ Chí Minh",
        r"(?i)\blăng hồ chủ tịch\b": "Lăng Chủ tịch Hồ Chí Minh",
        r"(?i)\bvăn miếu\b": "Văn Miếu Quốc Tử Giám",
        r"(?i)\bhoả lò\b": "Di tích lịch sử Nhà tù Hỏa Lò",
        r"(?i)\bnhà tù hoả lò\b": "Di tích lịch sử Nhà tù Hỏa Lò",
        r"(?i)\bcột cờ\b": "Cột cờ Hà Nội",
        r"(?i)\bhoàng thành\b": "Hoàng thành Thăng Long"
    }
    
    # Duyệt qua từ điển và thay thế
    for pattern, replacement in synonyms.items():
        question = re.sub(pattern, replacement, question)
    
    # Cập nhật lại câu hỏi đã được "dịch" ra tên chuẩn
    inputs["question"] = question
    print(f"\n[TIỀN XỬ LÝ] Câu hỏi sau chuẩn hoá: '{question}'")
    
    return inputs

# ==========================================
# 3. BUILD CHUỖI CRAG MỚI
# ==========================================
def build_rag_chain(llm, data_dir):
    
    # 1. Gọi Loader 
    doc_loaded = Loader().load_dir(data_dir, workers=2)
    
    # 2. Tạo VectorDB và Retriever (k=3)
    retriever = VectorDB(documents=doc_loaded).get_retriever(search_kwargs={"k": 3})
    
    # 3. Tạo CRAG Chain thay vì Offline_RAG
    crag_chain = CRAG(llm).get_chain(retriever)

    # 4. GHÉP NỐI: Tiền xử lý câu hỏi -> Đưa vào CRAG
    final_chain = RunnableLambda(normalize_query) | crag_chain

    return final_chain