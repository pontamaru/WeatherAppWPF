using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using WeatherAppWpf.Models;

namespace WeatherAppWpf.ViewModels
{
	public partial class TodayWeatherViewModel : ObservableObject
	{
		private readonly TodayWeatherModel _todayModel;
		private readonly SettingsModel _settingsModel;

		public TodayWeatherViewModel(TodayWeatherModel todayeModel, SettingsModel settingsModel)
		{
			_todayModel = todayeModel;
			_settingsModel = settingsModel;

			// Viewを更新するためのアクションを設定
			_todayModel.SetUpdatedAction(UpdateViews);
			_settingsModel.SetChangedAction(OnSettingsChanged);

			// 初期表示のために天気情報を更新
			UpdateWeather();
		}

		[ObservableProperty]
		private string _todayDateAndWeek = string.Empty;
		[ObservableProperty]
		private string _targetCity = string.Empty;
		[ObservableProperty]
		private string _todayWeather = string.Empty;
		[ObservableProperty]
		private string _todayTemperature = string.Empty;
		[ObservableProperty]
		private string _todayRainProbability = string.Empty;

		[RelayCommand(CanExecute = nameof(CanUpdateWeather))]
		private void UpdateWeather()
		{
			_todayModel.UpdateWeather();

			// 更新中はコマンドを無効化するため、CanExecuteChangedを通知
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}

		private void UpdateViews()
		{
			TodayDateAndWeek = _todayModel.GetTodayDateAndWeek();
			TargetCity = _todayModel.GetCityName();
			TodayWeather = _todayModel.GetTodayTelop();
			TodayTemperature = _todayModel.GetTodayTemperature();
			TodayRainProbability = _todayModel.GetTodayRainProbability();

			// 天気情報の更新が完了したので、コマンドの実行可否を再評価
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}

		/// <summary>
		/// 設定が変更されたときの処理
		/// </summary>
		private void OnSettingsChanged()
		{
			var cityCode = _settingsModel.GetCityCode();
			_todayModel.SetCityCode(cityCode);
			UpdateWeather();
		}

		private bool CanUpdateWeather() => _todayModel.CanUpdate;
	}
}