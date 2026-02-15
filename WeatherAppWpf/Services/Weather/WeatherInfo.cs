namespace WeatherAppWpf.Services.Weather
{
	public class WeatherInfo
	{
		public string? Date { get; set; } = null;
		public string? DateLabel { get; set; } = null;
		public string? Telop { get; set; } = null;
		public int? TemperatureMin { get; set; } = null;
		public int? TemperatureMax { get; set; } = null;
		public string[]? ChanceOfRain { get; set; } = null;
		public string? ImageUrl { get; set; } = null;

		public string Desplay()
		{
			return $"{DateLabel}({Date}) {Telop} 最低気温:{TemperatureMin}℃ 最高気温:{TemperatureMax}℃ 降水確率:{string.Join(",", ChanceOfRain)}";
		}
	}
}
