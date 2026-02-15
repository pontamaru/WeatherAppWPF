using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace WeatherAppWpf.Services.Json
{
	class JsonService
	{
		public static void SaveToJsonFile<T>(string filePath, T data)
		{
			var json = JsonConvert.SerializeObject(data, Formatting.Indented);
			using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
			{
				// JSON データをファイルに書き込み
				sw.Write(json);
			}
		}

		public static T? LoadFromJsonFile<T>(string filePath) where T : class
		{
			if (!File.Exists(filePath))
				return null;

			using (var sr = new StreamReader(filePath, Encoding.UTF8))
			{
				var json = sr.ReadToEnd();
				return JsonConvert.DeserializeObject<T>(json);
			}
		}
	}
}
