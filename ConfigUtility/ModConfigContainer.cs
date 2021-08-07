using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConfigUtility
{
	[Serializable]
	public class ModConfigContainer
	{
		public Dictionary<string, int> UserConfig;
		public int FileVersion;

		public ModConfigContainer()
		{
			UserConfig = new Dictionary<string, int>();
			FileVersion = 0;
		}
	}
}
