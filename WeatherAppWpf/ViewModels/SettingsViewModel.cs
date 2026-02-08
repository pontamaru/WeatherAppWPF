using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System.Windows.Controls;
using WeatherAppWpf.Models;
using WeatherAppWpf.Services;

namespace WeatherAppWpf.ViewModels
{
	public partial class SettingsViewModel : ObservableObject
	{
		private readonly SettingsModel _model;
		public SettingsViewModel(SettingsModel model)
		{
			_model = model;

			// コンボボックスに都市一覧を追加
			var code = _model.GetCityCode();
			var selectLabel = string.Empty;
			foreach (var item in CityCodeData.CityCodes)
            {
				var label = $"{item.Key} - {item.Value}";
				if (item.Value.Contains(code))
				{
					selectLabel = label;
				}

				AddCity(label);
			}
			SelectedComboBoxItem = selectLabel;
		}

		[ObservableProperty]
		private ObservableCollection<string> _cityComboBox = new ObservableCollection<string>();

		[ObservableProperty]
		private string _selectedComboBoxItem = string.Empty;

		[RelayCommand]
		private void SelectedItem()
		{
			if (string.IsNullOrEmpty(SelectedComboBoxItem))
				return;

			var cityCode = SelectedComboBoxItem.Split(" - ")[1];
			_model.SaveSettings(cityCode);
		}

		public void AddCity(string city)
		{
			CityComboBox.Add(city);
		}
	}
}