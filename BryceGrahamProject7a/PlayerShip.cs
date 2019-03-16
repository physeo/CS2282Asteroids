using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * PlayerShip.cs
 * Autors: Rob Merrick
 * This class holds all of the information regarding the player's
 * spaceship.
 */

/********************************************************
 * PlayerShip class
 * by Bryce Graham
 * Added fire animation, temporary invulnerability
 * and changed the laser dynamics
*********************************************************/

namespace BryceGrahamProject7a
{
    class PlayerShip : Entity2D
    {

        //Variable Declarations
        private bool startedThrusterSound;
        private SpecialEffects blowUpAnimation;
        private bool invulnerable;
        private System.Diagnostics.Stopwatch LaserTimer;

        //Static Variables
        private static Sound thrusterSound;

        ///******************************************************************
        ///<summary>Creates a new instance of the PlayerShip class.</summary>
        ///******************************************************************
        
        public PlayerShip()
        {
            LaserTimer = new System.Diagnostics.Stopwatch();
            LaserTimer.Start();
            Invulnerable = true;
            startedThrusterSound = false;
            setGraphic(new Graphic("Falcon.png"));
            float positionX = (GameManager.canvas.ClipBounds.Width + GameManager.canvasOrigin.X) / 2.0F;
            float positionY = (GameManager.canvas.ClipBounds.Height + GameManager.canvasOrigin.Y) / 2.0F;
            setPosition(positionX, positionY, true);
            setMaximumSpeed(10.0F);
            thrusterSound = new Sound("Thruster.wav");
        }

        // Provides an invulnerability option for the ship in order
        // to prevent it from being instantly destroyed at the beginning
        public bool Invulnerable
        {
            get { return invulnerable; }
            set { invulnerable = value; }
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        // Bryce Graham
        // Only allows ship to be destroyed if invulnerable is false
        public override void update()
        {
            if (LaserTimer.ElapsedMilliseconds > 300)
                LaserTimer.Stop();
            if(blowUpAnimation == null)
            {
                if(Settings.getUseMouseControls())
                    controlShipWithMouse();
                else
                    controlShipWithKeyboard();

                setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), true);
            }
            else if(!Invulnerable)
            {
                int currentFrame = blowUpAnimation.drawExplosion(getXLocation(), getYLocation(), getRotation(), getScaleX(), getScaleY());
                
                if(currentFrame >= SpecialEffects.EXPLOSION_KEY_FRAME)
                    this.setEntityIsShown(false);

                if(currentFrame >= SpecialEffects.EXPLOSION_FRAME_COUNT + 10)
                    GameManager.killPlayer();
            }
        }

        ///******************************************************************
        ///<summary>Destroys the ship after it is no longer needed.</summary>
        ///******************************************************************
        
        public override void destroy()
        {
            if(!Invulnerable)
            if(blowUpAnimation == null)
            {
                blowUpAnimation = new SpecialEffects();
                blowUpAnimation.setUpExplosion(true);
            }
        }

        ///****************************************************
        ///<summary>Controls the ship with the mouse.</summary>
        ///****************************************************

        // Bryce Graham
        // Slows down the rate of fire of the laser
        private void controlShipWithMouse()
        {
            float mouseX = System.Windows.Forms.Cursor.Position.X - GameManager.canvasOrigin.X;
            float mouseY = System.Windows.Forms.Cursor.Position.Y - GameManager.canvasOrigin.Y;
            pointEntity(mouseX + 20, mouseY);

            if(UserControls.getLeftMouseDown())
                accelerateShip();
            else
                decelerateShip();

            if(UserControls.getRightMouseDown())
            {
                if(LaserTimer.ElapsedMilliseconds > 250)
                {
                    fireLaser();
                    LaserTimer.Restart();
                }
            }
        }

        ///*******************************************************
        ///<summary>Controls the ship with the keyboard.</summary>
        ///*******************************************************

        private void controlShipWithKeyboard()
        {

        }

        ///************************************************************************************************************
        ///<summary>Increases the ship speed in the direction it is pointed unless it's at its maximum speed.</summary>
        ///************************************************************************************************************

        // Bryce Graham
        // Added a rocket flame to the back of the ship when it accelerates
        private void accelerateShip()
        {
            RocketFlame rocketFlame = new RocketFlame();
            rocketFlame.ShipIsAccelerating = true;
            showFlames(rocketFlame);

            float correctedRotation = (float) ((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float) (getXSpeed() + Math.Cos(correctedRotation) * 0.75);
            float ySpeed = (float) (getYSpeed() + Math.Sin(correctedRotation) * 0.75);
            float maximumSpeed = getMaximumSpeed();

            if(xSpeed > maximumSpeed)
                xSpeed = maximumSpeed;

            if(xSpeed < -maximumSpeed)
                xSpeed = -maximumSpeed;

            if(ySpeed > maximumSpeed)
                ySpeed = 10.0F;

            if(ySpeed < -maximumSpeed)
                ySpeed = -maximumSpeed;

            setSpeed(xSpeed, ySpeed);

            rocketFlame.ShipIsAccelerating = false;
        }

        ///**************************************************************************************************************
        ///<summary>Decreases the ship speed unless it's close to zero, in which case the speed is set to zero.</summary>
        ///**************************************************************************************************************

        private void decelerateShip()
        {
            startedThrusterSound = false;
            float xSpeed = getXSpeed() / 1.025F;
            float ySpeed = getYSpeed() / 1.025F;

            if(Math.Abs(xSpeed) < 0.01)
                xSpeed = 0.0F;

            if(Math.Abs(ySpeed) < 0.01)
                ySpeed = 0.0F;

            setSpeed(xSpeed, ySpeed);
        }

        ///****************************************************************************************************************************
        ///<summary>Creates a new laser to add to the lasers list, setting the initial fields so that it fires from the ship.</summary>
        ///****************************************************************************************************************************

        private void fireLaser()
        {
            Scores.addPoints(Scores.FIRE_BULLET_POINTS); //Yay for pity points!
            Laser laser = new Laser(true);
            laser.playLaserSound();
            laser.setRotation(getRotation());
            float correctedRotation = (float) ((getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float xSpeed = (float) (Math.Cos(correctedRotation) * 50);
            float ySpeed = (float) (Math.Sin(correctedRotation) * 50);
            laser.setSpeed(xSpeed, ySpeed);
            laser.setPosition(getXLocation() + laser.getXSpeed() * 0.85F, getYLocation() + laser.getYSpeed() * 0.85F, false);
            GameManager.add2DEntity(laser);
        }

        // Bryce Graham
        // Creates rocket flames from the rear of the ship
        private void showFlames(RocketFlame rocketFlame)
        {
            if (!startedThrusterSound)
            {
                thrusterSound.playSound(false);
                startedThrusterSound = true;
            }

            rocketFlame.setCollisionRadius(0F);
            rocketFlame.setRotation(getRotation());
            rocketFlame.setPosition(getXLocation(), getYLocation(), false);
            GameManager.add2DEntity(rocketFlame);
            rocketFlame.setEntityIsShown(true);

        }
    }
}