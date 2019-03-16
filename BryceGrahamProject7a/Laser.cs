﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Laser.cs
 * Autors: Rob Merrick
 * This class contains the needed information for a laser beam.
 */

namespace BryceGrahamProject7a
{
    class Laser : Entity2D
    {

        //Variable declarations
        private bool laserIsFromPlayerShip;
        private int laserLife;

        //Static Variables
        private static Sound laserSound1;
        private static Sound laserSound2;
        private static Sound laserSound3;

        ///********************************************************************************************************************
        ///<summary>Creates a new instance of the Laser class. If laserIsFromPlayerShip is set to true, then the laser will
        ///only interact with enemy ships and asteroids. Otherwise, the laser will only interact with player ships and friendly
        ///entities. Make sure that the behavior for any new entity classes defined has this interaction included.</summary>
        ///********************************************************************************************************************

        public Laser(bool laserIsFromPlayerShip)
        {
            this.laserIsFromPlayerShip = laserIsFromPlayerShip;
            laserLife = 0;
            setGraphic(new Graphic("Laser.png"));

            if(laserSound1 == null)
                laserSound1 = new Sound("Laser1.wav");

            if(laserSound2 == null)
                laserSound2 = new Sound("Laser2.wav");

            if(laserSound3 == null)
                laserSound3 = new Sound("Laser3.wav");
        }

        ///***************************************************************************************************************
        ///<summary>Returns true if this laser was shot from the player ship (or a friendly entity). Returns false if this
        ///laser was shot from an enemy ship (or a non-friendly entity).</summary>
        ///***************************************************************************************************************

        public bool isFromPlayerShip()
        {
            return laserIsFromPlayerShip;
        }

        ///***********************************************************************************************************************
        ///<summary>Starts the playback of a randomly selected laser sound. You do not have to worry about stopping the sound once
        ///it is started as the sound will stop automatically when it reaches the end of the sound file.</summary>
        ///***********************************************************************************************************************

        public void playLaserSound()
        {
            int soundIndexToPlay = GameManager.randomNumberGenerator.Next(1, 4);

            switch(soundIndexToPlay)
            {
                case 1: laserSound1.playSound(false); break;
                case 2: laserSound2.playSound(false); break;
                case 3: laserSound3.playSound(false); break;
            }
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), false); // Boolean is for Wrapping
            laserLife++;

            if(laserLife >= 20)
                this.destroy();
        }

        ///*******************************************************************
        ///<summary>Destroys the Laser after it is no longer needed.</summary>
        ///*******************************************************************
        
        public override void destroy()
        {
            GameManager.remove2DEntity(this);
        }

    }
}