namespace MyFeedsDesktop
{
	partial class Form1
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
			StartButton = new Button();
			SuspendLayout();
			// 
			// StartButton
			// 
			StartButton.Location = new Point(73, 124);
			StartButton.Name = "StartButton";
			StartButton.Size = new Size(448, 113);
			StartButton.TabIndex = 0;
			StartButton.Text = "Get Feeds";
			StartButton.UseVisualStyleBackColor = true;
			StartButton.Click += button1_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(571, 450);
			Controls.Add(StartButton);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private Button StartButton;
	}
}
