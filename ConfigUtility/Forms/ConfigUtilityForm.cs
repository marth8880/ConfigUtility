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
		public const string PROJECT_URL = "https://github.com/marth8880/ConfigUtility";

		public ModConfig modConfig = new ModConfig();

		ModConfigContainer modConfigContainer = new ModConfigContainer();
		List<ComboBox> comboBoxes = new List<ComboBox>();

		public ConfigUtilityForm()
		{
			InitializeComponent();
		}

		private void ConfigUtilityForm_Load(object sender, EventArgs e)
		{
			SetToolTips();

			// Load any existing config
			bool configSaved = LoadUserConfig();
			string configPath = Directory.GetCurrentDirectory() + "\\config.json";
			modConfig = ModConfig.FromFile(configPath);

			// Generate the tab pages
			foreach (ConfigTab configTab in modConfig.configTabs)
			{
				TabPage tabPage = new TabPage();

				ConfigTabControl configTabControl = new ConfigTabControl();
				configTabControl.Dock = DockStyle.Fill;

				// Gather and set the UI controls
				Label descriptionLabel = (Label)configTabControl.Controls.Find("lbl_Description", true)[0];
				Label footNoteLabel = (Label)configTabControl.Controls.Find("lbl_FootNote", true)[0];
				FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)configTabControl.Controls.Find("flow_Flags", true)[0];

				descriptionLabel.Text = configTab.description;
				footNoteLabel.Text = configTab.footNote;

				// Construct the tab page control (this is very dumb)
				tabPage.Controls.Add(configTabControl);
				tabPage.Location = new Point(4, 34);
				tabPage.Name = "tabPage_" + configTab.name;
				tabPage.Padding = tabPage1.Padding;
				tabPage.Size = tabPage1.Size;
				tabPage.TabIndex = 0;
				tabPage.Text = configTab.name;
				tabPage.UseVisualStyleBackColor = true;

				// Generate the flag dropdowns
				foreach (ConfigFlag configFlag in configTab.flags)
				{
					ConfigFlagControl configFlagControl = new ConfigFlagControl();
					configFlagControl.Size = new Size(tabPage.Size.Width - 50, configFlagControl.Size.Height);

					// Gather and set the UI controls
					Label flagNameLabel = (Label)configFlagControl.Controls.Find("lbl_FlagName", true)[0];
					ComboBox flagValueCombo = (ComboBox)configFlagControl.Controls.Find("cmb_FlagValue", true)[0];

					flagNameLabel.Text = configFlag.name;
					flagValueCombo.Items.Clear();
					flagValueCombo.Items.AddRange(configFlag.values);
					flagValueCombo.Tag = configFlag;

					if (configFlag.toolTipCaption != "")
					{
						toolTips.SetToolTip(flagNameLabel, configFlag.toolTipCaption);
						toolTips.SetToolTip(flagValueCombo, configFlag.toolTipCaption);
					}

					// Ensure the key exists in our local user config dictionary
					if (!modConfigContainer.UserConfig.ContainsKey(configFlag.path))
						modConfigContainer.UserConfig.Add(configFlag.path, configFlag.defaultValue);

					// Ensure a higher value than possible wasn't set in the saved config
					// This needs to happen BEFORE adding the event handler otherwise it'll raise it
					if (modConfigContainer.UserConfig[configFlag.path] >= configFlag.values.Length)
						flagValueCombo.SelectedIndex = configFlag.defaultValue;
					else
						flagValueCombo.SelectedIndex = modConfigContainer.UserConfig[configFlag.path];

					// Whenever the combobox selection changes, either programmatically or by the user
					flagValueCombo.SelectedIndexChanged += delegate (object sender2, EventArgs eArgs)
					{
						ComboBox comboBox = (ComboBox)sender2;
						ConfigFlag senderConfigFlag = (ConfigFlag)comboBox.Tag;
						int oldValue = -1;
						if (modConfigContainer.UserConfig.ContainsKey(senderConfigFlag.path))
						{
							oldValue = modConfigContainer.UserConfig[senderConfigFlag.path];
							modConfigContainer.UserConfig[senderConfigFlag.path] = comboBox.SelectedIndex;
						}
						else
						{
							modConfigContainer.UserConfig.Add(senderConfigFlag.path, comboBox.SelectedIndex);
						}
						
						ConfigIsDirty();
						Debug.WriteLine(string.Format("Selection index for {0} changed from {1} to {2}", senderConfigFlag.name, oldValue, comboBox.SelectedIndex));
					};

					/// TODO: The width of all ComboBoxes in a tab page should be based on the widest one 
					///  https://github.com/marth8880/ConfigUtility/issues/4
					// Calculate new size and location of combobox
					//int extraWidth = 25;	// need to account for the combobox arrow
					//int newWidth = GetPreferredDropDownWidth(flagValueCombo) + extraWidth;
					//int widthDifference = 0, newLocationX = 0;
					//if (newWidth > flagValueCombo.MinimumSize.Width)
					//{
					//	widthDifference = newWidth - flagValueCombo.MinimumSize.Width;
					//	newLocationX = flagValueCombo.Location.X - widthDifference;
					//	flagValueCombo.Location = new Point(newLocationX, flagValueCombo.Location.Y);
					//	flagValueCombo.Size = new Size(newWidth, flagValueCombo.Size.Height);
					//}
					//flagValueCombo.DropDownWidth = newWidth;
					flagValueCombo.DropDownWidth = GetPreferredDropDownWidth(flagValueCombo);
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

		private void btn_About_Click(object sender, EventArgs e)
		{
			Forms.AboutBox aboutBox = new Forms.AboutBox();
			aboutBox.ShowDialog();
		}

		// Adapted from https://stackoverflow.com/a/4842576
		int GetPreferredDropDownWidth(ComboBox myCombo)
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

			FileStream fs = null;
			try
			{
				// Attempt to save the text file
				fs = new FileStream(filePath,
				   FileMode.Create,
				   FileAccess.Write,
				   FileShare.None);

				// Serialize the data as text and save it
				StreamWriter streamWriter = new StreamWriter(fs);

				// First line must always be the file version
				streamWriter.WriteLine(string.Format("{0}={1}", "FileVersion", saveData.FileVersion));
				foreach (KeyValuePair<string, int> flag in saveData.UserConfig)
				{
					streamWriter.WriteLine(string.Format("{0}={1}", flag.Key, flag.Value));
				}
				streamWriter.Close();
				fs.Close();
			}
			catch (IndexOutOfRangeException e)
			{
				MessageBox.Show(string.Format("Failed to write to file path: \"{0}\". Reason: {1}", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (IOException e)
			{
				MessageBox.Show(string.Format("Failed to write to file path: \"{0}\". Reason: {1}", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (UnauthorizedAccessException e)
			{
				MessageBox.Show(string.Format("Failed to write to file path: \"{0}\". Reason: {1} (File is probably read-only).", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
				}
			}
		}

		public ModConfigContainer DeserializeData(string filePath)
		{
			ModConfigContainer data = null;

			// Attempt to read the binary file
			FileStream fs = null;
			try
			{
				fs = new FileStream(filePath, FileMode.Open);
				data = new ModConfigContainer();

				// Deserialize and store the data
				StreamReader streamReader = new StreamReader(fs);
				int lineNum = 0;
				string line;
				while ((line = streamReader.ReadLine()) != null)
				{
					string[] parsedLine = line.Split('=');

					if (lineNum == 0)
					{
						data.FileVersion = Convert.ToInt32(parsedLine[1]);
					}
					else
					{
						data.UserConfig.Add(parsedLine[0], Convert.ToInt32(parsedLine[1]));
					}

					lineNum++;
				}
				streamReader.Close();
				fs.Close();

				// Ensure that the save file is compatible with this version of the application
				if (data.FileVersion < Properties.Settings.Default.Info_SaveFileVersion)
				{
					MessageBox.Show(string.Format("Save file {0} is incompatible with this version of the application.", filePath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (IndexOutOfRangeException e)
			{
				MessageBox.Show(string.Format("Failed to read from file path: \"{0}\". Reason: {1} \nUsing default settings instead.", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (FormatException e)
			{
				MessageBox.Show(string.Format("Failed to read from file path: \"{0}\". Reason: {1} \nUsing default settings instead.", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (IOException e)
			{
				MessageBox.Show(string.Format("Failed to read from file path: \"{0}\". Reason: {1} \nUsing default settings instead.", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (UnauthorizedAccessException e)
			{
				MessageBox.Show(string.Format("Failed to read from file path: \"{0}\". Reason: {1} (File is probably read-only). \nUsing default settings instead.", filePath, e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
				}
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
			string luaScriptFileName = modConfig.mungedScriptFileName + ".lua";
			string luaScriptPath = Directory.GetCurrentDirectory() + "\\" + luaScriptFileName;

			// Create the Lua script
			FileStream fs = new FileStream(luaScriptPath,
				FileMode.Create,
				FileAccess.Write,
				FileShare.None);
			StreamWriter streamWriter = new StreamWriter(fs);

			// Write the config table to the script
			streamWriter.WriteLine(modConfig.userConfigLuaTableName + " = {");
			foreach (KeyValuePair<string, int> flag in modConfigContainer.UserConfig)
			{
				string line = flag.Key + " = " + flag.Value + ",";
				streamWriter.WriteLine(line);
			}
			streamWriter.WriteLine("}");
			streamWriter.Close();
			fs.Close();

			// Munge!
			List<string> createdFiles = EnsureScriptMungeExists();
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = Directory.GetCurrentDirectory() + "\\ScriptMunge.exe";
			processStartInfo.Arguments = string.Format("-inputfile {0} -continue -platform PC -verbose -debug -outputdir {1}",
				luaScriptFileName,
				Directory.GetCurrentDirectory());
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
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
				foreach(string file in createdFiles)
					File.Delete(file);
				
				string logFile = Directory.GetCurrentDirectory() + "\\ScriptMunge.log";
				if (File.Exists(logFile))
					File.Delete(logFile);

				DisableWorkingStatus();
			};

			EnableWorkingStatus();
			process.Start();
		}

		/// <summary>
		/// Ensures that scriptmunge and luac exist.
		/// Returns an array of the files created so that the caller can know which files to delete; we wouldn't want to delete 
		/// a file that was already there.
		/// </summary>
		private List<String> EnsureScriptMungeExists()
		{
			List<string> retVal = new List<string>();

			string scriptMunge = Directory.GetCurrentDirectory() + "\\ScriptMunge.exe";
			string luac = Directory.GetCurrentDirectory() + "\\luac.exe";
			if (!File.Exists(scriptMunge))
			{
				string scriptMungeResourceName = "ConfigUtility.ScriptMunge.exe";
				SaveResourceToFile(scriptMungeResourceName, scriptMunge);
				retVal.Add(scriptMunge);
			}
			if (!File.Exists(luac))
			{
				string luacResourceName = "ConfigUtility.luac.exe";
				SaveResourceToFile(luacResourceName, luac);
				retVal.Add(luac);
			}
			return retVal;
		}

		private void SaveResourceToFile(string resourceName, string localFileName)
		{
			System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
			string[] resourceNames = ass.GetManifestResourceNames(); //uncomment to inspect embedded resources 
			Stream stream = ass.GetManifestResourceStream(resourceName);
			byte[] buff = new byte[stream.Length];
			stream.Seek(0, SeekOrigin.Begin);
			stream.Read(buff, 0, (int)stream.Length);
			File.WriteAllBytes(localFileName, buff);
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
				btn_About.Enabled = false;

				Application.UseWaitCursor = true;
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
				btn_About.Enabled = true;

				Application.UseWaitCursor = false;
				Cursor.Current = Cursors.Default;	// without this the cursor doesn't reset until it's moved
				Application.DoEvents();
			}
		}

		void ResetToDefaults()
		{
			foreach (ComboBox comboBox in comboBoxes)
			{
				ConfigFlag configFlag = (ConfigFlag)comboBox.Tag;
				comboBox.SelectedIndex = configFlag.defaultValue;
			}

			ConfigIsDirty();
		}

		void SetToolTips()
		{
			toolTips.AutoPopDelay = Properties.Settings.Default.TooltipPopDelay;
			toolTips.SetToolTip(btn_SaveChanges, "Save setting changes to disk.");
			toolTips.SetToolTip(btn_ResetToDefaults, "Reset settings to their default values.");
			toolTips.SetToolTip(btn_About, "See more info about the application.");
		}
	}
}
