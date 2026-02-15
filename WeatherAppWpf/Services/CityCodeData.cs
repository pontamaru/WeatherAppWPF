using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAppWpf.Services
{
	class CityCodeData
	{
		// TODO: 都市コードはAPIから取得するようにしたい
		public static readonly Dictionary<string, string> CityCodes = new Dictionary<string, string>
		{
			{ "札幌", "016010" },
			{ "東京", "130010" },
			{ "長野", "200010" },
			{ "名古屋", "230010" },
			{ "京都", "260010" },
			{ "大阪", "270000" },
			{ "福岡", "400010" },
			{ "那覇", "471010" }
		};
	}
}
