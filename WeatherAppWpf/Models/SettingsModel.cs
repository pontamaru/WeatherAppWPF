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

		private Action? _onSettingsChanged;

		/// <summary>
		/// 受け取った都市コードで設定を保存する
		/// </summary>
		/// <param name="cityCode"></param>
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
			_onSettingsChanged?.Invoke();
		}

		/// <summary>
		/// 設定変更を行ったときに呼び出されるアクションを設定
		/// </summary>
		/// <param name="finishedAction"></param>
		public void SetChangedAction(Action? finishedAction)
		{
			_onSettingsChanged = finishedAction;
		}

		public string GetCityCode()
		{
			return settings?.CityCode ?? string.Empty;
		}
	}
}
