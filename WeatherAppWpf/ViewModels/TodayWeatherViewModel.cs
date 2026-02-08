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
			_todayModel.SetUpdatedAction(UpdateViews);
			_settingsModel = settingsModel;
			_settingsModel.OnSettingsChanged += OnSettingsChanged;

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

		[RelayCommand(CanExecute = nameof(CanCulcxulate))]
		private void UpdateWeather()
		{
			_todayModel.UpdateWeather();
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}

		private void UpdateViews()
		{
			TodayDateAndWeek = _todayModel.GetTodayDateAndWeek();
			TargetCity = _todayModel.GetCityName();
			TodayWeather = _todayModel.GetTodayTelop();
			TodayTemperature = _todayModel.GetTodayTemperature();
			TodayRainProbability = _todayModel.GetTodayRainProbability();

			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}

		private void OnSettingsChanged()
		{
			var cityCode = _settingsModel.GetCityCode();
			_todayModel.SetCityCode(cityCode);
			UpdateWeather();
		}

		private bool CanCulcxulate() => _todayModel.CanUpdate;
	}
}