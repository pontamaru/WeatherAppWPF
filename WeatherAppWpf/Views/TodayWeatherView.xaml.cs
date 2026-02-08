using System.Windows.Controls;
using WeatherAppWpf.ViewModels;

namespace WeatherAppWpf.Views
{
	/// <summary>
	/// TodayWeatherView.xaml の相互作用ロジック
	/// </summary>
	public partial class TodayWeatherView : UserControl
    {
        public TodayWeatherView()
        {
            InitializeComponent();
		}

		public void SetViewModel(TodayWeatherViewModel viewModel)
		{
			DataContext = viewModel;
		}
	}
}
