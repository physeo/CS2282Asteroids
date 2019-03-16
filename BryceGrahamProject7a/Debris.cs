using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Debris.cs
 * Autors: Rob Merrick
 * This class is used to create a debris entity to be used in
 * the parallaxing background effect.
 */

namespace BryceGrahamProject7a
{
    class Debris : Entity2D
    {

        //Static Variables
        private static float parallaxSpeed = 15.0F;

        ///************************************************************************************************************
        ///<summary>Creates a new instance of the Debris class. This instance will automatically generate random values
        ///for position, movement, and rotation.</summary>
        ///************************************************************************************************************
        
        public Debris()
        {
            setGraphic(new Graphic("Debris.png"));
            respawn();
        }

        ///*****************************************************************************************
        ///<summary>Changes the debris location to reappear at the top of the game window.</summary>
        ///*****************************************************************************************

        public void respawn()
        {
            Random random = GameManager.randomNumberGenerator;
            float zValue = (float) (0.01F + random.NextDouble() * 0.99F);
            setPosition(random.Next(0, (int) GameManager.canvas.ClipBounds.Width), random.Next(-200, -20), false);
            setRotationalSpeed((float) (random.NextDouble() * 360.0F));
            setScale(zValue, zValue);
            setSpeed(0.0F, zValue * parallaxSpeed);
        }

        ///****************************************************************************************
        ///<summary>Returns true if the debris has moved below the drawing screen region.</summary>
        ///****************************************************************************************

        public bool isReadyToBeDestroyed()
        {
            return getYLocation() > GameManager.canvas.ClipBounds.Height + 20;
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            setPosition(getXLocation(), getYLocation() + getYSpeed(), false);
            setRotation(getRotationalSpeed());
        }

        ///***************************************************************************************************************
        ///<summary>Sets the speed of the parallaxing effect. Pick a value between 1.0 and 30.0. Note that the change will
        ///be observed when the current debris moves below the screen height and is respawned.</summary>
        ///***************************************************************************************************************

        public static void setParallaxSpeed(float newSpeed)
        {
            if(newSpeed < 1.0 || newSpeed > 30.0F)
                throw new Exception("You probably don't want to set the parallax speed to that value. It would not look pretty.");

            parallaxSpeed = newSpeed;
        }

        ///*****************************************************
        ///<summary>Throws a NotImplementedExeption().</summary>
        ///*****************************************************

        public override void destroy()
        {
            throw new NotImplementedException();
        }

    }
}