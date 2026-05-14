# [File: src/base/llm_model.py]
from langchain_ollama import ChatOllama
from langchain_huggingface import HuggingFaceEndpoint

def get_ollama_llm(model_name: str = "qwen-hanoi", base_url: str = "http://host.docker.internal:11434", **kwargs):
    print(f"🔌 Đang kết nối tới Ollama tại {base_url} với model: {model_name}...")
    try:
        llm = ChatOllama(
            base_url=base_url,
            model=model_name,
            temperature=0.01, # <--- Hạ nhiệt độ xuống mức thấp nhất để câu văn bớt bay bổng linh tinh
            num_ctx=4096,
            repeat_penalty=1.05, # <--- VŨ KHÍ BÍ MẬT: Phạt nặng hành vi lặp từ ("không gian không gian")
            stop=["<|im_end|>", "<|im_start|>", "Question:", "User:", "Câu hỏi:"], 
            **kwargs
        )
        return llm
    except Exception as e:
        print(f"❌ Lỗi khi khởi tạo Ollama: {e}")
        return None

def get_hf_llm(repo_id: str, huggingfacehub_api_token: str, **kwargs):
    print(f"🔌 Đang kết nối tới Hugging Face Hub với model: {repo_id}...")
    
    # Dùng pop() để lấy giá trị ra khỏi kwargs (xóa luôn key đó trong kwargs để tránh trùng lặp)
    temperature = kwargs.pop("temperature", 0.7)
    
    # Xử lý tương tự cho max_new_token (HuggingFaceEndpoint dùng tên tham số là max_new_tokens)
    max_new_tokens = kwargs.pop("max_new_token", 512)
    if "max_new_tokens" in kwargs:
        max_new_tokens = kwargs.pop("max_new_tokens")

    try:
        llm = HuggingFaceEndpoint(
            repo_id=repo_id,
            huggingfacehub_api_token=huggingfacehub_api_token,
            temperature=temperature,
            max_new_tokens=max_new_tokens,
            **kwargs # Lúc này kwargs đã sạch, không còn chứa temperature nữa
        )
        return llm
    except Exception as e:
        print(f"❌ Lỗi khi khởi tạo Hugging Face LLM: {e}")
        return None

# --- Phần này để test nhanh khi chạy trực tiếp file này ---
if __name__ == "__main__":
    # Test thử kết nối
    print("--- ĐANG TEST KẾT NỐI OLLAMA ---")
    try:
        # Gọi hàm khởi tạo (Test thẳng model hanoi_tour_guide của bạn)
        my_llm = get_ollama_llm(model_name="qwen-hanoi")
        
        # Thử hỏi một câu
        question = "Bạn có biết Hồ Gươm ở đâu không?"
        print(f"❓ Câu hỏi: {question}")
        
        response = my_llm.invoke(question)
        print("\n🤖 Trả lời:")
        # ChatOllama trả về đối tượng (AIMessage), nên thêm .content để in ra chữ
        print(response.content) 
        print("\n✅ THÀNH CÔNG! Ollama đang hoạt động tốt.")
    except Exception as e:
        print("\n❌ THẤT BẠI! Hãy kiểm tra xem bạn đã bật App Ollama chưa?")
        print(f"Lỗi chi tiết: {e}")