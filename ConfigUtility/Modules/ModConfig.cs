using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Windows.Forms;

namespace ConfigUtility
{
	[DataContract]
	public class ModConfig
	{
		[DataMember]
		public double fileVersion;

		[DataMember]
		public List<ConfigTab> configTabs;
		[DataMember]
		public string mungedScriptFileName;
		[DataMember]
		public string userConfigLuaTableName;

		public ModConfig()
		{
			configTabs = new List<ConfigTab>();
		}

		public static ModConfig FromFile(string file)
		{
			ModConfig retVal = null;
			using (System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open))
			{
				DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ModConfig));
				retVal = (ModConfig)ser.ReadObject(fs);
			}
			return retVal;
		}

		public static string ToJson(ModConfig config)
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ModConfig));
			StringBuilder builder = new StringBuilder();
			MemoryStream ms = new MemoryStream();
			ser.WriteObject(ms, config);
			String retVal = System.Text.Encoding.Default.GetString(ms.ToArray());
			return retVal;
		}

		public void DefinitionError(string parentName, string valueType)
		{
			MessageBox.Show(string.Format("config.json: No or invalid '{0}' definition was found for '{1}'.", valueType, parentName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(4);
		}

		public void ValueError(string parentName, string flagName)
		{
			MessageBox.Show(string.Format("config.json: Invalid '{0}' value specified for '{1}'.", flagName, parentName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(4);
		}

		public void GeneralError(string message)
		{
			MessageBox.Show(string.Format("config.json: {0}", message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(4);
		}
	}

	[DataContract]
	public class ConfigTab
	{
		[DataMember]
		public string name;
		[DataMember]
		public List<ConfigFlag> flags;
		[DataMember]
		public string description;
		[DataMember]
		public string footnote;

		public ConfigTab()
		{
			flags = new List<ConfigFlag>();
		}
	}

	[DataContract]
	public class ConfigFlag
	{
		[DataMember]
		public string name;
		[DataMember]
		public string path;
		[DataMember]
		public string toolTipCaption = "";
		[DataMember]
		public string[] values;
		[DataMember]
		public int defaultValue;
	}
}
