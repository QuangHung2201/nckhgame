import os
import json
import asyncio
from datasets import Dataset
from src.base.llm_model import get_ollama_llm
from src.rag.vectorstore import VectorDB
from src.rag.file_loader import Loader

# Import 2 hệ thống của bạn
from src.rag.offline_rag import Offline_RAG as Naive_RAG
from src.rag.crag import Offline_RAG as CRAG

# Import Ragas (CHỈ LẤY 2 TIÊU CHÍ CÒN LẠI)
from ragas import evaluate
from ragas.metrics import answer_relevancy, context_recall
from ragas.run_config import RunConfig

from langchain_openai import ChatOpenAI
from ragas.llms import LangchainLLMWrapper
from ragas.embeddings import LangchainEmbeddingsWrapper
from langchain_huggingface import HuggingFaceEmbeddings
from langchain_core.outputs import ChatResult

# ======================================================================
# 🚀 HACK: BẢN VÁ LỖI CHO DEEPSEEK (Vượt rào n=1 của API)
# Lớp này sẽ chặn yêu cầu n>1 của Ragas, chia nhỏ ra thành nhiều lần n=1 
# ======================================================================
class SafeDeepSeekChat(ChatOpenAI):
    async def _agenerate(self, messages, stop=None, run_manager=None, **kwargs):
        # Lấy giá trị n mà Ragas yêu cầu, nếu không có mặc định là 1
        n_requested = kwargs.pop('n', 1) 
        # Ép n=1 để đưa vào API DeepSeek (Tránh lỗi 400 Bad Request)
        kwargs['n'] = 1        

        if n_requested > 1:
            generations = []
            # Tự động lặp nhiều lần để giả lập n>1 cho Ragas
            for _ in range(n_requested):
                res = await super()._agenerate(messages, stop=stop, run_manager=run_manager, **kwargs)
                generations.extend(res.generations)
            return ChatResult(generations=generations)
        else:
            return await super()._agenerate(messages, stop=stop, run_manager=run_manager, **kwargs)

    def _generate(self, messages, stop=None, run_manager=None, **kwargs):
        n_requested = kwargs.pop('n', 1)
        kwargs['n'] = 1
        if n_requested > 1:
            generations = []
            for _ in range(n_requested):
                res = super()._generate(messages, stop=stop, run_manager=run_manager, **kwargs)
                generations.extend(res.generations)
            return ChatResult(generations=generations)
        else:
            return super()._generate(messages, stop=stop, run_manager=run_manager, **kwargs)


if __name__ == "__main__":
    
    # 1. KHỞI TẠO HỆ THỐNG (Giữ nguyên)
    print("Khởi tạo model và VectorDB...")
    llm = get_ollama_llm(model_name="qwen2.5:1.5b")
    doc_loaded = Loader().load_dir("./data_source/generative_ai", workers=2)
    retriever = VectorDB(documents=doc_loaded).get_retriever(search_kwargs={"k": 3})

    naive_chain = Naive_RAG(llm).get_chain(retriever)
    crag_chain = CRAG(llm).get_chain(retriever)

    # 2. ĐỌC ĐỀ THI
    print("\n--- ĐANG ĐỌC DỮ LIỆU TỪ TEST100.JSON ---")
    with open(r"D:\nckh_prj\rag_kaka\test100.json", "r", encoding="utf-8") as f:
        test_data = json.load(f)
    
    test_questions = test_data["question"]
    ground_truths = test_data["ground_truth"]

    # 3. HỆ THỐNG LÀM BÀI
    naive_answers = []
    naive_contexts = []
    crag_answers = []
    crag_contexts = []

    print("\n--- ĐANG CHO HỆ THỐNG LÀM BÀI TẬP ---")
    for q in test_questions:
        print(f"Đang xử lý câu hỏi: {q}")
        
        naive_ans = naive_chain.invoke({"question": q})
        naive_answers.append(naive_ans)
        naive_docs = retriever.invoke(q)
        naive_contexts.append([doc.page_content for doc in naive_docs])
        
        crag_ans = crag_chain.invoke({"question": q})
        crag_answers.append(crag_ans)
        crag_docs = retriever.invoke(q)
        crag_contexts.append([doc.page_content for doc in crag_docs])

    # 4. CHẤM ĐIỂM
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

    print("\n--- ĐANG GỌI GIÁM KHẢO DEEPSEEK CHẤM 2 TIÊU CHÍ CÒN LẠI ---")

    # ĐIỀN API KEY CỦA BẠN VÀO ĐÂY
    os.environ["DEEPSEEK_API_KEY"] = "sk-điền_mã_của_bạn"

    # DÙNG CLASS VỪA HACK Ở TRÊN (SafeDeepSeekChat)
    evaluator_llm = SafeDeepSeekChat(
        model="deepseek-chat", 
        api_key=os.environ["DEEPSEEK_API_KEY"], 
        base_url="https://api.deepseek.com",
        temperature=0
    )
    ragas_llm = LangchainLLMWrapper(evaluator_llm)

    evaluator_embeddings = HuggingFaceEmbeddings(model_name="keepitreal/vietnamese-sbert")
    ragas_embeddings = LangchainEmbeddingsWrapper(evaluator_embeddings)

    safe_config = RunConfig(max_workers=1, max_retries=3)

    # CHỈ CHẠY 2 TIÊU CHÍ CHƯA CÓ ĐIỂM
    remaining_metrics = [answer_relevancy, context_recall]

    print("Đang chấm điểm Naive RAG (Phần 2)...")
    naive_results_p2 = evaluate(
        dataset=naive_data, 
        metrics=remaining_metrics,
        llm=ragas_llm,
        embeddings=ragas_embeddings,
        run_config=safe_config,
        raise_exceptions=True 
    )

    print("Đang chấm điểm CRAG (Phần 2)...")
    crag_results_p2 = evaluate(
        dataset=crag_data, 
        metrics=remaining_metrics,
        llm=ragas_llm,
        embeddings=ragas_embeddings,
        run_config=safe_config,
        raise_exceptions=True
    )

    print("\n🎯 ĐIỂM CỦA NAIVE RAG (Phần 2):")
    print(naive_results_p2)

    print("\n🎯 ĐIỂM CỦA CRAG (Phần 2):")
    print(crag_results_p2)