// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// PROJECT INFORMATION
// Project:         Lab02 - Missile Command  
// Date:            Oct.21.2020
// Author:          Austin Klevgaard
// Instructor:      Shane Kelemen   
// Submission Code: 1201_2300_L02
//
// Description:     

// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// FILE INFORMATION
// Class:           CMPE 2300 A01  
// Last Edit:       Oct.21.2020
// Description:     Missile class file 
//
// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlevgaardA_Lab02_Missiles
{
    public delegate void delVoidVoid();

    public partial class statsModeless : Form
    {
        public delVoidVoid _dformClosing = null;

        public statsModeless()
        {
            InitializeComponent();
        }

        public void UpdateStats(gameStatistics stats)
        {
            currIncomingDisplay.Text = stats.thisIncomingMissiles.ToString();
            currMissilesDestDisplay.Text = stats.thisDestroyedMissiles.ToString();
            currFriendliesDisplay.Text = stats.thisFriendlyLaunched.ToString();

            totalEnemyMissileDisplay.Text = stats.totalincomingMissiles.ToString();
            totalMissilesDestroyedDisplay.Text = stats.totalDestroyedMissiles.ToString();
            totalFriendlyLaunchedDisplay.Text = stats.totalFriendlyLaunched.ToString();
            highScoreDisplay.Text = stats.highestScore.ToString();
        }

        private void statsModeless_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_dformClosing != null)
                {
                    _dformClosing();
                }
                e.Cancel = true;
                Hide();
            }
        }
    }
}
