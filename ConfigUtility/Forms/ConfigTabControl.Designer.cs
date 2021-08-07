
namespace ConfigUtility
{
	partial class ConfigTabControl
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
			this.lbl_FootNote = new System.Windows.Forms.Label();
			this.lbl_Description = new System.Windows.Forms.Label();
			this.flow_Flags = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// lbl_FootNote
			// 
			this.lbl_FootNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbl_FootNote.AutoSize = true;
			this.lbl_FootNote.Location = new System.Drawing.Point(3, 320);
			this.lbl_FootNote.Name = "lbl_FootNote";
			this.lbl_FootNote.Size = new System.Drawing.Size(102, 25);
			this.lbl_FootNote.TabIndex = 4;
			this.lbl_FootNote.Text = "Footer text.";
			// 
			// lbl_Description
			// 
			this.lbl_Description.AutoSize = true;
			this.lbl_Description.Location = new System.Drawing.Point(3, 11);
			this.lbl_Description.Name = "lbl_Description";
			this.lbl_Description.Size = new System.Drawing.Size(279, 25);
			this.lbl_Description.TabIndex = 3;
			this.lbl_Description.Text = "This page contains some settings.";
			// 
			// flow_Flags
			// 
			this.flow_Flags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flow_Flags.AutoScroll = true;
			this.flow_Flags.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flow_Flags.Location = new System.Drawing.Point(3, 39);
			this.flow_Flags.Name = "flow_Flags";
			this.flow_Flags.Size = new System.Drawing.Size(708, 278);
			this.flow_Flags.TabIndex = 2;
			this.flow_Flags.WrapContents = false;
			// 
			// ConfigTabControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lbl_FootNote);
			this.Controls.Add(this.lbl_Description);
			this.Controls.Add(this.flow_Flags);
			this.Name = "ConfigTabControl";
			this.Size = new System.Drawing.Size(714, 357);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_FootNote;
		private System.Windows.Forms.Label lbl_Description;
		private System.Windows.Forms.FlowLayoutPanel flow_Flags;
	}
}
