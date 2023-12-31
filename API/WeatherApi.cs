using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather_App.API;

public class WeatherApi
{
    private readonly HttpClient _client;
    private static readonly string? ApiKey = ConfigurationManager.AppSettings["ApiKey"];

    public WeatherApi()
    {
        _client = new HttpClient();
    }

    public async Task<ApiClass?> GetWeatherForecast(string? name, string country)
    {
        string requestUrl =
            $"https://api.openweathermap.org/data/2.5/weather?q={name},{country}&lang={Lang.Lang.LangToCode(MainWindow.country.ToString())}&appid={ApiKey}";
        try
        {
            HttpResponseMessage response = await _client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var weather = JsonConvert.DeserializeObject<ApiClass>(await response.Content.ReadAsStringAsync());
            return weather;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
            return null;
        }
    }
}

public class ApiClass
{
    public Coord? coord { get; set; }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public List<Weather>? weather { get; set; }

    public class Weather
    {
        public int id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }
    }

    public string? @base { get; set; }
    public Main? main { get; set; }

    public class Main
    {
        public float temp { get; set; }
        public float feelsLike { get; set; }
        public float tempMin { get; set; }
        public float tempMax { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int seaLevel { get; set; }
        public int grndLevel { get; set; }
    }

    public int visibility { get; set; }
    public Wind? wind { get; set; }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
        public float gust { get; set; }
    }

    public Clouds? clouds { get; set; }

    public class Clouds
    {
        public int all { get; set; }
    }

    public Rain? rain { get; set; }

    public class Rain
    {
        public float _1h { get; set; }
        public float _3h { get; set; }
    }

    public Snow? snow { get; set; }

    public class Snow
    {
        public float _1h { get; set; }
        public float _3h { get; set; }
    }

    public int dt { get; set; }
    public Sys? sys { get; set; }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string? country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public int timezone { get; set; }
    public int id { get; set; }
    public string? name { get; set; }
    public int cod { get; set; }
}