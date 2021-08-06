using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigUtility
{
	class ModConfig
	{
		public List<ConfigTab> Tabs = new List<ConfigTab>(2)
		{
			new ConfigTab()
			{
				Name = "General",
				Header = "This page contains general settings that affect the mod and its appearance.",
				Footer = "Settings marked with * only affect offline &amp; single-player matches.",
				Options = new List<ConfigOption>()
				{
					new ConfigOption()
					{
						Name = "Toggle Mass Effect: Unification",
						Path = "cfg_MEUnificationEnabled",
						Values = new List<string>()
						{
							[0] = "Disabled",
							[1] = "Enabled"
						}
					},
					new ConfigOption()
					{
						Name = "Menu Interface Style",
						Path = "cfg_CustomGUIEnabled",
						Values = new List<string>()
						{
							[0] = "Star Wars Battlefront II",
							[1] = "Mass Effect: Unification",
							[2] = "Mass Effect 3"
						}
					},
					new ConfigOption()
					{
						Name = "Custom HUD",
						Path = "cfg_CustomHUD",
						Values = new List<string>()
						{
							[0] = "Disabled",
							[1] = "Enabled"
						}
					}
				}
			},
			new ConfigTab()
			{
				Name = "Gameplay",
				Header = "This page contains settings that affect the mod's gameplay.",
				Footer = "Settings marked with * only affect offline &amp; single-player matches.",
				Options = new List<ConfigOption>()
				{
					new ConfigOption()
					{
						Name = "Faction Combination",
						Path = "cfg_SideVar",
						Values = new List<string>()
						{
							[0] = "Random",
							[1] = "Systems Alliance vs. Heretic Geth",
							[2] = "Systems Alliance vs. Collectors",
							[3] = "Evolved Geth vs. Heretic Geth",
							[4] = "Evolved Geth vs. Collectors",
							[5] = "Systems Alliance vs. Reapers"
						}
					}
				}
			}
		};
	}

	class ConfigTab
	{
		public string Name;
		public List<ConfigOption> Options;
		public string Header;
		public string Footer;
	}

	class ConfigOption
	{
		public string Name;
		public string Path;
		public List<string> Values;
	}
}
