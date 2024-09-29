using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

class API
{
    private static readonly HttpClient client = new HttpClient();

    public string City { get; private set; }
    public double Temp { get; private set; }
    public double TempMin { get; private set; }
    public double TempMax { get; private set; }
    public string WeatherDescription { get; private set; }
    public string WeatherType { get; private set; }

    public bool Valid = true;

    public API(string city)
    {
        City = city;
    }

    public async Task GetWeatherData()
    {
        string apiKey = "5079b0c4778b5c82ed7ae0f1bbe7035b";
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={apiKey}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Invalid station", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Valid = false;
                return;
            }
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(responseBody);
            City = json["name"].ToString();
            Temp = (double)json["main"]["temp"];
            TempMin = (double)json["main"]["temp_min"];
            TempMax = (double)json["main"]["temp_max"];
            WeatherDescription = json["weather"][0]["description"].ToString();
            WeatherType = GetWeatherGroup(WeatherDescription);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    private string GetWeatherGroup(string weatherDescription)
    {
        switch (weatherDescription.ToLower())
        {
            case "clear sky":
            case "few clouds":
            case "scattered clouds":
            case "broken clouds":
            case "overcast clouds":
                return "Sun";

            case "shower rain":
            case "rain":
            case "light rain":
            case "moderate rain":
            case "heavy intensity rain":
            case "very heavy rain":
            case "extreme rain":
            case "freezing rain":
            case "light intensity shower rain":
            case "heavy intensity shower rain":
            case "ragged shower rain":
            case "drizzle":
            case "light intensity drizzle":
            case "heavy intensity drizzle":
            case "drizzle rain":
            case "heavy intensity drizzle rain":
            case "shower drizzle":
            case "mist":
            case "fog":
                return "Rain";

            case "snow":
            case "light snow":
            case "heavy snow":
            case "sleet":
            case "light shower sleet":
            case "shower sleet":
            case "light rain and snow":
            case "rain and snow":
            case "light shower snow":
            case "shower snow":
            case "heavy shower snow":
                return "Snow";

            case "thunderstorm":
            case "thunderstorm with light rain":
            case "thunderstorm with rain":
            case "thunderstorm with heavy rain":
            case "light thunderstorm":
            case "heavy thunderstorm":
            case "ragged thunderstorm":
            case "thunderstorm with light drizzle":
            case "thunderstorm with drizzle":
            case "thunderstorm with heavy drizzle":
            case "windy":
                return "Thunder";

            default:
                return "Unknown";
        }
    }
}