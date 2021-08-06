
namespace ConfigUtility
{
	partial class ConfigUtilityForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.flow_Options = new System.Windows.Forms.FlowLayoutPanel();
			this.pnl_Option = new System.Windows.Forms.Panel();
			this.cmb_OptionValue = new System.Windows.Forms.ComboBox();
			this.lbl_OptionName = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.flow_Options.SuspendLayout();
			this.pnl_Option.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(722, 395);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.flow_Options);
			this.tabPage1.Location = new System.Drawing.Point(4, 34);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(714, 357);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 326);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 25);
			this.label2.TabIndex = 1;
			this.label2.Text = "Footer text.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(279, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "This page contains some settings.";
			// 
			// flow_Options
			// 
			this.flow_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flow_Options.AutoScroll = true;
			this.flow_Options.Controls.Add(this.pnl_Option);
			this.flow_Options.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flow_Options.Location = new System.Drawing.Point(3, 45);
			this.flow_Options.Name = "flow_Options";
			this.flow_Options.Size = new System.Drawing.Size(708, 278);
			this.flow_Options.TabIndex = 0;
			this.flow_Options.WrapContents = false;
			// 
			// pnl_Option
			// 
			this.pnl_Option.Controls.Add(this.cmb_OptionValue);
			this.pnl_Option.Controls.Add(this.lbl_OptionName);
			this.pnl_Option.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_Option.Location = new System.Drawing.Point(3, 3);
			this.pnl_Option.MaximumSize = new System.Drawing.Size(0, 45);
			this.pnl_Option.MinimumSize = new System.Drawing.Size(674, 0);
			this.pnl_Option.Name = "pnl_Option";
			this.pnl_Option.Size = new System.Drawing.Size(674, 45);
			this.pnl_Option.TabIndex = 0;
			// 
			// cmb_OptionValue
			// 
			this.cmb_OptionValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmb_OptionValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb_OptionValue.FormattingEnabled = true;
			this.cmb_OptionValue.Items.AddRange(new object[] {
            "Disabled",
            "Enabled"});
			this.cmb_OptionValue.Location = new System.Drawing.Point(482, 6);
			this.cmb_OptionValue.Name = "cmb_OptionValue";
			this.cmb_OptionValue.Size = new System.Drawing.Size(182, 33);
			this.cmb_OptionValue.TabIndex = 1;
			this.cmb_OptionValue.Tag = "optionValue";
			// 
			// lbl_OptionName
			// 
			this.lbl_OptionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_OptionName.AutoSize = true;
			this.lbl_OptionName.Location = new System.Drawing.Point(6, 9);
			this.lbl_OptionName.Name = "lbl_OptionName";
			this.lbl_OptionName.Size = new System.Drawing.Size(59, 25);
			this.lbl_OptionName.TabIndex = 0;
			this.lbl_OptionName.Tag = "optionName";
			this.lbl_OptionName.Text = "label3";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(567, 413);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(167, 34);
			this.button1.TabIndex = 1;
			this.button1.Text = "Submit Changes";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// ConfigUtilityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 459);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tabControl1);
			this.MinimumSize = new System.Drawing.Size(768, 515);
			this.Name = "ConfigUtilityForm";
			this.Text = "Configuration Utility";
			this.Load += new System.EventHandler(this.ConfigUtilityForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.flow_Options.ResumeLayout(false);
			this.pnl_Option.ResumeLayout(false);
			this.pnl_Option.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flow_Options;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel pnl_Option;
		private System.Windows.Forms.ComboBox cmb_OptionValue;
		private System.Windows.Forms.Label lbl_OptionName;
	}
}

