using WeatherAppWpf.Services.Weather;

namespace WeatherAppWpf.Models
{
	public class ThreeDaysWeatherModel : WeatherModelBase
	{
		public ThreeDaysWeatherModel(WeatherApiClient weather) : base(weather)
		{
		}

		public string[] GetThreeDaysDateAndWeek()
		{
			return new string[] { base.GetDateAndWeekByIndex(0), base.GetDateAndWeekByIndex(1), base.GetDateAndWeekByIndex(2) };
		}

		public string[] GetThreeDaysTelop()
		{
			return new string[] { base.GetTodayTelopByIndex(0), base.GetTodayTelopByIndex(1), base.GetTodayTelopByIndex(2) };
		}

		public string[] GetThreeDaysTemperature()
		{
			return new string[] { base.GetTemperatureByIndex(0), base.GetTemperatureByIndex(1), base.GetTemperatureByIndex(2) };
		}

		public string[] GetThreeDaysRainProbability()
		{
			return new string[] { base.GetRainProbabilityByIndex(0), base.GetRainProbabilityByIndex(1), base.GetRainProbabilityByIndex(2) };
		}

		public string[] GetThreeDaysWeatherIcon()
		{
			return new string[] { base.GetWeatherIconByIndex(0), base.GetWeatherIconByIndex(1), base.GetWeatherIconByIndex(2) };
		}
	}
}
