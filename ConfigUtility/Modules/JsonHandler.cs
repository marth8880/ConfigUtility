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

			if (jObj["mungedScriptFileName"] == null || (string)jObj["mungedScriptFileName"] == "")
			{
				MessageBox.Show(string.Format("config.json: Invalid or empty 'mungedScriptFileName' was specified or definition is missing. Defaulting to '{0}'.", DEFAULT_MUNGED_SCRIPT_FILE_NAME), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				modConfig.MungedScriptFileName = DEFAULT_MUNGED_SCRIPT_FILE_NAME;
			}
			else
			{
				modConfig.MungedScriptFileName = (string)jObj["mungedScriptFileName"];
			}

			if (jObj["userConfigLuaTableName"] == null || (string)jObj["userConfigLuaTableName"] == "" || (string)jObj["userConfigLuaTableName"] == "_G")
			{
				MessageBox.Show(string.Format("config.json: Invalid or empty 'userConfigLuaTableName' was specified or definition is missing. Defaulting to '{0}'.", DEFAULT_USER_CONFIG_LUA_TABLE_NAME), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				modConfig.UserConfigLuaTableName = DEFAULT_USER_CONFIG_LUA_TABLE_NAME;
			}
			else
			{
				modConfig.UserConfigLuaTableName = (string)jObj["userConfigLuaTableName"];
			}

			if (jObj["configTabs"] == null)
			{
				MessageBox.Show("config.json: No tabs were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(4);
			}

			// populate tabs
			foreach (var tab in jObj["configTabs"])
			{
				if (tab["name"] == null)
					DefinitionError("new tab", "name");
				if (tab["description"] == null)
					DefinitionError((string)tab["name"], "description");
				if (tab["footnote"] == null)
					DefinitionError((string)tab["name"], "footnote");
				if (tab["flags"] == null)
					DefinitionError((string)tab["name"], "flags");

				ConfigTab configTab = new ConfigTab();
				configTab.Name = (string)tab["name"];
				configTab.Description = (string)tab["description"];
				configTab.FootNote = (string)tab["footnote"];

				// populate flags
				foreach (var flag in tab["flags"])
				{
					if (flag["name"] == null)
						DefinitionError("new flag", "name");
					if (flag["path"] == null)
						DefinitionError((string)flag["name"], "path");
					if (flag["values"] == null)
						DefinitionError((string)flag["name"], "values");
					if (flag["defaultValue"] == null)
						DefinitionError((string)flag["name"], "defaultValue");

					ConfigFlag configFlag = new ConfigFlag();
					configFlag.Name = (string)flag["name"];
					configFlag.Path = (string)flag["path"];
					configFlag.Values = flag["values"].ToObject<string[]>();
					configFlag.DefaultValue = (int)flag["defaultValue"];

					if (configFlag.Values.Length == 0)
					{
						ValueError(configFlag.Name, "values");
					}

					configTab.Flags.Add(configFlag);
				}

				if (configTab.Flags.Count == 0)
				{
					ValueError(configTab.Name, "flags");
				}

				modConfig.Tabs.Add(configTab);
			}

			if (modConfig.Tabs.Count == 0)
			{
				MessageBox.Show("config.json: No tabs were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(4);
			}

			return modConfig;
		}

		static void DefinitionError(string parentName, string valueType)
		{
			MessageBox.Show(string.Format("config.json: No '{0}' definition was found for '{1}'.", valueType, parentName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(4);
		}

		static void ValueError(string parentName, string flagName)
		{
			MessageBox.Show(string.Format("config.json: Invalid '{0}' value specified for '{1}'.", flagName, parentName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(4);
		}
	}
}
