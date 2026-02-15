using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherAppWpf.Models;

namespace WeatherAppWpf.ViewModels
{
	public abstract partial class WeatherViewModelBase : ObservableObject
	{
		protected readonly WeatherModelBase _model;
		protected readonly SettingsModel _settingsModel;

		[ObservableProperty]
		private string _targetCity = string.Empty;

		protected WeatherViewModelBase(WeatherModelBase model, SettingsModel settingsModel)
		{
			_model = model;
			_settingsModel = settingsModel;

			// Viewを更新するためのアクションを設定
			_model.SetUpdatedAction(UpdateViews);
			_settingsModel.SetChangedAction(OnSettingsChanged);

			// 初期表示のために天気情報を更新
			UpdateWeather();
		}

		[RelayCommand(CanExecute = nameof(CanUpdateWeather))]
		protected void UpdateWeather()
		{
			_model.UpdateWeather();

			// 更新中はコマンドを無効化するため、CanExecuteChangedを通知
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}

		/// <summary>
		/// ビューの更新処理は派生クラスで実装
		/// </summary>
		protected abstract void UpdateViews();

		/// <summary>
		/// 設定が変更されたときの処理
		/// </summary>
		protected void OnSettingsChanged()
		{
			var cityCode = _settingsModel.GetCityCode();
			_model.SetCityCode(cityCode);
			UpdateWeather();
		}

		protected bool CanUpdateWeather() => _model.CanUpdate;
	}

	/// <summary>
	/// 一日分の天気情報表示用のデータクラス
	/// </summary>
	public partial class WeatherViewData : ObservableObject
	{
		[ObservableProperty]
		private string _dateAndWeek = string.Empty;
		[ObservableProperty]
		private string _weather = string.Empty;
		[ObservableProperty]
		private string _temperature = string.Empty;
		[ObservableProperty]
		private string _rainProbability = string.Empty;
		[ObservableProperty]
		private string _weatherIcon = string.Empty;
	}
}