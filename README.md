---
# Truy xuất dữ liệu chuyến bay bằng OpenSky Network API

![OpenSky Network]([https://opensky-network.org/images/logo_opensky.png](https://openskynetwork.github.io/opensky-api/_static/radar_small.png))

## Yêu cầu/chức năng của dự án:

- Tạo ứng dụng để truy xuất dữ liệu chuyến bay từ OpenSky Network API.
- Lưu trữ dữ liệu chuyến bay vào cơ sở dữ liệu MSSQL hoặc MySQL.
- Xây dựng một API bằng Python và FastAPI để truy xuất dữ liệu chuyến bay và cung cấp cho các ứng dụng khác.
- Tự động gọi API Python từ Node-RED để lấy dữ liệu và lưu vào cơ sở dữ liệu.
- Triển khai một trang web để hiển thị dữ liệu chuyến bay từ cơ sở dữ liệu.


## Chi tiết từng phần:

### 1. Cơ sở dữ liệu:

- **Loại**: MSSQL
- **Stored Procedures**:

### 2. Module đọc dữ liệu:

- Sử dụng Python và FastAPI để tạo API.
- Lấy dữ liệu từ OpenSky Network API thông qua yêu cầu HTTP.
- **Thuật toán**:
  - Gọi API của OpenSky Network để lấy thông tin về các chuyến bay.
  - Xử lý và lưu thông tin vào cơ sở dữ liệu.

### 3. Node-RED:

- Chu trình tự động gọi API Python để lấy dữ liệu.
- Xử lý dữ liệu và gọi Stored Procedure để lưu vào cơ sở dữ liệu.

### 4. Web:

- Sử dụng ASP.NET để tạo API lấy dữ liệu từ cơ sở dữ liệu.
- Triển khai trang web HTML, JS và CSS để hiển thị dữ liệu chuyến bay dưới dạng biểu đồ và bảng.
- Có chức năng export dữ liệu ra Excel.

---
