from llm_model import get_hf_llm

llm = get_hf_llm(
    max_new_token=128,
    temperature=0.7,
    top_p=0.9
)

response = llm("Xin chào, bạn là ai?")
print(response)