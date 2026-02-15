using Newtonsoft.Json;

namespace WeatherAppWpf.Services.Json
{
	[JsonObject("SettingsData")]
	public class SettingsData
	{
		public static readonly string SETTINGS_FILE_NAME = "settings.json";

		[JsonProperty("CityCode")]
		public string CityCode { get; set; } = string.Empty;		
	}
}