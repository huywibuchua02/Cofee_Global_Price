# -OpenSky_Network_API
## Giới thiệu
Cách sử dụng Node-RED để truy xuất dữ liệu chuyến bay từ API mạng OpenSky Network và hiển thị trên bản đồ thế giới.
## Cài đặt

Để cài đặt phiên bản ổn định, bạn có thể sử dụng giao diện **Manage palette** trong Node-RED hoặc chạy lệnh sau trong thư mục người dùng Node-RED của bạn:

```bash
npm install node-red-contrib-opensky-network
```

## Cách sử dụng

1. Đặt nút **OpenSky Network** vào không gian làm việc trên trình chỉnh sửa luồng Node-RED.
2. Thêm và kết nối với nút **worldmap** trong mô-đun `node-red-contrib-web-worldmap`.
3. Nhập 4 giá trị (vĩ độ nam, kinh độ tây, vĩ độ bắc và kinh độ đông) vào thuộc tính của nút **Mạng OpenSky**.
4. Triển khai quy trình và mở giao diện người dùng bản đồ thế giới

Sau khi mở giao diện người dùng bản đồ thế giới, bạn có thể xem dữ liệu chuyến bay trên giao diện người dùng theo thời gian thực.
