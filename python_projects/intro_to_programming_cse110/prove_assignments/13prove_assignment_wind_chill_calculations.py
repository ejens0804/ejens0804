temperature = float(input("What is the temperature?"))
temperature_unit = input("Is the temperature in Celsius or Fahrenheit? (F/C)").upper()
wind_speed = 5


def wind_chill_faranheit(temperature, wind_speed):
    return 35.74 + 0.6215 * temperature - 35.75 * wind_speed ** 0.16 + 0.4275 * temperature * wind_speed ** 0.16

def wind_chill_celsius(temperature, wind_speed_kilometers):
    wind_speed_kilometers = wind_speed * 1.60934
    return (13.12 + 0.6215 * temperature - 11.37 * (wind_speed_kilometers ** 0.16) + 0.3965 * temperature * (wind_speed_kilometers ** 0.16)) * (9/5) + 32.05


if temperature_unit == "C":
    while wind_speed != 65:
        print(f"At temperature {temperature} {temperature_unit}, and {wind_speed} mph, the windchill is:{wind_chill_celsius(temperature, wind_speed):.2f}")
        wind_speed += 5
    
elif temperature_unit == "F":
    while wind_speed != 65:
        print(f"At temperature {temperature} {temperature_unit}, and {wind_speed} mph, the windchill is:{wind_chill_faranheit(temperature, wind_speed):.2f}")
        wind_speed += 5