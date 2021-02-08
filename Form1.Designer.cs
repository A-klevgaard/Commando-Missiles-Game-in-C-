namespace KlevgaardA_Lab02_Missiles
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startpauseBttn = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.incomingMissileTrackBar = new System.Windows.Forms.TrackBar();
            this.explosionRadiusTrackbar = new System.Windows.Forms.TrackBar();
            this.incomingMissileLabel = new System.Windows.Forms.Label();
            this.explosionRadiusLabel = new System.Windows.Forms.Label();
            this._incomingMissileDisplay = new System.Windows.Forms.Label();
            this.explosionRadiusDisplay = new System.Windows.Forms.Label();
            this.timerIntervalTrack = new System.Windows.Forms.TrackBar();
            this.timerTrackLabel = new System.Windows.Forms.Label();
            this.timerTrackDisplay = new System.Windows.Forms.Label();
            this.showStatsCheck = new System.Windows.Forms.CheckBox();
            this.changeModesCheck = new System.Windows.Forms.CheckBox();
            this.missileCoolDown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.incomingMissileTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.explosionRadiusTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerIntervalTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startpauseBttn
            // 
            this.startpauseBttn.Location = new System.Drawing.Point(12, 12);
            this.startpauseBttn.Name = "startpauseBttn";
            this.startpauseBttn.Size = new System.Drawing.Size(89, 30);
            this.startpauseBttn.TabIndex = 0;
            this.startpauseBttn.Text = "Start";
            this.startpauseBttn.UseVisualStyleBackColor = true;
            this.startpauseBttn.Click += new System.EventHandler(this.startpauseBttn_Click);
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(12, 70);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(89, 30);
            this.restartButton.TabIndex = 1;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // incomingMissileTrackBar
            // 
            this.incomingMissileTrackBar.Location = new System.Drawing.Point(315, 21);
            this.incomingMissileTrackBar.Minimum = 5;
            this.incomingMissileTrackBar.Name = "incomingMissileTrackBar";
            this.incomingMissileTrackBar.Size = new System.Drawing.Size(247, 56);
            this.incomingMissileTrackBar.TabIndex = 2;
            this.incomingMissileTrackBar.Value = 5;
            this.incomingMissileTrackBar.Scroll += new System.EventHandler(this.incomingMissileTrackBar_Scroll);
            // 
            // explosionRadiusTrackbar
            // 
            this.explosionRadiusTrackbar.Location = new System.Drawing.Point(314, 120);
            this.explosionRadiusTrackbar.Maximum = 100;
            this.explosionRadiusTrackbar.Minimum = 5;
            this.explosionRadiusTrackbar.Name = "explosionRadiusTrackbar";
            this.explosionRadiusTrackbar.Size = new System.Drawing.Size(247, 56);
            this.explosionRadiusTrackbar.TabIndex = 3;
            this.explosionRadiusTrackbar.TickFrequency = 5;
            this.explosionRadiusTrackbar.Value = 50;
            this.explosionRadiusTrackbar.Scroll += new System.EventHandler(this.explosionRadiusTrackbar_Scroll);
            // 
            // incomingMissileLabel
            // 
            this.incomingMissileLabel.AutoSize = true;
            this.incomingMissileLabel.Location = new System.Drawing.Point(143, 21);
            this.incomingMissileLabel.Name = "incomingMissileLabel";
            this.incomingMissileLabel.Size = new System.Drawing.Size(121, 17);
            this.incomingMissileLabel.TabIndex = 4;
            this.incomingMissileLabel.Text = "Incoming Missiles:";
            // 
            // explosionRadiusLabel
            // 
            this.explosionRadiusLabel.AutoSize = true;
            this.explosionRadiusLabel.Location = new System.Drawing.Point(143, 120);
            this.explosionRadiusLabel.Name = "explosionRadiusLabel";
            this.explosionRadiusLabel.Size = new System.Drawing.Size(116, 17);
            this.explosionRadiusLabel.TabIndex = 5;
            this.explosionRadiusLabel.Text = "Explosion Radius";
            // 
            // _incomingMissileDisplay
            // 
            this._incomingMissileDisplay.AutoSize = true;
            this._incomingMissileDisplay.Location = new System.Drawing.Point(270, 21);
            this._incomingMissileDisplay.Name = "_incomingMissileDisplay";
            this._incomingMissileDisplay.Size = new System.Drawing.Size(16, 17);
            this._incomingMissileDisplay.TabIndex = 6;
            this._incomingMissileDisplay.Text = "5";
            // 
            // explosionRadiusDisplay
            // 
            this.explosionRadiusDisplay.AutoSize = true;
            this.explosionRadiusDisplay.Location = new System.Drawing.Point(265, 120);
            this.explosionRadiusDisplay.Name = "explosionRadiusDisplay";
            this.explosionRadiusDisplay.Size = new System.Drawing.Size(24, 17);
            this.explosionRadiusDisplay.TabIndex = 7;
            this.explosionRadiusDisplay.Text = "50";
            // 
            // timerIntervalTrack
            // 
            this.timerIntervalTrack.Location = new System.Drawing.Point(315, 222);
            this.timerIntervalTrack.Maximum = 150;
            this.timerIntervalTrack.Minimum = 10;
            this.timerIntervalTrack.Name = "timerIntervalTrack";
            this.timerIntervalTrack.Size = new System.Drawing.Size(247, 56);
            this.timerIntervalTrack.SmallChange = 10;
            this.timerIntervalTrack.TabIndex = 8;
            this.timerIntervalTrack.TickFrequency = 10;
            this.timerIntervalTrack.Value = 100;
            this.timerIntervalTrack.Scroll += new System.EventHandler(this.timerIntervalTrack_Scroll);
            // 
            // timerTrackLabel
            // 
            this.timerTrackLabel.AutoSize = true;
            this.timerTrackLabel.Location = new System.Drawing.Point(145, 222);
            this.timerTrackLabel.Name = "timerTrackLabel";
            this.timerTrackLabel.Size = new System.Drawing.Size(114, 17);
            this.timerTrackLabel.TabIndex = 9;
            this.timerTrackLabel.Text = "Timer Tick (ms): ";
            // 
            // timerTrackDisplay
            // 
            this.timerTrackDisplay.AutoSize = true;
            this.timerTrackDisplay.Location = new System.Drawing.Point(265, 222);
            this.timerTrackDisplay.Name = "timerTrackDisplay";
            this.timerTrackDisplay.Size = new System.Drawing.Size(24, 17);
            this.timerTrackDisplay.TabIndex = 10;
            this.timerTrackDisplay.Text = "60";
            // 
            // showStatsCheck
            // 
            this.showStatsCheck.AutoSize = true;
            this.showStatsCheck.Location = new System.Drawing.Point(12, 129);
            this.showStatsCheck.Name = "showStatsCheck";
            this.showStatsCheck.Size = new System.Drawing.Size(100, 21);
            this.showStatsCheck.TabIndex = 11;
            this.showStatsCheck.Text = "Show Stats";
            this.showStatsCheck.UseVisualStyleBackColor = true;
            this.showStatsCheck.CheckedChanged += new System.EventHandler(this.showStatsCheck_CheckedChanged);
            // 
            // changeModesCheck
            // 
            this.changeModesCheck.AutoSize = true;
            this.changeModesCheck.Location = new System.Drawing.Point(12, 168);
            this.changeModesCheck.Name = "changeModesCheck";
            this.changeModesCheck.Size = new System.Drawing.Size(107, 21);
            this.changeModesCheck.TabIndex = 12;
            this.changeModesCheck.Text = "Game Mode";
            this.changeModesCheck.UseVisualStyleBackColor = true;
            this.changeModesCheck.CheckedChanged += new System.EventHandler(this.changeModesCheck_CheckedChanged);
            // 
            // missileCoolDown
            // 
            this.missileCoolDown.Tick += new System.EventHandler(this.missileCoolDown_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 290);
            this.Controls.Add(this.changeModesCheck);
            this.Controls.Add(this.showStatsCheck);
            this.Controls.Add(this.timerTrackDisplay);
            this.Controls.Add(this.timerTrackLabel);
            this.Controls.Add(this.timerIntervalTrack);
            this.Controls.Add(this.explosionRadiusDisplay);
            this.Controls.Add(this._incomingMissileDisplay);
            this.Controls.Add(this.explosionRadiusLabel);
            this.Controls.Add(this.incomingMissileLabel);
            this.Controls.Add(this.explosionRadiusTrackbar);
            this.Controls.Add(this.incomingMissileTrackBar);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.startpauseBttn);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.incomingMissileTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.explosionRadiusTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerIntervalTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startpauseBttn;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.TrackBar incomingMissileTrackBar;
        private System.Windows.Forms.TrackBar explosionRadiusTrackbar;
        private System.Windows.Forms.Label incomingMissileLabel;
        private System.Windows.Forms.Label explosionRadiusLabel;
        private System.Windows.Forms.Label _incomingMissileDisplay;
        private System.Windows.Forms.Label explosionRadiusDisplay;
        private System.Windows.Forms.TrackBar timerIntervalTrack;
        private System.Windows.Forms.Label timerTrackLabel;
        private System.Windows.Forms.Label timerTrackDisplay;
        private System.Windows.Forms.CheckBox showStatsCheck;
        private System.Windows.Forms.CheckBox changeModesCheck;
        private System.Windows.Forms.Timer missileCoolDown;
    }
}

