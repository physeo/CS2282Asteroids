using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * GUI.cs
 * Autors: Rob Merrick
 * This class is used to draw an on screen Graphical User 
 * Interface (GUI) so that the user is better able to 
 * understand the gameplay.
 */

namespace BryceGrahamProject7a
{
    class GUI
    {

        //Variable declarations
        private Font drawFont;
        private SolidBrush drawBrush;
        private StringFormat drawFormat;

        ///***********************************************************
        ///<summary>Creates a new instance of the GUI class.</summary>
        ///***********************************************************

        public GUI()
        {
            drawFont = new System.Drawing.Font("Copperplate Gothic Bold", 20);
            drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            drawFormat = new System.Drawing.StringFormat();
        }

        ///*****************************************************************************************
        ///<summary>Draws the updated GUI to the screen based on the provided information.</summary>
        ///*****************************************************************************************

        // Bryce Graham
        // Added visuals for the invulnerability timer and pause button
        public void drawGUI(int remainingLives, long invunlnerabilityTimer)
        {
            drawString("Lives: " + remainingLives.ToString(), 10, 10);
            drawString("Score: " + Scores.getCurrentScore().ToString(), 10, drawFont.GetHeight() + 10);
            drawString("High Score: " + Scores.getHighScore().ToString(), 10, 2.0F * drawFont.GetHeight() + 10);
            drawString("Level: " + Level.getCurrentLevel().ToString(), 10, 3.0F * drawFont.GetHeight() + 10);
            if (invunlnerabilityTimer <= 3000)
                 drawString("Invulnerability for: " + (int)(4000 - invunlnerabilityTimer)/1000, 500, 500);
            if (UserControls.PauseButtonPushed)
                drawString("PAUSED", 600, 450);
            if (UserControls.TargetingButtonPushed)
                drawString("Targeting: ON", 10, 4.0F * drawFont.GetHeight() + 10);
            else drawString("Targeting: OFF", 10, 4.0F * drawFont.GetHeight() + 10);
        }

        ///**************************************************************************************************************
        ///<summary>Draws the provided string to the xLocation and yLocation. xLocation and yLocation are relative to the
        ///form's top-left corner.</summary>
        ///**************************************************************************************************************

        private void drawString(string valueToDraw, float xLocation, float yLocation)
        {
            GameManager.canvas.DrawString(valueToDraw, drawFont, drawBrush, xLocation, yLocation, drawFormat);
        }

    }
}