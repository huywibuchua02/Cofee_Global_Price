document.addEventListener('DOMContentLoaded', function () {
    // Lấy dữ liệu từ API và điền vào các thẻ HTML
    fetch('/apicff.aspx?action=get_all_coffee')
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to fetch location data');
            }
            return response.json();
        })  
        .then(jsonData => {
            //document.getElementById('kq').innerText = JSON.stringify(jsonData);
            //let now = jsonData.data.
            document.getElementById('now').innerText = JSON.stringify("2024-04-01 : 239 cents per pound");


            // Lấy tham chiếu tới canvas
            var ctx = document.getElementById('myChart').getContext('2d');

            // Dữ liệu JSON mà bạn đã cung cấp
            // Tạo các mảng chứa ngày và giá trị từ dữ liệu JSON
            var dates = [];
            var values = [];
            jsonData.data.forEach(function (item) {
                dates.push(item.date);
                values.push(item.value);
            });

            // Đảo lại mảng 
            dates.reverse();
            values.reverse();


            // Tạo biểu đồ
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: dates, // Ngày làm trục x
                    datasets: [{
                        label: 'Giá trị cà phê ',
                        data: values, // Giá trị làm trục y
                        borderColor: 'rgb(75, 192, 192)', // Màu của đường biểu đồ
                        tension: 0.1 // Độ cong của đường
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: false // Bắt đầu trục y từ giá trị khác 0
                        }
                    }
                }
            });



           
        })
        .catch(error => console.error('Error fetching location data:', error));

    document.getElementById('exportExcelBtn').addEventListener('click', function () {
        // Viết logic xử lý tại đây
    });
});
