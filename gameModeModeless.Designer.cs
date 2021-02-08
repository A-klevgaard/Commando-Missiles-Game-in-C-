namespace KlevgaardA_Lab02_Missiles
{
    partial class gameModeModeless
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.missileBounceCheck = new System.Windows.Forms.CheckBox();
            this.reloadRequiredCheck = new System.Windows.Forms.CheckBox();
            this.modeSettingLabel = new System.Windows.Forms.Label();
            this.shockAndAweCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // missileBounceCheck
            // 
            this.missileBounceCheck.AutoSize = true;
            this.missileBounceCheck.Location = new System.Drawing.Point(12, 58);
            this.missileBounceCheck.Name = "missileBounceCheck";
            this.missileBounceCheck.Size = new System.Drawing.Size(189, 21);
            this.missileBounceCheck.TabIndex = 0;
            this.missileBounceCheck.Text = "Missiles Bounce off Walls";
            this.missileBounceCheck.UseVisualStyleBackColor = true;
            this.missileBounceCheck.CheckedChanged += new System.EventHandler(this.missileBounceCheck_CheckedChanged);
            // 
            // reloadRequiredCheck
            // 
            this.reloadRequiredCheck.AutoSize = true;
            this.reloadRequiredCheck.Location = new System.Drawing.Point(12, 98);
            this.reloadRequiredCheck.Name = "reloadRequiredCheck";
            this.reloadRequiredCheck.Size = new System.Drawing.Size(137, 21);
            this.reloadRequiredCheck.TabIndex = 1;
            this.reloadRequiredCheck.Text = "Reload Required";
            this.reloadRequiredCheck.UseVisualStyleBackColor = true;
            this.reloadRequiredCheck.CheckedChanged += new System.EventHandler(this.reloadRequiredCheck_CheckedChanged);
            // 
            // modeSettingLabel
            // 
            this.modeSettingLabel.AutoSize = true;
            this.modeSettingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modeSettingLabel.Location = new System.Drawing.Point(12, 22);
            this.modeSettingLabel.Name = "modeSettingLabel";
            this.modeSettingLabel.Size = new System.Drawing.Size(116, 20);
            this.modeSettingLabel.TabIndex = 2;
            this.modeSettingLabel.Text = "Mode Settings";
            // 
            // shockAndAweCheck
            // 
            this.shockAndAweCheck.AutoSize = true;
            this.shockAndAweCheck.Location = new System.Drawing.Point(12, 140);
            this.shockAndAweCheck.Name = "shockAndAweCheck";
            this.shockAndAweCheck.Size = new System.Drawing.Size(256, 21);
            this.shockAndAweCheck.TabIndex = 3;
            this.shockAndAweCheck.Text = "Shock and Awe Enabled (right click)";
            this.shockAndAweCheck.UseVisualStyleBackColor = true;
            this.shockAndAweCheck.CheckedChanged += new System.EventHandler(this.shockAndAweCheck_CheckedChanged);
            // 
            // gameModeModeless
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 182);
            this.Controls.Add(this.shockAndAweCheck);
            this.Controls.Add(this.modeSettingLabel);
            this.Controls.Add(this.reloadRequiredCheck);
            this.Controls.Add(this.missileBounceCheck);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "gameModeModeless";
            this.Text = "gameModeModeless";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.gameModeModeless_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox missileBounceCheck;
        private System.Windows.Forms.CheckBox reloadRequiredCheck;
        private System.Windows.Forms.Label modeSettingLabel;
        private System.Windows.Forms.CheckBox shockAndAweCheck;
    }
}