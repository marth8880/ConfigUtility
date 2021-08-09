
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigUtilityForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btn_SaveChanges = new System.Windows.Forms.Button();
			this.btn_ResetToDefaults = new System.Windows.Forms.Button();
			this.btn_About = new System.Windows.Forms.Button();
			this.toolTips = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(8, 7);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(505, 237);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new System.Drawing.Size(497, 209);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btn_SaveChanges
			// 
			this.btn_SaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_SaveChanges.Enabled = false;
			this.btn_SaveChanges.Location = new System.Drawing.Point(398, 250);
			this.btn_SaveChanges.Margin = new System.Windows.Forms.Padding(2);
			this.btn_SaveChanges.Name = "btn_SaveChanges";
			this.btn_SaveChanges.Size = new System.Drawing.Size(117, 25);
			this.btn_SaveChanges.TabIndex = 1;
			this.btn_SaveChanges.Text = "&Save Changes";
			this.btn_SaveChanges.UseVisualStyleBackColor = true;
			this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
			// 
			// btn_ResetToDefaults
			// 
			this.btn_ResetToDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_ResetToDefaults.AutoSize = true;
			this.btn_ResetToDefaults.Location = new System.Drawing.Point(285, 250);
			this.btn_ResetToDefaults.Margin = new System.Windows.Forms.Padding(2);
			this.btn_ResetToDefaults.Name = "btn_ResetToDefaults";
			this.btn_ResetToDefaults.Size = new System.Drawing.Size(109, 25);
			this.btn_ResetToDefaults.TabIndex = 2;
			this.btn_ResetToDefaults.Text = "&Reset to Defaults";
			this.btn_ResetToDefaults.UseVisualStyleBackColor = true;
			this.btn_ResetToDefaults.Click += new System.EventHandler(this.btn_ResetToDefaults_Click);
			// 
			// btn_About
			// 
			this.btn_About.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_About.Location = new System.Drawing.Point(11, 250);
			this.btn_About.Margin = new System.Windows.Forms.Padding(2);
			this.btn_About.Name = "btn_About";
			this.btn_About.Size = new System.Drawing.Size(78, 25);
			this.btn_About.TabIndex = 3;
			this.btn_About.Text = "&About";
			this.btn_About.UseVisualStyleBackColor = true;
			this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
			// 
			// ConfigUtilityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 286);
			this.Controls.Add(this.btn_About);
			this.Controls.Add(this.btn_ResetToDefaults);
			this.Controls.Add(this.btn_SaveChanges);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(540, 318);
			this.Name = "ConfigUtilityForm";
			this.Text = "Configuration Utility";
			this.Load += new System.EventHandler(this.ConfigUtilityForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btn_SaveChanges;
		private System.Windows.Forms.Button btn_ResetToDefaults;
		private System.Windows.Forms.Button btn_About;
		private System.Windows.Forms.ToolTip toolTips;
	}
}

