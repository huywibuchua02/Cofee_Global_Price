from fastapi import FastAPI
from pydantic import BaseModel
import requests
import pyodbc
import json

app = FastAPI()

# Database connection string
connection_string = "Driver={ODBC Driver 17 for SQL Server};Server=127.0.0.1\SQLEXPRESS;Database=flightdata;UID=sa;PWD=123;"

class FlightData(BaseModel):
    icao24: str
    callsign: str
    origin_country: str
    time_position: int
    last_contact: int
    longitude: float
    latitude: float
    baro_altitude: float
    on_ground: bool
    velocity: float
    true_track: float
    vertical_rate: float
    sensors: str
    geo_altitude: float
    squawk: str
    spi: bool
    position_source: int

def insert_flight_data(flight):
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("""
        EXEC SP_InsertFlightData ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?
    """, (
        flight["icao24"],
        flight["callsign"],
        flight["origin_country"],
        flight["time_position"],
        flight["last_contact"],
        flight["longitude"],
        flight["latitude"],
        flight["baro_altitude"],
        flight["on_ground"],
        flight["velocity"],
        flight["true_track"],
        flight["vertical_rate"],
        flight["sensors"],
        flight["geo_altitude"],
        flight["squawk"],
        flight["spi"],
        flight["position_source"]
    ))
    conn.commit()
    cursor.close()
    conn.close()

@app.get("/fetch_flights")
def fetch_flights():
    url = "https://opensky-network.org/api/states/all"
    response = requests.get(url)
    data = response.json()

    flights = []
    for state in data['states']:
        flight = {
            "icao24": state[0],
            "callsign": state[1],
            "origin_country": state[2],
            "time_position": state[3],
            "last_contact": state[4],
            "longitude": state[5],
            "latitude": state[6],
            "baro_altitude": state[7],
            "on_ground": state[8],
            "velocity": state[9],
            "true_track": state[10],
            "vertical_rate": state[11],
            "sensors": json.dumps(state[12]) if state[12] else None,
            "geo_altitude": state[13],
            "squawk": state[14],
            "spi": state[15],
            "position_source": state[16]
        }
        flights.append(flight)
        insert_flight_data(flight)

    return {"status": "success", "data": flights}

@app.get("/get_flights")
def get_flights():
    conn = pyodbc.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM Flights")
    columns = [column[0] for column in cursor.description]
    results = []
    for row in cursor.fetchall():
        results.append(dict(zip(columns, row)))
    cursor.close()
    conn.close()
    return results
