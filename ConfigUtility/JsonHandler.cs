using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigUtility
{
	public static class JsonHandler
	{
		public static ModConfig ParseConfigJson()
		{
			ModConfig modConfig = new ModConfig();
			string configPath = Directory.GetCurrentDirectory() + "\\config.json";
			if (!File.Exists(configPath))
			{
				throw new FileNotFoundException();
			}

			JObject jObj = JObject.Parse(File.ReadAllText(configPath));

			// populate tabs
			foreach (var tab in jObj["configTabs"])
			{
				ConfigTab configTab = new ConfigTab();
				configTab.Name = (string)tab["name"];
				configTab.Description = (string)tab["description"];
				configTab.FootNote = (string)tab["footnote"];

				// populate flags
				foreach (var flag in tab["flags"])
				{
					ConfigFlag configFlag = new ConfigFlag();
					configFlag.Name = (string)flag["name"];
					configFlag.Path = (string)flag["path"];
					foreach (var value in flag["values"])
					{
						configFlag.Values.Add((string)value);
					}
					configFlag.DefaultValue = (int)flag["defaultValue"];

					configTab.Flags.Add(configFlag);
				}

				modConfig.Tabs.Add(configTab);
			}

			return modConfig;
		}
	}
}
