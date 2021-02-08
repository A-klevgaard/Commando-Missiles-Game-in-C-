// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// PROJECT INFORMATION
// Project:         Lab02 - Missile Command  
// Date:            Oct.21.2020
// Author:          Austin Klevgaard
// Instructor:      Shane Kelemen   
// Submission Code: 1201_2300_L02
//
// Description:     Recreation of the classic game Missile Commando, except with way worse graphics, and way less functionality.

// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// FILE INFORMATION
// Class:           CMPE 2300 A01  
// Last Edit:       Oct.21.2020
// Description:     Form class file - Main file where the game is run in. Uses windows forms to control the game and change game settings
//                  Missiles are generated along the top of the screen, and the player fires missiles from the bottom center. If
//                  5 enemy missiles hit the bottom then the game is over and you die, but if you get the highest score, at least that
//                  will live on (until the program stops, I didn't make a filestream to save it this time)
//
// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using GDIDrawer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlevgaardA_Lab02_Missiles
{
    /// <summary>
    /// nameSpace level strucutre to allow the same structure to be created in both the main form, and in
    /// the stats modeless dialogue
    /// </summary>
    public struct gameStatistics
    {
        public int thisIncomingMissiles;    //number of incoming missiles generated this round
        public int thisFriendlyLaunched;    //number of friendly missiles launched this round
        public int thisDestroyedMissiles;   //number of missiles destroyed this round
        public int totalincomingMissiles;   //total incoming missiles generated, except this round
        public int totalDestroyedMissiles;  //total missiles destroyed, except this round
        public int totalFriendlyLaunched;   //total friendly missiles launched, except this round
        public int highestScore;            //Highest score recorded since the program launched
    }
    public partial class Form1 : Form
    {
        statsModeless _statsDLG;            //modeless dialogue to show the stats of the game
        gameModeModeless _gameModesDLG;     //modeless dialogue to allow the user to choose game mode settings
        CDrawer gameCanvas = new CDrawer(); //canvas the game is played in
        int _playerLives = 5;               //total player lives this round
        int _maxIncoming = 0;               //max amount of missiles that can drop at a time
        int _score = 0;                     //current player score
        int[] missileCapacity;              //Array used to limit available missiles if enabled
        int missileCooldownTick;            //timer tick that is used to control missile cooldown
        Random rng = new Random();          //random number generator

        /// <summary>
        /// Structure that is used to control the setting of game modes through bools
        /// </summary>
        private struct Modes
        {
            public bool missileBounce;      //determines if wall bounce in enabled for enemy missiles
            public bool reloadLimit;        //determines if there is a 5 missile limit on the user that recharges
            public bool shockAndAwe;        //lets the user go Many Booms on right click
        }

        gameStatistics gameStats = new gameStatistics();    //initializes a modeless dialogue to show stats
        Modes modeSettings = new Modes();                   //initalizes a modeless dialogue to show gamemode settings
        List<Missile> myMissile;                            //initializes a list for friendly missiles
        List<Missile> enemyMissile;                         //initializes a list for enemy missiles
        List<Missile> FriendlyBombard;                      //a list for a friendly bombardment (shock and awe)
        enum eGameState { Paused, Unstarted, Over, Running};//enumeration that is used to control the games current state
        eGameState gameState = eGameState.Unstarted;

        public Form1()
        {
            InitializeComponent();

            enemyMissile = new List<Missile>();                 //creates memory space for the game list, but only initializes them to null
            myMissile = new List<Missile>();
            FriendlyBombard = new List<Missile>();

            Missile.Canvas = gameCanvas;                        //sets the formCanvas to be the missile canvas
            Missile.Explosion = explosionRadiusTrackbar.Value;  //sets explostion radius to match the trackbar value
            gameCanvas.MouseLeftClickScaled += GameCanvas_MouseLeftClickScaled;     //creates the left click event handler
            gameCanvas.MouseRightClickScaled += GameCanvas_MouseRightClickScaled;   //creates the right click event handler
            _maxIncoming = incomingMissileTrackBar.Value;                           //establishes how many missiles can come in at once

        }
        //******************************************************************************************
        //Canvas Event Handler and their helper methods
        //******************************************************************************************
        /// <summary>
        /// Invokes Shock and Awe if the user right clicks in the canvas
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dr"></param>
        private void GameCanvas_MouseRightClickScaled(Point pos, CDrawer dr)
        {
            try
            {
                Invoke(new Action(() => ShockandAwe()));
            }
            catch (Exception errClick)
            {

                MessageBox.Show($"Error: {errClick}", "Error", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Fires the newPlayerMissile method in main if a left click occurs in the canvas area
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dr"></param>
        private void GameCanvas_MouseLeftClickScaled(Point pos, CDrawer dr)
        {
            try
            {
                Invoke(new Action(() => newPlayerMissile(pos)));
            }
            catch (Exception errClick)
            {

                MessageBox.Show($"Error: {errClick}", "Error", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Helper Method to add a friendly missile to my Missile list
        /// </summary>
        /// <param name="clickPoint"></param>
        private void newPlayerMissile(Point clickPoint)
        {
            //prevents errors from clicking in canvas before the game is running
            if (gameState == eGameState.Running)
            {
                //Controller for Missile Behavior. If reload limit is enabled only 5 missiles are available at a time before they must recharge.
                if (modeSettings.reloadLimit)
                {
                    //stars the timer that controls missile cooldown
                    missileCoolDown.Enabled = true;

                    //if a 0 (ready missile) is found in the index of an array then fire a missle and set missile cooldown for that array spot
                    int missileIndex = Array.IndexOf(missileCapacity, 0);
                    if (missileIndex >= 0)
                    {
                        //value in index of missile capacity is how many seconds it takes for that missile to reload
                        missileCapacity[missileIndex] = 5;
                        myMissile.Add(new Missile(clickPoint));
                        gameStats.thisFriendlyLaunched++;
                    }
                }
                //limitless missiles
                else
                {
                    myMissile.Add(new Missile(clickPoint));
                    gameStats.thisFriendlyLaunched++;
                }
            }
        }

        /// <summary>
        /// Helper method for canvas right click event. If the user right clicks in the canvas 30 friendly missiles are creates
        /// and added to the bombardment list. These missiles fly to random locations in the canvas and explode.
        /// </summary>
        private void ShockandAwe()
        {
            //ensures shock and awe is enabled
            if (modeSettings.shockAndAwe)
            {
                //adds missiles into a friendly missile bombardment
                for (int i = 0; i < 31; i++)
                {
                    //FriendlyBombard.Add(new Missile(rng.Next(0, gameCanvas.ScaledHeight)));
                    FriendlyBombard.Add(new Missile(new Point(rng.Next(0, gameCanvas.ScaledWidth), rng.Next(0, gameCanvas.ScaledHeight))));
                }
            }
        }

        //******************************************************************************************
        //Timer Related Main form event Handlers
        //******************************************************************************************
        /// <summary>
        /// Main Timer event of the form, and where the meat of the program occurs. Every timer tick active missiles are moved.
        /// If a friendly missile comes in contact with an enemy missiles it removes it. If an enemy missiles reaches the bottom
        /// it will explode and take a player life
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //checks if game is over
            if (gameState == eGameState.Over)
            {
                //update total lifetime stats for the current runtime (doesn't save to file right now though)
                EndGameStatsUpdate();

                gameCanvas.AddText("GAME OVER", 100, Color.Red);
                timer1.Enabled = false;
            }

            //populates a new enemy missile into the list
            if (enemyMissile.Count < _maxIncoming )
            {
                enemyMissile.Add(new Missile());
                //update currernt incoming missiles stat
                gameStats.thisIncomingMissiles++;
            }
            //ensures that the game state is currently running
            if (gameState == eGameState.Running)
            {
                gameCanvas.Clear();

                //******************************************************************************************
                //Enemy Missile Movement and Control
                //******************************************************************************************
                //moves and draws the enemy missiles
                if (enemyMissile.Count > 0)
                {
                    foreach (Missile em in enemyMissile)
                    {
                        em.Move();
                        em.Show();
                    }
                }

                //Controls what enemy missile behavior is displayed when it encounters a horizontal edge
                EdgeManeuverActions();

                //if an enemy missile reaches the ground then remove a player life
                foreach (Missile em in enemyMissile)
                {
                    if (em.TargetReached)
                    {
                        _playerLives--;
                        if (_playerLives == 0) { gameState = eGameState.Over; }
                    }
                }
                enemyMissile.RemoveAll(Missile.doneExplosion);
                
                //******************************************************************************************
                //Friendly Missile Movement and Control
                //******************************************************************************************
                //moves and draws friendly missiles
                if (myMissile.Count > 0)
                {
                    foreach (Missile m in myMissile)
                    {
                        m.Move();
                        m.Show();
                    }
                }
                //clears enemy missiles detroyed by friendlies
                ClearDestroyedMissiles(myMissile);
                //removes expired friendly missiles
                myMissile.RemoveAll(Missile.doneExplosion);

                if (FriendlyBombard.Count > 0)
                {
                    foreach (Missile m in FriendlyBombard)
                    {
                        m.Move();
                        m.Show();
                    }
                }
                //clears enemy missiles detroyed by friendlies
                ClearDestroyedMissiles(FriendlyBombard);
                //removes expired 
                FriendlyBombard.RemoveAll(m => m.FinishedExplosion == true);

                //******************************************************************************************
                //Updates the top status bar
                //******************************************************************************************
                //semi-transparent grey background bar
                gameCanvas.AddRectangle(0, 0, 800, 25, Color.FromArgb(150, 230, 230, 230), 0);
                //updates the amount of player lives left
                gameCanvas.AddText($"Lives Remaining: {_playerLives}", 14, 610, 0, 200, 25, Color.Blue);
                //updates the player score
                gameCanvas.AddText($"Score: {_score}", 14, -40, 0, 200, 25, Color.Orange);

                //******************************************************************************************
                //If enabled sets up missile reload limiter and draws available missiles to canvas
                //******************************************************************************************
                //If Reload Limits have been set, ensures that an array is created to track missiles fired
                if (modeSettings.reloadLimit && missileCapacity == null)
                {
                    missileCapacity = new int[] { 0, 0, 0, 0, 0 };
                }
                if (modeSettings.reloadLimit)
                {
                    //draws the current missile capacity to the top of the canvas
                    int startX = 325;
                    for (int index = 0; index < missileCapacity.Length; index++)
                    {
                        if (missileCapacity[index] == 0)
                        {
                            gameCanvas.AddEllipse(startX, 0, 20, 20, Color.Green, 0);
                            startX += 30;
                        }
                    }
                }

                //******************************************************************************************
                //Sends updated stats to the modeless dialogue every timer tick
                //******************************************************************************************
                if (_statsDLG != null) { _statsDLG.UpdateStats(gameStats); }
                    

            }
        }
        /// <summary>
        /// Second timer that controls the cooldown of friendly missile fire if that mode is selected
        /// Timer runs at 100ms (max Value) so every 10 timer ticks is 1 second.
        /// 
        /// A missile is considered "reloaded" when it's index in the missile capacity array is 0;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void missileCoolDown_Tick(object sender, EventArgs e)
        {
            //Counts 1 second
            if (missileCooldownTick < 10) missileCooldownTick++;
            else
            {
                //decrements every missile that is currently on cooldown 
                for (int index = 0; index < missileCapacity.Length; index++)
                {
                    if (missileCapacity[index] != 0) missileCapacity[index]--;
                }
                //resets tick timer
                missileCooldownTick = 0;
            }
        }
        //******************************************************************************************
        //Non-Timer related main form event handlers
        //******************************************************************************************
        private void startpauseBttn_Click(object sender, EventArgs e)
        {

            //Changes the game state depending on which button is pressed or if the game ends

            //starts the game
            if (gameState == eGameState.Unstarted)
            {
                gameState = eGameState.Running;
                timer1.Enabled = true;
                startpauseBttn.Text = "Pause";
                return;
            }
            //pauses the game
            if (gameState == eGameState.Running)
            {
                gameState = eGameState.Paused;
                timer1.Enabled = false;
                startpauseBttn.Text = "Go";
                return;
            }
            //unpauses the game
            if (gameState == eGameState.Paused)
            {
                gameState = eGameState.Running;
                timer1.Enabled = true;
                startpauseBttn.Text = "Pause";
                return;
            }
        }

        private void incomingMissileTrackBar_Scroll(object sender, EventArgs e)
        {
            _incomingMissileDisplay.Text = incomingMissileTrackBar.Value.ToString();
            _maxIncoming = incomingMissileTrackBar.Value;

        }

        private void timerIntervalTrack_Scroll(object sender, EventArgs e)
        {
            //ensures that the trackbar snaps to a 10s value
            //convert trackbar value to a double by a factor of it's tick frequency (10)
            
            int currentVal = timerIntervalTrack.Value;
            double dVal = (double)timerIntervalTrack.Value / timerIntervalTrack.TickFrequency;
            int index = Convert.ToInt32(Math.Round(dVal));
            timerIntervalTrack.Value = index * timerIntervalTrack.TickFrequency;

            //sets the timer interval (min = 10ms, max = 150 ms
            timer1.Interval = 160 - timerIntervalTrack.Value;
            timerTrackDisplay.Text = (160 - timerIntervalTrack.Value).ToString();
        }

        private void explosionRadiusTrackbar_Scroll(object sender, EventArgs e)
        {
            Missile.Explosion = explosionRadiusTrackbar.Value;
            explosionRadiusDisplay.Text = explosionRadiusTrackbar.Value.ToString();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            //resets the game values to their initial states
            timer1.Enabled = false;
            EndGameStatsUpdate();
            _playerLives = 5;
            missileCapacity = new int[] { 0, 0, 0, 0, 0 };
            _score = 0;
            gameStats.thisDestroyedMissiles = 0;
            gameStats.thisFriendlyLaunched = 0;
            gameStats.thisIncomingMissiles = 0;
            _maxIncoming = incomingMissileTrackBar.Value;
            gameCanvas.Clear();
            enemyMissile.Clear();
            myMissile.Clear();
            gameState = eGameState.Running;
            timer1.Enabled = true;
        }

        private void showStatsCheck_CheckedChanged(object sender, EventArgs e)
        {
            //Shows or Hides the stats modeless dialogue
            if (showStatsCheck.Checked)
            {
                if (_statsDLG == null)
                {
                    _statsDLG = new statsModeless();
                    _statsDLG._dformClosing = new delVoidVoid(CallBackCloseStats);
                }
                _statsDLG.Show();
            }
            else _statsDLG.Hide();
        }

        private void changeModesCheck_CheckedChanged(object sender, EventArgs e)
        {
            //shows are hides the game mode settings modeless dialogue
            if (changeModesCheck.Checked)
            {
                if (_gameModesDLG == null)
                {
                    _gameModesDLG = new gameModeModeless();
                    _gameModesDLG._dBounceChanged = new delVoidBool(CallBackBounceMode);
                    _gameModesDLG._dReloadChanged = new delVoidBool(CallBackReloadMode);
                    _gameModesDLG._dShockChanged = new delVoidBool(CallBackShockAndAwe);
                    _gameModesDLG._dformClosing = new delVoidVoid(CallBackCloseModes);
                }
                _gameModesDLG.Show();
            }
            else _gameModesDLG.Hide();
        }

        //******************************************************************************************
        //Final Helper Methods
        //******************************************************************************************
        /// <summary>
        /// Clears out any missile that is currently overlapping a friendly missile
        /// This will also update the score and remove the missiles from the list
        /// </summary>
        /// <param name="missileList"></param>
        private void ClearDestroyedMissiles(List<Missile> missileList)
        {
            //checks to see if enemy missiles should be destroyed 
            List<Missile> overlaps = missileList.Intersect(enemyMissile).ToList();

            //adds how many missiles were destroyed
            gameStats.thisDestroyedMissiles += overlaps.Count;
            //each removed missile adds 100 to the current score
            _score += overlaps.Count * 100;

            //removes destroyed enemy missiles from the list
            foreach (Missile m in overlaps)
            {
                while (enemyMissile.Remove(m)) ;
            }
        }
        /// <summary>
        /// Helper method to control how an enemy missile reacts at the edge boundaries. If edge bounce is allowed the offending
        /// missile is removed and a new one is created with an opposite but equal angle. Otherwise if bounce is not enabled
        /// then the missile is simply destroyed at the border
        /// </summary>
        private void EdgeManeuverActions()
        {
            if (modeSettings.missileBounce)
            {
                List<Missile> temp = new List<Missile>();
                //Bounce missiles off the horizontal edges
                temp = enemyMissile.FindAll(Missile.IsPastHorizontal).ToList();

                //remove bouncing missiles, create a new one.
                foreach (Missile t in temp)
                {
                    enemyMissile.Remove(t);
                    enemyMissile.Add(new Missile(Missile.Where(t), t.Angle));
                }
                temp.Clear();
            }
            //remove all offending missiles
            else
            {
                enemyMissile.RemoveAll(Missile.IsPastHorizontal);
                enemyMissile.RemoveAll(Missile.IsPastVertical);
            }
        }
        /// <summary>
        /// Updates the stats at the end of the game so they are recorded for the next game
        /// </summary>
        private void EndGameStatsUpdate()
        {
            //update total lifetime stats for the current runtime (doesn't save to file right now though)
            gameStats.totalDestroyedMissiles += gameStats.thisDestroyedMissiles;
            gameStats.totalFriendlyLaunched += gameStats.thisFriendlyLaunched;
            gameStats.totalincomingMissiles += gameStats.thisIncomingMissiles;
            if (_score > gameStats.highestScore) { gameStats.highestScore = _score; }
        }

        //******************************************************************************************
        //Delegate Callbacks
        //******************************************************************************************
        
        /// <summary>
        /// closes the stats modeless dialogue
        /// </summary>
        private void CallBackCloseStats()
        {
            showStatsCheck.Checked = false;
        }
        /// <summary>
        /// closes the game mode modeless dialogue
        /// </summary>
        private void CallBackCloseModes()
        {
            changeModesCheck.Checked = false;
        }
        /// <summary>
        /// Sets the bool for bounce mode
        /// </summary>
        /// <param name="value"></param>
        private void CallBackBounceMode(bool value)
        {
            modeSettings.missileBounce = value;
        }
        /// <summary>
        /// sets the bool for reload limiting
        /// </summary>
        /// <param name="value"></param>
        private void CallBackReloadMode(bool value)
        {
            modeSettings.reloadLimit = value;
        }
        /// <summary>
        /// sets the bool that enables shock and awe
        /// </summary>
        /// <param name="value"></param>
        private void CallBackShockAndAwe(bool value)
        {
            modeSettings.shockAndAwe = value;
        }


    }
}
