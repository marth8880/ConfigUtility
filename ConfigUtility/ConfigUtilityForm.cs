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
				foreach (ConfigFlag configFlag in configTab.Flags)
				{
					// Create the new panel
					Panel newPanel = new Panel();
					newPanel.Location = pnl_Option.Location;
					newPanel.Size = pnl_Option.Size;

					// Create the new option label
					Label newLabel = new Label();
					newLabel.AutoSize = lbl_OptionName.AutoSize;
					newLabel.Anchor = lbl_OptionName.Anchor;
					newLabel.Location = lbl_OptionName.Location;
					newLabel.Text = configFlag.Name;
					newLabel.Parent = newPanel;

					ComboBox newComboBox = new ComboBox();
					newComboBox.Anchor = cmb_OptionValue.Anchor;
					newComboBox.DropDownStyle = cmb_OptionValue.DropDownStyle;
					newComboBox.FormattingEnabled = cmb_OptionValue.FormattingEnabled;
					newComboBox.Items.Clear();
					newComboBox.Items.AddRange(configFlag.Values);
					newComboBox.Location = cmb_OptionValue.Location;
					newComboBox.Size = cmb_OptionValue.Size;
					newComboBox.DropDownWidth = DropDownWidth(newComboBox);

					newPanel.Controls.Add(newLabel);
					newPanel.Controls.Add(newComboBox);
					flow_Options.Controls.Add(newPanel);
				}
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
