from typing import Union, List
import glob
from tqdm import tqdm
import multiprocessing
import json
from langchain_core.documents import Document
from langchain_text_splitters import RecursiveCharacterTextSplitter

# 1. Hàm load JSON (Biến JSON thành Document chuẩn LangChain)
def load_json(json_file):
    docs = []
    with open(json_file, 'r', encoding='utf-8') as f:
        data = json.load(f)
        for item in data:
            doc = Document(
                page_content=item.get("content", ""),
                metadata=item.get("metadata", {})
            )
            docs.append(doc)
    return docs

def get_num_cpu():
    return multiprocessing.cpu_count()

# 2. Class chạy đa luồng để load JSON siêu tốc
class JSONLoader:
    def __init__(self) -> None:
        self.num_processes = get_num_cpu()

    def __call__(self, json_files: List[str], workers: int = 1):
        num_processes = min(self.num_processes, workers)
        with multiprocessing.Pool(processes=num_processes) as pool:
            doc_loaded = []
            total_files = len(json_files)
            with tqdm(total=total_files, desc="Loading JSONs", unit="file") as pbar:
                for result in pool.imap_unordered(load_json, json_files):
                    doc_loaded.extend(result)
                    pbar.update(1)
        return doc_loaded

# 3. Class cắt văn bản
class TextSplitter:
    def __init__(self, separators: List[str] = ['\n\n', '\n', ' ', ''],
                 chunk_size: int = 2500, # Set mặc định cực lớn để giữ nguyên vẹn khối JSON
                 chunk_overlap: int = 600) -> None:
        self.splitter = RecursiveCharacterTextSplitter(
            separators=separators,
            chunk_size=chunk_size,
            chunk_overlap=chunk_overlap
        )
    def __call__(self, documents):
        return self.splitter.split_documents(documents)

# 4. Class Loader chính
class Loader:
    def __init__(self, split_kwargs: dict = None) -> None:
        # Nếu không truyền gì, mặc định sẽ giữ cục JSON to
        if split_kwargs is None:
            split_kwargs = {"chunk_size": 2500, "chunk_overlap": 600}

        self.doc_loader = JSONLoader()
        self.splitter = TextSplitter(**split_kwargs)

    def load(self, files: Union[str, List[str]], workers: int = 1):
        if isinstance(files, str):
            files = [files]
        doc_loaded = self.doc_loader(files, workers=workers)
        doc_split = self.splitter(doc_loaded)
        return doc_split
    
    def load_dir(self, dir_path: str, workers: int = 1):
        # Chỉ quét tìm file .json
        files = glob.glob(f"{dir_path}/*.json")
        assert len(files) > 0, f"Không tìm thấy file JSON nào trong thư mục {dir_path}"
        return self.load(files, workers=workers)