fetch('http://localhost:8000/get_flights')
    .then(response => response.json())
    .then(data => {
        const ctx = document.getElementById('flightChart').getContext('2d');
        const chart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: data.map(d => d.callsign),
                datasets: [{
                    label: 'Altitude',
                    data: data.map(d => d.geo_altitude),
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
    });
