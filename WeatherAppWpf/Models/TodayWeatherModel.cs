using WeatherAppWpf.Services.Weather;

namespace WeatherAppWpf.Models
{
	public class TodayWeatherModel : WeatherModelBase
	{
		public TodayWeatherModel(WeatherApiClient weather) : base(weather)
		{
		}
		public string GetTodayDateAndWeek()
		{
			return base.GetDateAndWeekByIndex(0);
		}

		public string GetTodayTelop()
		{

			return base.GetTodayTelopByIndex(0);
		}

		public string GetTodayTemperature()
		{
			return base.GetTemperatureByIndex(0);
		}

		public string GetTodayRainProbability()
		{
			return base.GetRainProbabilityByIndex(0);
		}

		public string GetTodayWeatherIcon()
		{
			return base.GetWeatherIconByIndex(0);
		}
	}
}
