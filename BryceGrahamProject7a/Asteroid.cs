using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Asteroid.cs
 * Autors: Rob Merrick
 * This class is used to create and manage the asteroids
 * (large and small) for each level.
 */


namespace BryceGrahamProject7a
{
    class Asteroid : Enemy
    {

        //Variable Declarations
        private SIZE asteroidSize;

        public enum SIZE
        {
            SMALL, MEDIUM, LARGE
        }

        ///***************************************************************************************************************************
        ///<summary>This private constructor is called to set up the values based on the public constructor that was called.</summary>
        ///***************************************************************************************************************************

        private Asteroid(Asteroid.SIZE asteroidSize, bool useStartLocation, float xLocation, float yLocation)
        {
            this.asteroidSize = asteroidSize;
            setGraphic(new Graphic("Asteroid.png"));
            generateInitialValues(useStartLocation, xLocation, yLocation);

            switch(asteroidSize)
            {
                case SIZE.SMALL:
                    setScale(0.25F, 0.25F);
                    setCollisionRadius(35.0F);
                    break;
                case SIZE.MEDIUM:
                    setScale(0.5F, 0.5F);
                    setCollisionRadius(60.0F);
                    break;
                case SIZE.LARGE:
                    setScale(1.0F, 1.0F);
                    setCollisionRadius(100.0F);
                    break;
            }
        }

        ///***************************************************************************************
        ///<summary>Creates a new instance of the Asteroid class with the provided size.</summary>
        ///***************************************************************************************

        public Asteroid(Asteroid.SIZE asteroidSize) : this(asteroidSize, false, -1.0F, -1.0F)
        {
            //Empty
        }

        public Asteroid(Asteroid.SIZE asteroidSize, float xLocation, float yLocation) : this(asteroidSize, true, xLocation, yLocation)
        {
            //Empty
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            setRotation(getRotation() + getRotationalSpeed());
            setPosition(getXLocation() + getXSpeed(), getYLocation() + getYSpeed(), true);
            updateWithRespectToPlayer();
        }

        ///*************************************************************************************************
        ///<summary>Increases the current score by the set amount of points for a medium asteroid.</summary>
        ///*************************************************************************************************

        protected override void awardPoints()
        {
            switch(asteroidSize)
            {
                case SIZE.SMALL:
                    Scores.addPoints(Scores.SMALL_ASTEROID_POINTS);
                    break;
                case SIZE.MEDIUM:
                    Scores.addPoints(Scores.MEDIUM_ASTEROID_POINTS);
                    GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation()));
                    GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation()));
                    GameManager.add2DEntity(new Asteroid(SIZE.SMALL, getXLocation(), getYLocation()));
                    break;
                case SIZE.LARGE:
                    Scores.addPoints(Scores.LARGE_ASTEROID_POINTS);
                    GameManager.add2DEntity(new Asteroid(SIZE.MEDIUM, getXLocation(), getYLocation()));
                    GameManager.add2DEntity(new Asteroid(SIZE.MEDIUM, getXLocation(), getYLocation()));
                    break;
            }
        }

        ///****************************************************************************************************************
        ///<summary>Creates a random location and velocity for the asteroid. If useStartLocation is true, then the provided
        ///start location values will be used for the initial position instead of randomly generating one.</summary>
        ///****************************************************************************************************************

        private void generateInitialValues(bool useStartLocation, float xLocation, float yLocation)
        {
            Random random = GameManager.randomNumberGenerator;

            if(useStartLocation)
                setPosition(xLocation, yLocation, true);
            else
            {
                int screenWidth = (int) (GameManager.canvas.ClipBounds.Width - GameManager.canvasOrigin.X);
                int screenHeight = (int) (GameManager.canvas.ClipBounds.Height - GameManager.canvasOrigin.Y);
                float centerScreenX = (GameManager.canvas.ClipBounds.Width + GameManager.canvasOrigin.X) / 2.0F;
                float centerScreenY = (GameManager.canvas.ClipBounds.Height + GameManager.canvasOrigin.Y) / 2.0F;
                float positionX = (float) (Math.Pow(-1, random.Next(1, 3)) * random.Next(200, screenWidth));
                float positionY = (float) (Math.Pow(-1, random.Next(1, 3)) * random.Next(200, screenHeight));
                setPosition(centerScreenX + positionX, centerScreenY + positionY, true);
            }

            setSpeed( (float) (-5.0 + random.NextDouble() * 15.0), (float) (-5.0 + random.NextDouble() * 15.0));
            setRotationalSpeed( (float) (-5.0 + random.NextDouble() * 10.0) );
        }

    }
}