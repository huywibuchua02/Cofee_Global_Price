$(document).ready(function () {
    // Lấy thông tin vị trí từ FastAPI thông qua biến locationData
    var locationData = locationData;

    // Hiển thị thông tin vị trí từ dữ liệu JSON
    $('#ipAddress').text(locationData.ip);
    $('#countryCode').text(locationData.country_code);
    $('#countryName').text(locationData.country_name);
    $('#regionName').text(locationData.region_name);
    $('#city').text(locationData.city);
    $('#zip').text(locationData.zip);
    $('#latitude').text(locationData.latitude);
    $('#longitude').text(locationData.longitude);

    // Chức năng tạo file Excel từ dữ liệu vị trí
    $('#exportExcelBtn').click(function () {
        // Tạo một mảng chứa dữ liệu vị trí
        var locationArray = [
            ['IP Address', locationData.ip],
            ['Country Code', locationData.country_code],
            ['Country Name', locationData.country_name],
            ['Region Name', locationData.region_name],
            ['City', locationData.city],
            ['Zip', locationData.zip],
            ['Latitude', locationData.latitude],
            ['Longitude', locationData.longitude]
        ];

        // Tạo workbook và worksheet
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet('Location Information');

        // Thêm dữ liệu từ mảng vào worksheet
        locationArray.forEach(function (row) {
            worksheet.addRow(row);
        });

        // Tạo tệp Excel và tải xuống
        workbook.xlsx.writeBuffer().then(function (buffer) {
            var blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
            var url = URL.createObjectURL(blob);

            // Tạo một thẻ 'a' để tải xuống tệp Excel
            var a = document.createElement('a');
            a.href = url;
            a.download = 'location_information.xlsx';
            a.click();

            // Giải phóng URL
            URL.revokeObjectURL(url);
        });
    });
});
