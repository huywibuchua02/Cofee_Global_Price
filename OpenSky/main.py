from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import requests
import pyodbc

app = FastAPI()

# Kết nối đến cơ sở dữ liệu MSSQL
conn = pyodbc.connect('DRIVER={SQL Server};SERVER=127.0.0.1\SQLEXPRESS;DATABASE=IPSTACK;UID=sa;PWD=123')
cursor = conn.cursor()

class Location(BaseModel):
    ip_address: str

@app.post("/get_location/")
async def get_location(location: Location):
    # Lấy thông tin từ ipstack
    api_key = '256b7419297060449c6ea234d054a5f6'
    url = f'http://api.ipstack.com/{location.ip_address}?access_key={api_key}'
    response = requests.get(url)
    
    if response.status_code == 200:
        data = response.json()
        # Lưu thông tin vào cơ sở dữ liệu
        cursor.execute("EXEC AddLocationData ?, ?, ?, ?, ?, ?, ?, ?", 
                       (data['ip'], data['country_code'], data['country_name'], data['region_name'],
                        data['city'], data['zip'], data['latitude'], data['longitude']))
        conn.commit()
        
        # Trả về thông tin vị trí dưới dạng JSON
        return data
    else:
        raise HTTPException(status_code=404, detail="Không tìm thấy thông tin vị trí")
