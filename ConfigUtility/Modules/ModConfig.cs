using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigUtility
{
	public class ModConfig
	{
		public List<ConfigTab> Tabs;
		public string MungedScriptFileName;

		public ModConfig()
		{
			Tabs = new List<ConfigTab>();
		}
	}

	public class ConfigTab
	{
		public string Name;
		public List<ConfigFlag> Flags;
		public string Description;
		public string FootNote;

		public ConfigTab()
		{
			Flags = new List<ConfigFlag>();
		}
	}

	public class ConfigFlag
	{
		public string Name;
		public string Path;
		public string[] Values;
		public int DefaultValue;
	}
}
