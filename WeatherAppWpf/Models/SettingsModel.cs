using System;
using System.Collections.Generic;
using System.Text;
using WeatherAppWpf.Services.Json;

namespace WeatherAppWpf.Models
{
    public class SettingsModel
	{
        private readonly SettingsData? settings;

		public SettingsModel(SettingsData? data)
		{
			settings = data;
		}

		public Action? OnSettingsChanged { get; set; }

		public void SaveSettings(string cityCode)
		{
			if (settings != null)
			{
				settings.CityCode = cityCode;
				JsonService.SaveToJsonFile<SettingsData>(SettingsData.SETTINGS_FILE_NAME, settings);
			}
			else
			{
				var writeDate = new SettingsData { CityCode = cityCode };
				JsonService.SaveToJsonFile<SettingsData>(SettingsData.SETTINGS_FILE_NAME, writeDate);
			}
			OnSettingsChanged?.Invoke();
		}

		public string GetCityCode()
		{
			return settings?.CityCode ?? string.Empty;
		}
	}
}
