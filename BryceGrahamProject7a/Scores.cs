using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Scores.cs
 * Autors: Rob Merrick
 * This class is used to keep track of the scoring system. This
 * is a static-only class so that it can be accessed anywhere in
 * the program.
 */

namespace BryceGrahamProject7a
{
    class Scores
    {

        //Scoring System Constants
        public const int FIRE_BULLET_POINTS = 1;
        public const int LARGE_ASTEROID_POINTS = 50;
        public const int MEDIUM_ASTEROID_POINTS = 75;
        public const int SMALL_ASTEROID_POINTS = 100;
        public const int LARGE_ENEMY_SHIP_POINTS = 150;
        public const int SMALL_ENEMY_SHIP_POINTS = 200;
        public const int GAIN_LIFE_THRESHOLD = 20000;
        public const int SMALL_SAUCER_APPEARS_THRESHOLD = 40000;

        //Variable declarations
        private static int currentScore = 0;
        private static int numberOfLivesAwarded = 1;
        private static int highScore = 0;

        ///**********************************************************************************
        ///<summary>Do not allow public instantiation. This is a static-only class.</summary>
        ///**********************************************************************************

        private Scores()
        {
            //Empty
        }

        ///***********************************************************************
        ///<summary>Resets the scoring system back to its initial state.</summary>
        ///***********************************************************************

        public static void initialize()
        {
            currentScore = 0;
            numberOfLivesAwarded = 1;
        }

        ///*******************************************************
        ///<summary>Sets the score for the current game.</summary>
        ///*******************************************************

        public static void setScore(int points)
        {
            if(points < 0)
                throw new Exception("When settings the score, the value must be a non-negative integer.");

            currentScore = points;
        }

        ///**********************************************************************************************************************
        ///<summary>Adds points to the current score. Try to make the points follow the public constants in this class.</summary>
        ///**********************************************************************************************************************

        public static void addPoints(int points)
        {
            if(points < 1)
                throw new Exception("When adding points to the current score, the value provided must be a positive integer.");

            currentScore += points;

            if(currentScore > highScore)
                highScore = currentScore;
        }

        ///**************************************************************************************************
        ///<summary>Call this method after the GameManager has awarded the player with a new life so that the
        ///next score for the next new life threshold can be calculated.</summary>
        ///**************************************************************************************************

        public static void calculateNextNewLifeScore()
        {
            ++numberOfLivesAwarded;
        }

        ///*******************************************************************************************
        ///<summary>Returns true if the player has accumulated enough points for a new life.</summary>
        ///*******************************************************************************************

        public static bool shouldAwardNewLife()
        {
            return currentScore >= numberOfLivesAwarded * GAIN_LIFE_THRESHOLD;
        }

        ///***********************************************************
        ///<summary>Returns the current score in the system.</summary>
        ///***********************************************************

        public static int getCurrentScore()
        {
            return currentScore;
        }

        ///********************************************************
        ///<summary>Returns the high score in the system.</summary>
        ///********************************************************

        public static int getHighScore()
        {
            return highScore;
        }

    }
}