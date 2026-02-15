using System.Windows.Controls;
using WeatherAppWpf.ViewModels;

namespace WeatherAppWpf.Views
{
	/// <summary>
	/// SettingsView.xaml の相互作用ロジック
	/// </summary>
	public partial class SettingsView : UserControl
	{
		public SettingsView()
		{
			InitializeComponent();
		}

		public void SetViewModel(SettingsViewModel viewModel)
		{
			DataContext = viewModel;
		}
	}
}
