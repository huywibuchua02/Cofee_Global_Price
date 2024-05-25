from fastapi import FastAPI
import requests

# Tạo một ứng dụng FastAPI
app = FastAPI()

# Định nghĩa tuyến đường cho ứng dụng
@app.get("/")
async def get_ip_info():
    # Sử dụng API key mà bạn đã lấy được
    api_key = '50b2f28a27b9b0718953bd387dd219fb'
    
    # Địa chỉ IP cần lấy thông tin
    ip_address = '117.5.147.158'
    
    # URL của API ipstack
    url = f'http://api.ipstack.com/{ip_address}?access_key={api_key}'
    
    # Gửi yêu cầu đến API
    response = requests.get(url)
    
    # Kiểm tra xem yêu cầu có thành công không
    if response.status_code == 200:
        # Chuyển đổi dữ liệu JSON thành dictionary
        data = response.json()
        
        # Trả về dữ liệu dưới dạng JSON
        return data
    else:
        # Nếu yêu cầu không thành công, trả về một thông báo lỗi
        return {"error": "Failed to fetch IP information"}

# Chạy ứng dụng với Uvicorn
# Định nghĩa mô-đun "main" trong tệp này, chạy:
# uvicorn main:app --reload
