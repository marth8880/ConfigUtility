using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConfigUtility
{
	[Serializable]
	public class ModConfigContainer
	{
		public ModConfig ConfigData;
		public int FileVersion;

		public ModConfigContainer()
		{
			ConfigData = new ModConfig();
			FileVersion = 0;
		}
	}
}
