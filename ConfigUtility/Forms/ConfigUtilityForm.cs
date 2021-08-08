using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace ConfigUtility
{
	public partial class ConfigUtilityForm : Form
	{
		public const string CONFIG_SAVE_FILE_NAME = "ConfigUtility.dat";

		public ModConfig modConfig = new ModConfig();
		public ModConfigContainer modConfigContainer = new ModConfigContainer();
		List<ComboBox> comboBoxes = new List<ComboBox>();

		public ConfigUtilityForm()
		{
			InitializeComponent();
		}

		private void ConfigUtilityForm_Load(object sender, EventArgs e)
		{
			// Load any existing config
			bool configSaved = LoadUserConfig();
			modConfig = JsonHandler.ParseConfigJson();

			// Generate the tab pages
			foreach (ConfigTab configTab in modConfig.Tabs)
			{
				TabPage tabPage = new TabPage();

				ConfigTabControl configTabControl = new ConfigTabControl();
				configTabControl.Dock = DockStyle.Fill;

				// Gather and set the UI controls
				Label descriptionLabel = (Label)configTabControl.Controls.Find("lbl_Description", true)[0];
				Label footNoteLabel = (Label)configTabControl.Controls.Find("lbl_FootNote", true)[0];
				FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)configTabControl.Controls.Find("flow_Flags", true)[0];

				descriptionLabel.Text = configTab.Description;
				footNoteLabel.Text = configTab.FootNote;

				// Construct the tab page control (this is very dumb)
				tabPage.Controls.Add(configTabControl);
				tabPage.Location = new Point(4, 34);
				tabPage.Name = "tabPage_" + configTab.Name;
				tabPage.Padding = tabPage1.Padding;
				tabPage.Size = tabPage1.Size;
				tabPage.TabIndex = 0;
				tabPage.Text = configTab.Name;
				tabPage.UseVisualStyleBackColor = true;

				// Generate the flag dropdowns
				foreach (ConfigFlag configFlag in configTab.Flags)
				{
					ConfigFlagControl configFlagControl = new ConfigFlagControl();
					configFlagControl.Size = new Size(tabPage.Size.Width - 50, configFlagControl.Size.Height);

					// Gather and set the UI controls
					Label flagNameLabel = (Label)configFlagControl.Controls.Find("lbl_FlagName", true)[0];
					ComboBox flagValueCombo = (ComboBox)configFlagControl.Controls.Find("cmb_FlagValue", true)[0];

					flagNameLabel.Text = configFlag.Name;
					flagValueCombo.Items.Clear();
					flagValueCombo.Items.AddRange(configFlag.Values);
					flagValueCombo.Tag = configFlag;

					// Ensure the key exists in our local user config dictionary
					if (!modConfigContainer.UserConfig.ContainsKey(configFlag.Path))
						modConfigContainer.UserConfig.Add(configFlag.Path, configFlag.DefaultValue);

					// This needs to happen BEFORE adding the event handler
					flagValueCombo.SelectedIndex = modConfigContainer.UserConfig[configFlag.Path];

					// Whenever the combobox selection changes, either programmatically or by the user
					flagValueCombo.SelectedIndexChanged += delegate (object sender, EventArgs e)
					{
						ComboBox comboBox = (ComboBox)sender;
						ConfigFlag senderConfigFlag = (ConfigFlag)comboBox.Tag;
						int oldValue = -1;
						if (modConfigContainer.UserConfig.ContainsKey(senderConfigFlag.Path))
						{
							oldValue = modConfigContainer.UserConfig[senderConfigFlag.Path];
							modConfigContainer.UserConfig[senderConfigFlag.Path] = comboBox.SelectedIndex;
						}
						else
						{
							modConfigContainer.UserConfig.Add(senderConfigFlag.Path, comboBox.SelectedIndex);
						}
						
						ConfigIsDirty();
						Debug.WriteLine(string.Format("Selection index for {0} changed from {1} to {2}", senderConfigFlag.Name, oldValue, comboBox.SelectedIndex));
					};

					// Calculate new size and location of combobox
					int extraWidth = 25;	// need to account for the combobox arrow
					int newWidth = DropDownWidth(flagValueCombo) + extraWidth;
					int widthDifference = 0, newLocationX = 0;
					if (newWidth > flagValueCombo.MinimumSize.Width)
					{
						widthDifference = newWidth - flagValueCombo.MinimumSize.Width;
						newLocationX = flagValueCombo.Location.X - widthDifference;
						flagValueCombo.Location = new Point(newLocationX, flagValueCombo.Location.Y);
						flagValueCombo.Size = new Size(newWidth, flagValueCombo.Size.Height);
					}
					flagValueCombo.DropDownWidth = newWidth;
					comboBoxes.Add(flagValueCombo);

					flowLayoutPanel.Controls.Add(configFlagControl);
				}

				tabControl1.Controls.Add(tabPage);
			}

			// Remove the template tab page
			tabControl1.Controls.Remove(tabPage1);
		}

		private void btn_SaveChanges_Click(object sender, EventArgs e)
		{
			SaveUserConfig();
			btn_SaveChanges.Enabled = false;
		}

		private void btn_ResetToDefaults_Click(object sender, EventArgs e)
		{
			ResetToDefaults();
		}

		// Adapted from https://stackoverflow.com/a/4842576
		int DropDownWidth(ComboBox myCombo)
		{
			int maxWidth = 0, temp = 0;
			foreach (var obj in myCombo.Items)
			{
				temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
				if (temp > maxWidth)
				{
					maxWidth = temp;
				}
			}
			return maxWidth;
		}

		void ConfigIsDirty()
		{
			btn_SaveChanges.Enabled = true;
		}

		public void SerializeData(string filePath)
		{
			ModConfigContainer saveData = modConfigContainer;
			saveData.FileVersion = Properties.Settings.Default.Info_SaveFileVersion;

			// Attempt to save the binary file
			FileStream fs = new FileStream(filePath,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None);
			try
			{
				// Serialize and save the data
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, saveData);
			}
			catch (SerializationException e)
			{
				Trace.WriteLine("Failed to serialize data. Reason: " + e.Message);
				throw;
			}
			catch (IOException e)
			{
				Trace.WriteLine(string.Format("Failed to write to file path: \"{0}\". Reason: {1}", filePath, e.Message));
				throw;
			}
			finally
			{
				fs.Close();
			}
		}

		public ModConfigContainer DeserializeData(string filePath)
		{
			ModConfigContainer data = null;

			// Attempt to read the binary file
			FileStream fs = new FileStream(filePath, FileMode.Open);
			try
			{
				IFormatter formatter = new BinaryFormatter();

				// Deserialize and store the data
				data = (ModConfigContainer)formatter.Deserialize(fs);

				// Ensure that the save file is compatible with this version of the application
				if (data.FileVersion < Properties.Settings.Default.Info_SaveFileVersion)
				{
					MessageBox.Show(string.Format("Save file {0} is incompatible with this version of the application.", filePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (SerializationException e)
			{
				Trace.WriteLine("Failed to deserialize. Reason: " + e.Message);
				throw;
			}
			catch (IOException e)
			{
				MessageBox.Show(string.Format("Failed to read from file path: \"{0}\". Reason: {1}", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				fs.Close();
			}

			return data;
		}

		void SaveUserConfig()
		{
			SerializeData(Directory.GetCurrentDirectory() + "\\" + CONFIG_SAVE_FILE_NAME);
			MungeChanges();
		}

		bool LoadUserConfig()
		{
			string filePath = Directory.GetCurrentDirectory() + "\\" + CONFIG_SAVE_FILE_NAME;

			if (File.Exists(filePath))
			{
				ModConfigContainer data = DeserializeData(filePath);
				if (data == null)
				{
					return false;
				}
				modConfigContainer = data;

				return true;
			}

			return false;
		}

		void MungeChanges()
		{
			string luaScriptFileName = modConfig.MungedScriptFileName + ".lua";
			string luaScriptPath = Directory.GetCurrentDirectory() + "\\" + luaScriptFileName;

			// Create the Lua script
			FileStream fs = new FileStream(luaScriptPath,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None);
			StreamWriter streamWriter = new StreamWriter(fs);

			// Write the config table to the script
			streamWriter.WriteLine(modConfig.UserConfigLuaTableName + " = {");
			foreach (KeyValuePair<string, int> flag in modConfigContainer.UserConfig)
			{
				string line = flag.Key + " = " + flag.Value + ",";
				streamWriter.WriteLine(line);
			}
			streamWriter.WriteLine("}");
			streamWriter.Close();
			fs.Close();

			// Munge!
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = Directory.GetCurrentDirectory() + "\\ScriptMunge.exe";
			processStartInfo.Arguments = string.Format("-inputfile {0} -continue -platform PC -verbose -debug -outputdir {1}",
				luaScriptFileName,
				Directory.GetCurrentDirectory());
			processStartInfo.CreateNoWindow = true;
			processStartInfo.RedirectStandardOutput = true;

			Process process = new Process();
			process.StartInfo = processStartInfo;
			process.EnableRaisingEvents = true;
			process.OutputDataReceived += (sender, e) =>
			{
				Trace.WriteLine(e.Data);
			};
			process.Exited += (sender, e) =>
			{
				Trace.WriteLine("ScriptMunge finished with exit code " + process.ExitCode + ", cleaning up");
				File.Delete(luaScriptPath);
				DisableWorkingStatus();
			};

			EnableWorkingStatus();
			process.Start();
		}

		void EnableWorkingStatus()
		{
			EnableWorkingStatus_Proc();
		}

		delegate void EnableWorkingStatusCallback();

		// WARNING: Don't call this directly, please call `EnableWorkingStatus` instead.
		void EnableWorkingStatus_Proc()
		{
			if (InvokeRequired)
			{
				EnableWorkingStatusCallback cb = new EnableWorkingStatusCallback(EnableWorkingStatus_Proc);
				BeginInvoke(cb);
			}
			else
			{
				tabControl1.Enabled = false;
				btn_ResetToDefaults.Enabled = false;
				Cursor.Current = Cursors.WaitCursor;
				Application.DoEvents();
			}
		}

		void DisableWorkingStatus()
		{
			DisableWorkingStatus_Proc();
		}

		delegate void DisableWorkingStatusCallback();

		// WARNING: Don't call this directly, please call `DisableWorkingStatus` instead.
		void DisableWorkingStatus_Proc()
		{
			if (InvokeRequired)
			{
				DisableWorkingStatusCallback cb = new DisableWorkingStatusCallback(DisableWorkingStatus_Proc);
				BeginInvoke(cb);
			}
			else
			{
				tabControl1.Enabled = true;
				btn_ResetToDefaults.Enabled = true;
				Cursor.Current = Cursors.Default;
				Application.DoEvents();
			}
		}

		void ResetToDefaults()
		{
			foreach (ComboBox comboBox in comboBoxes)
			{
				ConfigFlag configFlag = (ConfigFlag)comboBox.Tag;
				comboBox.SelectedIndex = configFlag.DefaultValue;
			}

			ConfigIsDirty();
		}
	}
}
