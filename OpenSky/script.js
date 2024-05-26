document.addEventListener('DOMContentLoaded', function () {
    // Lấy dữ liệu từ API và điền vào các thẻ HTML
    fetch('/api/location/getlocation?ipAddress=117.5.147.158')
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch location data');
            }
            return response.json();
        })
        .then(data => {
            document.getElementById('ipAddress').innerText = data.ip;
            document.getElementById('countryCode').innerText = data.country_code;
            document.getElementById('countryName').innerText = data.country_name;
            document.getElementById('regionName').innerText = data.region_name;
            document.getElementById('city').innerText = data.city;
            document.getElementById('zip').innerText = data.zip;
            document.getElementById('latitude').innerText = data.latitude;
            document.getElementById('longitude').innerText = data.longitude;
        })
        .catch(error => console.error('Error fetching location data:', error));

    // Xử lý sự kiện cho nút "Xuất Excel" nếu cần
    document.getElementById('exportExcelBtn').addEventListener('click', function () {
        // Viết logic xử lý tại đây
    });
});
