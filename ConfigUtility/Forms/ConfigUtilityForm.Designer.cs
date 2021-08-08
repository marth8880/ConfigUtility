
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
			this.btn_SaveChanges = new System.Windows.Forms.Button();
			this.btn_ResetToDefaults = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
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
			this.tabPage1.Location = new System.Drawing.Point(4, 34);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(714, 357);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btn_SaveChanges
			// 
			this.btn_SaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_SaveChanges.Enabled = false;
			this.btn_SaveChanges.Location = new System.Drawing.Point(567, 413);
			this.btn_SaveChanges.Name = "btn_SaveChanges";
			this.btn_SaveChanges.Size = new System.Drawing.Size(167, 34);
			this.btn_SaveChanges.TabIndex = 1;
			this.btn_SaveChanges.Text = "Save Changes";
			this.btn_SaveChanges.UseVisualStyleBackColor = true;
			this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
			// 
			// btn_ResetToDefaults
			// 
			this.btn_ResetToDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btn_ResetToDefaults.AutoSize = true;
			this.btn_ResetToDefaults.Location = new System.Drawing.Point(12, 412);
			this.btn_ResetToDefaults.Name = "btn_ResetToDefaults";
			this.btn_ResetToDefaults.Size = new System.Drawing.Size(156, 35);
			this.btn_ResetToDefaults.TabIndex = 2;
			this.btn_ResetToDefaults.Text = "Reset to Defaults";
			this.btn_ResetToDefaults.UseVisualStyleBackColor = true;
			this.btn_ResetToDefaults.Click += new System.EventHandler(this.btn_ResetToDefaults_Click);
			// 
			// ConfigUtilityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 459);
			this.Controls.Add(this.btn_ResetToDefaults);
			this.Controls.Add(this.btn_SaveChanges);
			this.Controls.Add(this.tabControl1);
			this.MinimumSize = new System.Drawing.Size(768, 515);
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
	}
}

