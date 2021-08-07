using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigUtility
{
	[Serializable]
	public class ModConfig
	{
		public List<ConfigTab> Tabs;

		public ModConfig()
		{
			Tabs = new List<ConfigTab>();
		}
	}

	[Serializable]
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

	[Serializable]
	public class ConfigFlag
	{
		public string Name;
		public string Path;
		public string[] Values;
		public int DefaultValue;
		public int SavedValue;
	}
}
