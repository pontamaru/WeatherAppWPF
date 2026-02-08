using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherAppWpf.Services.Weather
{
    public class WeatherApiClient
	{
		private static readonly string API_URL = "https://weather.tsukumijima.net/api/forecast/city/{0}";

		public List<WeatherInfo> WeatherInfoList { get; set; }
		public string CityName { get; set; } = string.Empty;
		public bool IsUpdating { get; private set; } = false;

		private string _cityCode;
		private Action? _onWeatherInfoUpdated;

		public WeatherApiClient(string cityCode = "", Action? finishedAction = null)
		{
			_cityCode = cityCode;
			_onWeatherInfoUpdated = finishedAction;
			WeatherInfoList = new List<WeatherInfo>();
		}

		public void SetCityCode(string cityCode)
		{
			_cityCode = cityCode;
		}

		public void SetUpdatedAction(Action? finishedAction)
		{
			_onWeatherInfoUpdated = finishedAction;
		}

		/// <summary>
		/// 非同期で天気情報を更新する
		/// </summary>
		public async void UpdateWeatherInfoAsync()
		{
			IsUpdating = true;
			if (!string.IsNullOrEmpty(_cityCode))
			{
				WeatherInfoList.Clear();
				await FetchWeatherInfoAsync();
				WeatherInfoList.ForEach(info => Debug.WriteLine($"[WeatherApiClient]{info.Desplay()}"));
			}

			IsUpdating = false;
			_onWeatherInfoUpdated?.Invoke();
		}

		private async Task FetchWeatherInfoAsync()
		{
			if (string.IsNullOrEmpty(_cityCode))
			{
				Debug.WriteLine("[WeatherApiClient]CityCodeが未設定");
				return;
			}

			using (HttpClient client = new HttpClient())
			{
				string requestUrl = string.Format(API_URL, _cityCode);
				HttpResponseMessage response = await client.GetAsync(requestUrl);
				if (response.IsSuccessStatusCode)
				{
					string jsonResponse = await response.Content.ReadAsStringAsync();
					JObject weatherJsonData = JObject.Parse(jsonResponse);

					// 都市名を取得
					CityName = (string?)weatherJsonData["location"]?["city"] ?? string.Empty;

					// ３日分の天気情報をリストで取得
					var forecasts = weatherJsonData["forecasts"];
                    if (forecasts != null)
                    {
                        foreach (var forecast in forecasts)
                        {
							WeatherInfoList.Add(new WeatherInfo
							{
								Date = (string?)forecast["date"],
								DateLabel = (string?)forecast["dateLabel"],
								Telop = (string?)forecast["telop"],
								TemperatureMin = (int?)forecast["temperature"]?["min"]?["celsius"],
								TemperatureMax = (int?)forecast["temperature"]?["max"]?["celsius"],
								ChanceOfRain = new string[]
								{
									forecast["chanceOfRain"]?["T00_06"]?.ToString() ?? string.Empty,
									forecast["chanceOfRain"]?["T06_12"]?.ToString() ?? string.Empty,
									forecast["chanceOfRain"]?["T12_18"]?.ToString() ?? string.Empty,
									forecast["chanceOfRain"]?["T18_24"]?.ToString() ?? string.Empty
								},
								ImageUrl = (string?)forecast["image"]?["url"]
							});
						}
                    }
				}
				else
				{
					Debug.WriteLine($"[WeatherApiClient]天気情報取得失敗: {response.StatusCode}");
					return;
				}
			}
		}
	}
}
