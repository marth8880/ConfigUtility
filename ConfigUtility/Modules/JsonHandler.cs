using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigUtility
{
	public static class JsonHandler
	{
		const string DEFAULT_USER_CONFIG_LUA_TABLE_NAME = "gModConfig";
		const string DEFAULT_MUNGED_SCRIPT_FILE_NAME = "modconfig";

		public static ModConfig ParseConfigJson()
		{
			ModConfig modConfig = new ModConfig();
			string configPath = Directory.GetCurrentDirectory() + "\\config.json";
			if (!File.Exists(configPath))
			{
				throw new FileNotFoundException();
			}

			JObject jObj = JObject.Parse(File.ReadAllText(configPath));

			modConfig.MungedScriptFileName = (string)jObj["mungedScriptFileName"];
			modConfig.UserConfigLuaTableName = (string)jObj["userConfigLuaTableName"];

			if (modConfig.MungedScriptFileName == "")
			{
				MessageBox.Show(string.Format("config.json: Invalid or empty mungedScriptFileName was specified. Defaulting to '{0}'.", DEFAULT_MUNGED_SCRIPT_FILE_NAME), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				modConfig.MungedScriptFileName = DEFAULT_MUNGED_SCRIPT_FILE_NAME;
			}

			if (modConfig.UserConfigLuaTableName == "" || modConfig.UserConfigLuaTableName == "_G")
			{
				MessageBox.Show(string.Format("config.json: Invalid or empty userConfigLuaTableName was specified. Defaulting to '{0}'.", DEFAULT_USER_CONFIG_LUA_TABLE_NAME), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				modConfig.UserConfigLuaTableName = DEFAULT_USER_CONFIG_LUA_TABLE_NAME;
			}

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
					configFlag.Values = flag["values"].ToObject<string[]>();
					configFlag.DefaultValue = (int)flag["defaultValue"];

					configTab.Flags.Add(configFlag);
				}

				modConfig.Tabs.Add(configTab);
			}

			return modConfig;
		}
	}
}
