using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WeatherAppWpf.Models;

namespace WeatherAppWpf.ViewModels
{
	public partial class ThreeDaysWeatherViewModel : WeatherViewModelBase
	{
		private readonly ThreeDaysWeatherModel _threeDaysModel;

		public ThreeDaysWeatherViewModel(ThreeDaysWeatherModel threeDaysModel, SettingsModel settingsModel) : base(threeDaysModel, settingsModel)
		{
			_threeDaysModel = threeDaysModel;
			_threeDaysWeatherInfo = new ObservableCollection<WeatherViewData>
			{
				new WeatherViewData(),
				new WeatherViewData(),
				new WeatherViewData()
			};
		}

		[ObservableProperty]
		private ObservableCollection<WeatherViewData> _threeDaysWeatherInfo;

		protected override void UpdateViews()
		{
			// 3日分の天気情報を取得
			var threeDaysDateAndWeek = _threeDaysModel.GetThreeDaysDateAndWeek();
			var threeDaysTelop = _threeDaysModel.GetThreeDaysTelop();
			var threeDaysTemperature = _threeDaysModel.GetThreeDaysTemperature();
			var threeDaysRainProbability = _threeDaysModel.GetThreeDaysRainProbability();
			var threeDaysWeatherIcon = _threeDaysModel.GetThreeDaysWeatherIcon();

			// ビューの更新
			TargetCity = _threeDaysModel.GetCityName();
            for (int i = 0; i < threeDaysDateAndWeek.Length; i++)
            {
				ThreeDaysWeatherInfo[i].DateAndWeek = threeDaysDateAndWeek[i];
				ThreeDaysWeatherInfo[i].Weather = threeDaysTelop[i];
				ThreeDaysWeatherInfo[i].Temperature = threeDaysTemperature[i];
				ThreeDaysWeatherInfo[i].RainProbability = threeDaysRainProbability[i];
				ThreeDaysWeatherInfo[i].WeatherIcon = threeDaysWeatherIcon[i];
			}

			// 天気情報の更新が完了したので、コマンドの実行可否を再評価
			UpdateWeatherCommand.NotifyCanExecuteChanged();
		}
	}
}