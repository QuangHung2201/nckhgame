import os
os.environ["TOKENIZERS_PARALLELISM"] = "false"

from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware

from langserve import add_routes
from src.base.llm_model import get_ollama_llm
from src.rag.main import build_rag_chain, InputQA, OutputQA
from src.chat.main import build_chat_chain
from src.rag.file_loader import Loader
from src.rag.vectorstore import VectorDB
# ==========================================
# GỌI MODEL QUA OLLAMA (Chạy Local 100%)
# ==========================================
# Không cần dùng HF_TOKEN hay get_hf_llm nữa, 
# Gọi thẳng model "hanoi_tour_guide" mà bạn vừa nạp vào Ollama


genai_docs = "./data_source/generative_ai"
ollama_base_url = os.getenv("OLLAMA_BASE_URL", "http://host.docker.internal:11434")
llm = get_ollama_llm(model_name="qwen2.5:1.5b", base_url=ollama_base_url)
doc_loaded = Loader().load_dir(genai_docs, workers=2)
retriever = VectorDB(documents=doc_loaded).get_retriever(search_kwargs={"k": 2}) # Gợi ý để k=3 để tránh nhiễu

# --------- Chains----------------
genai_chain = build_rag_chain(llm, data_dir=genai_docs)

# Truyền thêm retriever vào chat_chain
chat_chain = build_chat_chain(llm, 
                              retriever=retriever, # Đưa bộ tìm kiếm tài liệu vào đây
                              history_folder="./chat_histories",
                              max_history_length=6)

app = FastAPI(
    title="LangChain Server",
    version="1.0",
    description="A simple api server using Langchain's Runnable interfaces",
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
    expose_headers=["*"],
)


@app.get("/check")
async def check():
    return {"status": "ok"}


@app.post("/generative_ai", response_model=OutputQA)
async def generative_ai(inputs: InputQA):
    # API tự viết của bạn: Bọc inputs.question vào dictionary
    answer = genai_chain.invoke({"question": inputs.question})
    return {"answer": answer}


# Đổi path thành /genai_langserve để tránh xung đột với @app.post("/generative_ai")
add_routes(app, 
           genai_chain, 
           path="/genai_langserve",
           input_type=InputQA,   
           output_type=OutputQA)

add_routes(app,
           chat_chain,
           path="/chat")