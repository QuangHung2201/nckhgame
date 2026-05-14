import requests
# Gửi thẳng một đoạn chữ siêu dài để ép nó lòi ra lỗi 59 ký tự
res = requests.post("http://localhost:11434/api/chat", json={
    "model": "hanoi_tour_guide",
    "messages": [{"role": "user", "content": "test " * 3000}] 
})
print("LỖI THỰC SỰ LÀ:", res.text)