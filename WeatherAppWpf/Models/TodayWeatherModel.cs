using WeatherAppWpf.Services.Weather;

namespace WeatherAppWpf.Models
{
	public class TodayWeatherModel
	{
		private WeatherApiClient _weatherApiClient;

		public TodayWeatherModel(WeatherApiClient weather)
		{
			_weatherApiClient = weather;
		}

		public bool CanUpdate => !_weatherApiClient.IsUpdating;

		/// <summary>
		/// 天気情報を更新する
		/// </summary>
		public void UpdateWeather()
		{
			if (!CanUpdate)
				return;

			_weatherApiClient.UpdateWeatherInfoAsync();
		}

		public void SetCityCode(string cityCode)
		{
			_weatherApiClient.SetCityCode(cityCode);
		}

		/// <summary>
		/// 天気情報の更新が完了したときに呼び出されるアクションを設定
		/// </summary>
		/// <param name="finishedAction"></param>
		public void SetUpdatedAction(Action? finishedAction)
		{
			_weatherApiClient.SetUpdatedAction(finishedAction);
		}

		public string GetTodayDateAndWeek()
		{
			var todayWeather = _weatherApiClient.WeatherInfoList.FirstOrDefault();
			if (todayWeather == null)
				return "XXXX/XX/XX (X)";

			DateTime date = DateTime.Parse(todayWeather.Date!);
			string dayOfWeek = date.ToString("ddd");
			return $"{date.ToString("yyyy/MM/dd")} ({dayOfWeek})";
		}

		public string GetCityName()
		{
			return _weatherApiClient.CityName;
		}

		public string GetTodayTelop()
		{

			var todayWeather = _weatherApiClient.WeatherInfoList.FirstOrDefault();
			if (todayWeather == null)
				return "不明";

			return todayWeather.Telop ?? "不明";
		}

		public string GetTodayTemperature()
		{
			var todayWeather = _weatherApiClient.WeatherInfoList.FirstOrDefault();
			if (todayWeather == null)
				return "最低: --℃ 最高: --℃";

			string minTemp = todayWeather.TemperatureMin.HasValue ? todayWeather.TemperatureMin.Value.ToString() : "--";
			string maxTemp = todayWeather.TemperatureMax.HasValue ? todayWeather.TemperatureMax.Value.ToString() : "--";
			return $"最低: {minTemp}℃ 最高: {maxTemp}℃";
		}

		public string GetTodayRainProbability()
		{
			var todayWeather = _weatherApiClient.WeatherInfoList.FirstOrDefault();
			if (todayWeather == null || todayWeather.ChanceOfRain == null)
				return "0% 0% 0% 0%";

			return string.Join(" ", todayWeather.ChanceOfRain);
		}
	}
}
