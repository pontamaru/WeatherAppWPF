using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WeatherAppWpf.Models;

namespace WeatherAppWpf.ViewModels
{
	public partial class TodayWeatherViewModel : WeatherViewModelBase
	{
		private readonly TodayWeatherModel _todayModel;

		public TodayWeatherViewModel(TodayWeatherModel todayModel, SettingsModel settingsModel) : base(todayModel, settingsModel)
		{
			_todayModel = todayModel;
		}

		[ObservableProperty]
		private WeatherViewData _todayWeatherInfo = new WeatherViewData();

		protected override void UpdateViews()
		{
			// ビューの更新
			TargetCity = _todayModel.GetCityName();
			TodayWeatherInfo.DateAndWeek = _todayModel.GetTodayDateAndWeek();
			TodayWeatherInfo.Weather = _todayModel.GetTodayTelop();
			TodayWeatherInfo.Temperature = _todayModel.GetTodayTemperature();
			TodayWeatherInfo.RainProbability = _todayModel.GetTodayRainProbability();
			TodayWeatherInfo.WeatherIcon = _todayModel.GetTodayWeatherIcon();

			// 天気情報の更新が完了したので、コマンドの実行可否を再評価
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}
	}
}