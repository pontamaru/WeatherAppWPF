using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using WeatherAppWpf.Models;
using WeatherAppWpf.Services.Json;
using WeatherAppWpf.Services.Weather;
using WeatherAppWpf.ViewModels;
using WeatherAppWpf.Views;

namespace WeatherAppWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// 天気情報を更新するためのサービスを初期化
			var settings = JsonService.LoadFromJsonFile<SettingsData>(SettingsData.SETTINGS_FILE_NAME);
			var cityCode = settings?.CityCode ?? "130010"; // デフォルトは東京
			var weather = new WeatherApiClient(cityCode: cityCode);

			// Modelを作成
			var todayModel = new TodayWeatherModel(weather);
			var settingsModel = new SettingsModel(settings);

			// ViewModelを作成し、Modelを渡す
			var todayWeatherViewModel = new TodayWeatherViewModel(todayModel, settingsModel);
			var settingsViewModel = new SettingsViewModel(settingsModel);
			//var threeDaysWeatherViewModel = new ThreeDaysWeatherViewModel();

			// Viewを作成し、ViewModelを渡す
			var mainWindow = new MainWindow();
			mainWindow.TodayWeatherView.SetViewModel(todayWeatherViewModel);
			mainWindow.SettingsView.SetViewModel(settingsViewModel);
			//mainWindow.ThreeDaysWeatherView.SetViewModel(threeDaysWeatherViewModel);

			// ウィンドウ表示
			mainWindow.Show();
		}
	}
}
