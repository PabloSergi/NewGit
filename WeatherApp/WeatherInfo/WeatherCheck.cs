using System.Net;
using Newtonsoft.Json;
using WeatherApp.WeatherInfo;
using WeatherApp.MySql;
using WeatherApp.Exceptions;

namespace WeatherApp.WeatherInfo
{
    public class WeatherCheck
    {
        public static (string weather, string icon) CheckWeather(string cityName)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&lang=ru&units=metric&appid=271988e019a63beb1e08c8550b0d872f";
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            string answer = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                using StreamReader streamReader = new StreamReader(stream);
                answer = streamReader.ReadToEnd();
            }

            response.Close();

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(answer);

            return
                (weather:
                $"Температура в городе {weatherResponse.Name}: {weatherResponse.Main.Temp} °C\n" +
                $"Влажность: {weatherResponse.Main.Humidity}%\n" +
                $"Скорость ветра: {weatherResponse.Wind.Speed} м/с\n",
                icon:
                $"images/01d.png");


            //Console.WriteLine(weatherResponse.Weather[0].Icon); {weatherResponse.Weather[0].Icon}
            //Console.WriteLine(); $"icons/{weatherResponse.Weather[0].Icon}.png"
            //$"http://openweathermap.org/img/wn/{weatherResponse.Weather[0].Icon}@2x.png"

        }
    }
}

