using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ReceiptReward.Config
{
	public class TestSettings
	{
		public string Version { get; set; } = null!;
		public string Path { get; set; } = null!;
	}
	public class AppConfig
	{
		public string Environment { get; set; } = "production";
		public string Version { get; set; } = "V01";
		public Dictionary<string, List<string>> Calculations { get; set; } = new();
		public TestSettings Test { get; set; } = new();


		public static AppConfig Load(string path = "config.yml")
		{
			if (!File.Exists(path)) return new AppConfig();

			var yaml = File.ReadAllText(path);
			var deserializer = new DeserializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();

			return deserializer.Deserialize<AppConfig>(yaml);
		}
	}
}
