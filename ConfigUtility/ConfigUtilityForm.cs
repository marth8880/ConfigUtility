using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace ConfigUtility
{
	public partial class ConfigUtilityForm : Form
	{
		public ConfigUtilityForm()
		{
			InitializeComponent();
		}

		private void ConfigUtilityForm_Load(object sender, EventArgs e)
		{
			ModConfig modConfig = JsonHandler.ParseConfigJson();

			foreach (ConfigTab configTab in modConfig.Tabs)
			{
				TabPage tabPage = new TabPage();

				ConfigTabControl configTabControl = new ConfigTabControl();
				configTabControl.Dock = DockStyle.Fill;

				Label descriptionLabel = (Label)configTabControl.Controls.Find("lbl_Description", true)[0];
				Label footNoteLabel = (Label)configTabControl.Controls.Find("lbl_FootNote", true)[0];
				FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)configTabControl.Controls.Find("flow_Flags", true)[0];

				descriptionLabel.Text = configTab.Description;
				footNoteLabel.Text = configTab.FootNote;

				tabPage.Controls.Add(configTabControl);
				tabPage.Location = new Point(4, 34);
				tabPage.Name = "tabPage_" + configTab.Name;
				tabPage.Padding = tabPage1.Padding;
				tabPage.Size = tabPage1.Size;
				tabPage.TabIndex = 0;
				tabPage.Text = configTab.Name;
				tabPage.UseVisualStyleBackColor = true;

				foreach (ConfigFlag configFlag in configTab.Flags)
				{
					ConfigFlagControl configFlagControl = new ConfigFlagControl();
					//configFlagControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;

					Label flagNameLabel = (Label)configFlagControl.Controls.Find("lbl_FlagName", true)[0];
					ComboBox flagValueCombo = (ComboBox)configFlagControl.Controls.Find("cmb_FlagValue", true)[0];

					flagNameLabel.Text = configFlag.Name;
					flagValueCombo.Items.Clear();
					flagValueCombo.Items.AddRange(configFlag.Values);
					//flagValueCombo.Text = flagValueCombo.Items[configFlag.DefaultValue];

					// calculate new size and location of combobox
					int newWidth = DropDownWidth(flagValueCombo);
					int widthDifference = 0, newLocationX = 0;
					if (newWidth > flagValueCombo.MinimumSize.Width)
					{
						widthDifference = newWidth - flagValueCombo.MinimumSize.Width;
						newLocationX = flagValueCombo.Location.X - widthDifference;
						flagValueCombo.Location = new Point(newLocationX, flagValueCombo.Location.Y);
						flagValueCombo.Size = new Size(newWidth, flagValueCombo.Size.Height);
					}
					flagValueCombo.DropDownWidth = newWidth;

					flowLayoutPanel.Controls.Add(configFlagControl);
				}

				tabControl1.Controls.Add(tabPage);
			}
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
	}
}
