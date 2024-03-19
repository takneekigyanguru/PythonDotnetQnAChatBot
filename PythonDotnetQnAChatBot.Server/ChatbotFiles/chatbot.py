import os
import sys

 #pip install langchain
 #pip install OpenAI

from langchain.document_loaders import TextLoader
from langchain.indexes import VectorstoreIndexCreator
from langchain.llms import OpenAI
from langchain.chat_models import ChatOpenAI

   
with open("apikey.txt", "rb") as f:
    apikey = f.read().decode("utf-8-sig").strip()
    
os.environ["OPENAI_API_KEY"] = apikey

query = sys.argv[1]


loader = TextLoader("qa.json")

index = VectorstoreIndexCreator().from_loaders([loader])

print(index.query(query))
