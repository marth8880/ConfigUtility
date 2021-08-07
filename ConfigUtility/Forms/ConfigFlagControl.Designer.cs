
namespace ConfigUtility
{
	partial class ConfigFlagControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmb_FlagValue = new System.Windows.Forms.ComboBox();
			this.lbl_FlagName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cmb_FlagValue
			// 
			this.cmb_FlagValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmb_FlagValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_FlagValue.FormattingEnabled = true;
			this.cmb_FlagValue.Items.AddRange(new object[] {
            "Disabled",
            "Enabled"});
			this.cmb_FlagValue.Location = new System.Drawing.Point(484, 6);
			this.cmb_FlagValue.MinimumSize = new System.Drawing.Size(182, 0);
			this.cmb_FlagValue.Name = "cmb_FlagValue";
			this.cmb_FlagValue.Size = new System.Drawing.Size(182, 33);
			this.cmb_FlagValue.TabIndex = 4;
			this.cmb_FlagValue.Tag = "optionValue";
			// 
			// lbl_FlagName
			// 
			this.lbl_FlagName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_FlagName.AutoSize = true;
			this.lbl_FlagName.Location = new System.Drawing.Point(8, 9);
			this.lbl_FlagName.Name = "lbl_FlagName";
			this.lbl_FlagName.Size = new System.Drawing.Size(59, 25);
			this.lbl_FlagName.TabIndex = 3;
			this.lbl_FlagName.Tag = "optionName";
			this.lbl_FlagName.Text = "label3";
			// 
			// ConfigFlagControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmb_FlagValue);
			this.Controls.Add(this.lbl_FlagName);
			this.Name = "ConfigFlagControl";
			this.Size = new System.Drawing.Size(674, 45);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cmb_FlagValue;
		private System.Windows.Forms.Label lbl_FlagName;
	}
}
