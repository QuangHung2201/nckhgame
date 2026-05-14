# from pydantic import BaseModel, Field
# from operator import itemgetter
# from langchain_core.prompts import ChatPromptTemplate, MessagesPlaceholder
# from langchain_core.runnables.history import RunnableWithMessageHistory
# from src.chat.history import create_session_factory
# from src.chat.output_parser import Str_OutputParser

# # 1. THÊM LUẬT CHỐNG BỊA VÀ NGỮ CẢNH VÀO PROMPT
# chat_prompt = ChatPromptTemplate.from_messages(
#     [
#         ("system", "Bạn là một hướng dẫn viên du lịch ảo tại Hà Nội. BẠN BỊ CẤM SỬ DỤNG KIẾN THỨC BÊN NGOÀI.\n"
#                    "Chỉ được phép trả lời dựa trên nội dung trong phần <ngu_canh> dưới đây.\n\n"
#                    "<ngu_canh>\n{context}\n</ngu_canh>\n\n"
#                    "QUY TẮC BẮT BUỘC: Nếu thông tin không có trong <ngu_canh>, TUYỆT ĐỐI KHÔNG TỰ BỊA ĐẶT mà hãy trả lời nguyên văn: "
#                    "'Tôi không biết, tôi không có thông tin về nó và không trả lời thêm thông tin nào khác'."),
#         MessagesPlaceholder(variable_name="chat_history"),
#         ("human", "{human_input}"),
#     ]
# )

# class InputChat(BaseModel):
#     human_input: str = Field(
#         ...,
#         description="The human input to the chat system.",
#         extra={"widget": {"type": "chat", "input": "human_input"}},
#     )

# def format_docs_for_chat(docs):
#     return "\n\n".join(doc.page_content for doc in docs)

# # 2. THÊM BIẾN `retriever` VÀO HÀM ĐỂ TÌM TÀI LIỆU
# def build_chat_chain(llm, retriever, history_folder, max_history_length):
#     # Setup luồng kết hợp: Tìm tài liệu -> Bỏ vào Prompt -> Đưa cho LLM
#     chain = (
#         {
#             "context": itemgetter("human_input") | retriever | format_docs_for_chat,
#             "human_input": itemgetter("human_input"),
#             "chat_history": itemgetter("chat_history")
#         }
#         | chat_prompt 
#         | llm 
#         | Str_OutputParser()
#     )
    
#     chain_with_history = RunnableWithMessageHistory(
#         chain,
#         create_session_factory(base_dir=history_folder, 
#                                max_history_length=max_history_length),
#         input_messages_key="human_input",
#         history_messages_key="chat_history",
#     )
#     return chain_with_history
from langchain_core.runnables import RunnableLambda
from pydantic import BaseModel, Field
from operator import itemgetter
from langchain_core.prompts import ChatPromptTemplate, MessagesPlaceholder
from langchain_core.runnables.history import RunnableWithMessageHistory
from src.chat.history import create_session_factory
from src.chat.output_parser import Str_OutputParser

from src.rag.crag import Offline_RAG as CRAG

# ==========================================
# 2. PROMPT DỊCH CÂU HỎI (THUỐC TRỊ BỆNH MÙ NGỮ CẢNH)
# ==========================================
condense_prompt = ChatPromptTemplate.from_messages([
    ("system", "Dựa vào đoạn lịch sử trò chuyện dưới đây và câu hỏi mới nhất của người dùng. "
               "Hãy viết lại câu hỏi mới nhất thành một câu độc lập, đầy đủ chủ ngữ, vị ngữ và rõ ràng ý nghĩa để tìm kiếm tài liệu. "
               "CHỈ trả về đúng câu hỏi đã viết lại, không giải thích, không thêm chữ thừa nào khác."),
    MessagesPlaceholder("chat_history"),
    ("human", "{human_input}")
])

class InputChat(BaseModel):
    human_input: str = Field(
        ...,
        description="The human input to the chat system.",
        extra={"widget": {"type": "chat", "input": "human_input"}},
    )

def build_chat_chain(llm, retriever, history_folder, max_history_length):
    
    # A. KHỞI TẠO ĐỒ THỊ CRAG
    # CRAG sẽ nhận thầu toàn bộ logic: Tìm kiếm -> Đánh giá -> Cứu hộ -> Trả lời -> Chém văn phong
    crag_chain = CRAG(llm).get_chain(retriever)
    
    # B. XÂY DỰNG HÀM ĐIỀU PHỐI (ORCHESTRATOR)
    def process_chat_logic(inputs: dict) -> str:
        chat_history = inputs.get("chat_history", [])
        human_input = inputs.get("human_input", "")
        
        # Bước 1: Kiểm tra xem có lịch sử không để dịch câu hỏi
        if len(chat_history) > 0:
            condenser = condense_prompt | llm | StrOutputParser()
            standalone_question = condenser.invoke({
                "chat_history": chat_history,
                "human_input": human_input
            })
            print(f"\n[CHAT LOGIC] 📝 Dịch câu hỏi: '{human_input}' -> '{standalone_question}'")
        else:
            # Nếu là câu đầu tiên, lấy luôn câu của người dùng
            standalone_question = human_input
            
        # Bước 2: Bắn câu hỏi đã dịch vào luồng CRAG
        print(f"[CHAT LOGIC] 🚀 Chuyển giao sang hệ thống CRAG xử lý...")
        final_answer = crag_chain.invoke({"question": standalone_question})
        
        return final_answer

    # Bọc hàm điều phối thành Runnable để LangChain hiểu được
    core_chain = RunnableLambda(process_chat_logic)
    
    # C. LẮP BỘ NHỚ LỊCH SỬ BÊN NGOÀI CÙNG
    chain_with_history = RunnableWithMessageHistory(
        core_chain,
        create_session_factory(base_dir=history_folder, max_history_length=max_history_length),
        input_messages_key="human_input",
        history_messages_key="chat_history",
    )
    
    return chain_with_history