from typing import Union,Type
from langchain_chroma import Chroma
from langchain_community.vectorstores import FAISS
from langchain_huggingface import HuggingFaceEmbeddings

class VectorDB:
    def __init__(self,documents=None,vector_db:Union[Chroma,FAISS]=Chroma,
        embedding=HuggingFaceEmbeddings(model_name="keepitreal/vietnamese-sbert"))->None:
        self.vector_db=vector_db
        self.embedding=embedding
        self.db=self._built_db(documents)
    def _built_db(self,documents):
        db=self.vector_db.from_documents(documents,embedding=self.embedding)
        return db
    
    def get_retriever(self,search_type:str="similarity",search_kwargs:dict={"k":8}):
        retriever=self.db.as_retriever(search_type=search_type,search_kwargs=search_kwargs)

        return retriever