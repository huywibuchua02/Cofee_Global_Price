from fastapi import FastAPI, HTTPException, Header
import requests

app = FastAPI()

@app.get("/")
def home():
    return "Trang chủ api vào /data"

@app.get("/data")
async def get_coffee_data(api_key: str = Header(None)):
    api_key = "BGPYNSBKZV6CQDHD"
    if api_key is None:
        raise HTTPException(status_code=400, detail="API key is required")
    # https://www.alphavantage.co/query?function=COFFEE&interval=monthly&apikey=BGPYNSBKZV6CQDHD
    url = f'https://www.alphavantage.co/query?function=COFFEE&interval=monthly&apikey={api_key}'
    response  = requests.get(url)
    if response .status_code != 200:
        raise HTTPException(status_code=500, detail="Failed to fetch data from Alpha Vantage")
    
    # Đổi từ JSON thành một đối tượng Python
    data = response .json()
    
    return data
