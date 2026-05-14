# RAG Kaka - Huong dan chay du an

Du an la FastAPI + LangChain/LangServe cho bai toan RAG, dong goi san bang Docker.

## 1) Yeu cau

- Docker Desktop da cai dat va dang chay
- WSL2 (khuyen dung khi lam viec tren Windows)
- Ollama dang chay tren may host
- Model Ollama: `qwen2.5:1.5b`

Kiem tra model:

```bash
ollama list
```

Neu chua co:

```bash
ollama pull qwen2.5:1.5b
```

## 2) Chay du an

Di den thu muc du an:

```bash
cd /mnt/d/nckh_prj/rag_kaka
```

### Chay hang ngay (khong doi dependency)

```bash
docker compose up
```

### Khi co doi `requirements.txt` hoac `Dockerfile`

```bash
docker compose up --build
```

### Chay nen

```bash
docker compose up -d
```

## 3) Kiem tra da len app chua

Xem log:

```bash
docker compose logs -f rag-api
```

Khi thay dong:

`Uvicorn running on http://0.0.0.0:8000`

thi moi nen mo trinh duyet.

## 4) Link su dung

- Swagger docs: <http://localhost:8000/docs>
- Health check: <http://localhost:8000/check>
- LangServe playground (RAG): <http://localhost:8000/genai_langserve/playground/>
- LangServe playground (Chat): <http://localhost:8000/chat/playground/>

## 5) Test nhanh bang curl

### Health

```bash
curl http://localhost:8000/check
```

### Endpoint `generative_ai`

```bash
curl -X POST "http://localhost:8000/generative_ai" \
  -H "Content-Type: application/json" \
  -d '{"question":"Ho Guom o dau?"}'
```

## 6) Dung dich vu

```bash
docker compose down
```

## 7) Cac loi thuong gap

### `ERR_EMPTY_RESPONSE` hoac `Connection reset by peer`

Thuong do app chua startup xong (dang load model/du lieu).  
Cho den khi log co `Application startup complete` va `Uvicorn running ...`.

### `no configuration file provided: not found`

Ban dang o sai thu muc. Hay `cd` vao folder co `docker-compose.yml`.

### Warning `HF_TOKEN`

Chi la canh bao khong bat buoc. App van chay duoc, nhung toc do tai tu HuggingFace co the cham hon.

## 8) Cau hinh quan trong

Container dang goi Ollama qua bien moi truong:

- `OLLAMA_BASE_URL=http://host.docker.internal:11434`

Nghia la Ollama chay tren host, khong nam trong container `rag-api`.
