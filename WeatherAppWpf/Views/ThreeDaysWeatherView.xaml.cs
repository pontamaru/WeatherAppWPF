using System.Windows.Controls;
using WeatherAppWpf.ViewModels;

namespace WeatherAppWpf.Views
{
	/// <summary>
	/// ThreeDayWeatherView.xaml の相互作用ロジック
	/// </summary>
	public partial class ThreeDaysWeatherView : UserControl
    {
        public ThreeDaysWeatherView()
        {
            InitializeComponent();
		}

		public void SetViewModel(ThreeDaysWeatherViewModel viewModel)
		{
			DataContext = viewModel;
		}
	}
}
