import os
import json
import pandas as pd
from datasets import Dataset
from src.base.llm_model import get_ollama_llm
from src.rag.vectorstore import VectorDB
from src.rag.file_loader import Loader

# Import 2 hệ thống của bạn
from src.rag.offline_rag import Offline_RAG as Naive_RAG
from src.rag.crag import Offline_RAG as CRAG

# Import Ragas (ĐÃ MỞ KHÓA ĐỦ 4 TIÊU CHÍ)
from ragas import evaluate
from ragas.metrics import faithfulness, answer_relevancy, context_precision, context_recall
from ragas.run_config import RunConfig

# Import cấu hình giám khảo DEEPSEEK (Dùng qua giao thức OpenAI)
from langchain_openai import ChatOpenAI
from ragas.llms import LangchainLLMWrapper
from ragas.embeddings import LangchainEmbeddingsWrapper
from langchain_huggingface import HuggingFaceEmbeddings


# ========================================================
# BẮT BUỘC TRÊN WINDOWS: Đưa toàn bộ code chạy vào hàm này
# ========================================================
if __name__ == "__main__":
    
    # ==========================================
    # 1. CHUẨN BỊ MÔ HÌNH VÀ TÀI LIỆU
    # ==========================================
    print("Khởi tạo model và VectorDB...")
    llm = get_ollama_llm(model_name="qwen2.5:1.5b")
    doc_loaded = Loader().load_dir("./data_source/generative_ai", workers=2)
    retriever = VectorDB(documents=doc_loaded).get_retriever(search_kwargs={"k": 3})

    # Khởi tạo cả 2 hệ thống
    naive_chain = Naive_RAG(llm).get_chain(retriever)
    crag_chain = CRAG(llm).get_chain(retriever)

    # ==========================================
    # 2. ĐỌC ĐỀ THI (BỘ 50 CÂU ĐÃ THIẾT KẾ)
    # ==========================================
    print("\n--- ĐANG ĐỌC DỮ LIỆU TỪ TEST100.JSON ---")
    with open(r"D:\nckh_prj\rag_kaka\test100.json", "r", encoding="utf-8") as f:
        test_data = json.load(f)
    
    # Chạy luôn bộ 50 câu xịn xò
    test_questions = test_data["question"]
    ground_truths = test_data["ground_truth"]
    
    print(f"✅ Đã nạp thành công {len(test_questions)} câu hỏi.")

    # ==========================================
    # 3. CHO 2 HỆ THỐNG LÀM BÀI
    # ==========================================
    naive_answers = []
    naive_contexts = []

    crag_answers = []
    crag_contexts = []

    print("\n--- ĐANG CHO HỆ THỐNG LÀM BÀI TẬP ---")
    for q in test_questions:
        print(f"\nĐang xử lý câu hỏi: {q}")
        
        # Hệ thống cũ làm bài
        naive_ans = naive_chain.invoke({"question": q})
        naive_answers.append(naive_ans)
        naive_docs = retriever.invoke(q)
        naive_contexts.append([doc.page_content for doc in naive_docs])
        
        # Hệ thống mới làm bài
        crag_ans = crag_chain.invoke({"question": q})
        crag_answers.append(crag_ans)
        crag_docs = retriever.invoke(q)
        crag_contexts.append([doc.page_content for doc in crag_docs])

    # ==========================================
    # 4. CHẤM ĐIỂM BẰNG RAGAS VỚI GIÁM KHẢO DEEPSEEK
    # ==========================================
    naive_data = Dataset.from_dict({
        "question": test_questions,
        "answer": naive_answers,
        "contexts": naive_contexts,
        "ground_truth": ground_truths
    })

    crag_data = Dataset.from_dict({
        "question": test_questions,
        "answer": crag_answers,
        "contexts": crag_contexts,
        "ground_truth": ground_truths
    })

    print("\n--- ĐANG GỌI GIÁM KHẢO DEEPSEEK CHẤM ĐIỂM ---")

    # BẮT BUỘC SỬA: Điền mã API DeepSeek của bạn vào đây
    os.environ["DEEPSEEK_API_KEY"] = "sk-5d876fb82c294e17ba13289bdf4481c1"

    # Sử dụng mô hình DeepSeek-V3 (deepseek-chat)
    evaluator_llm = ChatOpenAI(
        model="deepseek-chat", 
        api_key=os.environ["DEEPSEEK_API_KEY"], 
        base_url="https://api.deepseek.com",
        temperature=0,
        max_retries=3
    )
    ragas_llm = LangchainLLMWrapper(evaluator_llm)

    evaluator_embeddings = HuggingFaceEmbeddings(model_name="keepitreal/vietnamese-sbert")
    ragas_embeddings = LangchainEmbeddingsWrapper(evaluator_embeddings)

    safe_config = RunConfig(max_workers=1, max_retries=3)
    
    # BẬT ĐỦ 4 TIÊU CHÍ (Metrics)
    all_metrics = [faithfulness, context_precision]

    print("Đang chấm điểm Naive RAG...")
    naive_results = evaluate(
        dataset=naive_data, 
        metrics=all_metrics, 
        llm=ragas_llm,
        embeddings=ragas_embeddings,
        run_config=safe_config,
        raise_exceptions=True 
    )

    print("Đang chấm điểm CRAG...")
    crag_results = evaluate(
        dataset=crag_data, 
        metrics=all_metrics, 
        llm=ragas_llm,
        embeddings=ragas_embeddings,
        run_config=safe_config,
        raise_exceptions=True
    )

    print("\n🎯 ĐIỂM CỦA NAIVE RAG:")
    print(naive_results)

    print("\n🎯 ĐIỂM CỦA CRAG:")
    print(crag_results)