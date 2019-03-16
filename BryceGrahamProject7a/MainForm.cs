using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

/**
 * MainForm.cs
 * Authors: Rob Merrick
 * This is the main class which invokes the entire game set of
 * methods.
 */

namespace BryceGrahamProject7a
{
    public partial class MainForm : Form
    {

        //Variable delcarations
        private bool isGameManagerInitialized;
        private Sound backgroundMusic;
        GameManager gameManager;
        System.Diagnostics.Stopwatch framesPerSecondTimer;

        ///*************************************************************************************************************
        ///<summary>This constructor initializes the form. Do no place custom code here as it may be unstable.</summary>
        ///*************************************************************************************************************
        
        public MainForm()
        {
            InitializeComponent();
        }

        ///********************************************************************************************
        ///<summary>After the constructor, program flow moves here to set up the form window.</summary>
        ///********************************************************************************************

        private void MainForm_Load(object sender, EventArgs e)
        {
            isGameManagerInitialized = false;
            framesPerSecondTimer = new System.Diagnostics.Stopwatch();
            backgroundMusic = new Sound("Music\\WheresMyThing.wav");
            backgroundMusic.playSound(true);
            backgroundMusic.setVolume(0.9);
            btn_NewGame.Anchor = AnchorStyles.Bottom;
            btn_Settings.Anchor = AnchorStyles.Bottom;
            btn_Credits.Anchor = AnchorStyles.Bottom;
            controlsButton.Anchor = AnchorStyles.Bottom;
            pctr_MainLogo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
        }

        ///*******************************************************************************
        ///<summary>Begins playing a new game with the current settings applied.</summary>
        ///*******************************************************************************

        private void btn_NewGame_Click(object sender, EventArgs e)
        {
            (new Sound("Thruster.wav")).playSound(false);

            //for(int i = 0; i < 100; i++)
            //{
            //    if(i % 16 <= 8)
            //        this.BackColor = Color.DarkRed;
            //    else
            //        this.BackColor = Color.Black;

            //    backgroundMusic.setVolume(1.0 - i/100.0);
            //    Application.DoEvents();
            //    Thread.Sleep(15);
            //}
            this.Cursor = Cursors.Cross;
            backgroundMusic.stopSound();
            Voices.intitialize();
            Voices.theForce.playSound(false);
            Thread.Sleep(2000);
            this.BackColor = Color.Black;
            backgroundMusic.stopSound();
            gameManager = new GameManager();
            btn_Credits.Hide();
            btn_NewGame.Hide();
            btn_Settings.Hide();
            controlsButton.Hide();
            pctr_MainLogo.Hide();
        }

        ///***********************************************************************************
        ///<summary>Shows a window that allows the user to change gameplay settings.</summary>
        ///***********************************************************************************

        // Bryce Graham
        // Created form
        private void btn_Settings_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
        }

        ///******************************************
        ///<summary>Shows a credits screen.</summary>
        ///******************************************

        private void btn_Credits_Click(object sender, EventArgs e)
        {
            string credits = "";
            credits += "Credits\r\n\r\n";
            credits += "Directed by Dr. David Beard\r\n";
            credits += "Lead Programmer - Rob Merrick\r\n";
            credits += "Assistant Programmer - Ted Delezene\r\n";
            credits += "Additional Programming by Bryce Graham\r\n";
            credits += "Testing by Dallyn Graham\r\n";
            credits += "Music Composed by Rob Merrick\r\n";
            credits += "Intro Music \"Where's My Thing\" by Rush\r\n";
            credits += "Image Processing - Ted Delezene, Rob Merrick, Thomas Veyrat, Qiao Wei\r\n";
            credits += "Sound Effects - Rob Merrick, Yoji Inagaki (Nintendo, Star Fox 64 copyright 1997)\r\n";
            MessageBox.Show(credits);
        }

        ///*********************************************************************************************************
        ///<summary>Every time the main form goes to redraw, this method is called. This is where the main
        ///loop exists for the game. All frame drawing occurs here. Note that there is a call to invalidate
        ///the graphics at the end, which causes it to loop until the game is over, beginning as soon as gameManager
        ///is not null.</summary>
        ///*********************************************************************************************************

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if(gameManager == null)
                return;

            framesPerSecondTimer.Start();
            GameManager.canvasOrigin = new PointF(this.Location.X, this.Location.Y);
            GameManager.canvas = e.Graphics;

            if(!isGameManagerInitialized)
            {
                gameManager.initialize();
                isGameManagerInitialized = true;
            }

            gameManager.clearScreen();
            gameManager.updateFrame();
            gameManager.drawFrame();
            framesPerSecondTimer.Stop();

            if(framesPerSecondTimer.ElapsedMilliseconds < 33)
                Thread.Sleep((int) (33 - framesPerSecondTimer.ElapsedMilliseconds)); //This simulates roughly 30 fps.

            framesPerSecondTimer.Reset();

            if(gameManager.isGameOver())
                resetForm();
            else
                this.Invalidate();
        }

        ///*******************************************************************************************************
        ///<summary>Once the game finishes, call this method to reset the form back to the title screen.</summary>
        ///*******************************************************************************************************

        private void resetForm()
        {
            isGameManagerInitialized = false;
            gameManager.clearScreen();
            GameManager.canvas = null;
            gameManager = null;
            btn_Credits.Show();
            btn_NewGame.Show();
            btn_Settings.Show();
            controlsButton.Show();
            pctr_MainLogo.Show();
            backgroundMusic = new Sound("Music\\Title Theme Song.wav");
            backgroundMusic.playSound(true);
            backgroundMusic.setVolume(0.9);
            this.Cursor = Cursors.Default;
        }

        ///******************************************************************************************
        ///<summary>This event is called if the user holds the mouse down on the main form.</summary>
        ///******************************************************************************************

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if(!UserControls.getLeftMouseDown())
                UserControls.setLeftMouseDown(e.Button.Equals(MouseButtons.Left));

            if(!UserControls.getRightMouseDown())
                UserControls.setRightMouseDown(e.Button.Equals(MouseButtons.Right));
        }

        ///****************************************************************************************
        ///<summary>This event is called if the user releases the mouse on the main form.</summary>
        ///****************************************************************************************

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if(UserControls.getLeftMouseDown())
                UserControls.setLeftMouseDown(!e.Button.Equals(MouseButtons.Left));

            if(UserControls.getRightMouseDown())
                UserControls.setRightMouseDown(!e.Button.Equals(MouseButtons.Right));
        }
        
        // Bryce Graham
        // Handles the keyboard input
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'p')
            {
                if (UserControls.PauseButtonPushed == false)
                    UserControls.PauseButtonPushed = true;
                else UserControls.PauseButtonPushed = false;
            }
            else if(e.KeyChar == 't')
            {
                if (UserControls.TargetingButtonPushed == false)
                    UserControls.TargetingButtonPushed = true;
                else UserControls.TargetingButtonPushed = false;
            }
        }

        // Bryce Graham
        // Shows the control mapping
        private void controlsButton_Click(object sender, EventArgs e)
        {
            string controlString;
            controlString = "Fire: Right Mouse\r\n";
            controlString += "Thrust: Left Mouse\r\n";
            controlString += "Toggle Targeting System: T\r\n";
            controlString += "Pause: P\r\n";
            MessageBox.Show(controlString);
        }

    }
}