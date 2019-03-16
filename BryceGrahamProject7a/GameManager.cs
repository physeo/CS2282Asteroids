using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

/**
 * GameManager.cs
 * Autors: Rob Merrick
 * This class is used to manage all of the gampeplay items, 
 * including updating and drawing objects to the screen.
 */

/********************************************************
 * Game Manager class
 * by Bryce Graham
 * Added a pause function
 * Slowed down the fire rate
 * Created a targeting system
*********************************************************/

namespace BryceGrahamProject7a
{
    class GameManager
    {

        //Variable declarations
        private static bool finishedTheGame;
        private static int livesLeft;
        private static PlayerShip player1Ship;
        private static Environment starField;
        private static List<Entity2D> entity2DList;
        private static List<Entity2D> newEntitiesToAdd;
        private static List<Entity2D> oldEntitiesToRemove;
        private static TargetingSystem targetSystem;
        private static GUI gui;
        private static Sound music;
        private static Sound pauseSound;
        private static System.Diagnostics.Stopwatch invulnerabilityTimer;

        //Public variables;
        public static PointF canvasOrigin;
        public static Graphics canvas;
        public static Random randomNumberGenerator = new Random();

        ///*******************************************************************
        ///<summary>Creates a new instance of the GameManager class.</summary>
        ///*******************************************************************

        public GameManager()
        {
            //Empty
        }

        ///******************************************************************************************************************
        ///<summary>This method works like a constructor, but it must be called after the GameManager was created. Otherwise,
        ///many methods that depend on the Graphics object to paint on will fail.</summary>
        ///******************************************************************************************************************

        public void initialize()
        {
            invulnerabilityTimer = new System.Diagnostics.Stopwatch();
            finishedTheGame = false;
            livesLeft = 3;
            newEntitiesToAdd = new List<Entity2D>();
            oldEntitiesToRemove = new List<Entity2D>();
            gui = new GUI();
            Scores.initialize();
            Level.setCurrentLevel(1);
            music = new Sound("Music\\Game Music Looped.wav");
            music.playSound(true);
            music.setVolume(0.5);
            pauseSound = new Sound("");
            Voices.intitialize();
            reloadLevel();
        }

        ///*************************************************************************************************
        ///<summary>Clears the entire screen to black. Call this once prior to drawing each frame.</summary>
        ///*************************************************************************************************

        public void clearScreen()
        {
            canvas.Clear(Color.Black);
        }

        ///*************************************************************************************
        ///<summary>Draws every object on the screen. Call this once per frame update.</summary>
        ///*************************************************************************************

        public void drawFrame()
        {
            //starField.draw();
            foreach (Entity2D entity in entity2DList)
            {
                entity.draw();
                if(Settings.DebugMode)
                    entity.showCollisionRadius();
            }
            if (UserControls.TargetingButtonPushed)
                targetSystem.draw();

            SpecialEffects.drawAllSpecialEffects();
            gui.drawGUI(livesLeft, invulnerabilityTimer.ElapsedMilliseconds);
        }

        ///********************************************************************************************
        ///<summary>Updates every object in the game. Call this prior to calling drawFrame().</summary>
        ///********************************************************************************************

        public void updateFrame()
        {
            if (!UserControls.PauseButtonPushed)
            {
                invulnerabilityTimer.Start();
                //starField.update();
                if (invulnerabilityTimer.ElapsedMilliseconds > 3000)
                {
                    player1Ship.Invulnerable = false;
                    invulnerabilityTimer.Stop();
                }

                foreach (Entity2D entity in entity2DList)
                    entity.update();

                updateEntity2DList();

                if (Scores.shouldAwardNewLife())
                {
                    Voices.extraLifeAwarded.playSound(false);
                    ++livesLeft;
                    Scores.calculateNextNewLifeScore();
                }

                if (isEveryEnemyDefeated())
                {
                    Voices.levelComplete.playSound(false);
                    Level.setCurrentLevel(Level.getCurrentLevel() + 1);
                    reloadLevel();
                }
            }
            else invulnerabilityTimer.Stop();
        }

        ///****************************************************
        ///<summary>Returns true if the game is over.</summary>
        ///****************************************************

        public bool isGameOver()
        {
            return finishedTheGame;
        }

        ///****************************************************************************************************************
        ///<summary>Updates the needed information to kill the player (take away a life) and reset the level. If the user's
        ///remaining lives drops below 1, the game over screen is displayed.</summary>
        ///****************************************************************************************************************

        public static void killPlayer()
        {
            --livesLeft;

            if (livesLeft > 0)
            {
                Voices.yourShipHasBeenDestroyed.playSound(false);
                Thread.Sleep(3000);
                reloadLevel();
            }
            else
                gameOver(false);
        }

        ///********************************************************************************************************
        ///<summary>Returns a read-only list of 2D entities that are currently stored in the GameManager.</summary>
        ///********************************************************************************************************

        public static IReadOnlyCollection<Entity2D> getAll2DEntities()
        {
            return entity2DList.AsReadOnly();
        }

        ///**************************************************************************************************************
        ///<summary>Add the provided Entity2D to the GameManager's list so that it may be included on the next draw() and
        ///update() calls for the game.</summary>
        ///**************************************************************************************************************

        public static void add2DEntity(Entity2D entity)
        {
            newEntitiesToAdd.Add(entity);
        }

        ///***************************************************************************************************************
        ///<summary>Call this method if you need to remove any entity from the GameManager's entity list. This change will
        ///be reflected beginning on the next draw() and update() calls.</summary>
        ///***************************************************************************************************************

        public static void remove2DEntity(Entity2D entity)
        {
            oldEntitiesToRemove.Add(entity);
        }

        ///*******************************************************************************************************************
        ///<summary>If any entities were added or removed during the last update, the GameManager's entity2DList is updated to 
        ///reflect the new changes by calling this method.</summary>
        ///*******************************************************************************************************************

        private void updateEntity2DList()
        {
            foreach(Entity2D entity in oldEntitiesToRemove)
                entity2DList.Remove(entity);

            foreach(Entity2D entity in newEntitiesToAdd)
                entity2DList.Add(entity);

            oldEntitiesToRemove.Clear();
            newEntitiesToAdd.Clear();
        }

        ///**************************************************************************************************************
        ///<summary>Returns true if every entity that is of type Enemy has been removed from the entity2D list.</summary>
        ///**************************************************************************************************************

        private bool isEveryEnemyDefeated()
        {
            foreach(Entity2D entity in entity2DList)
                if(entity is Enemy)
                    return false;

            return true;
        }

        ///*******************************************************************************************************************
        ///<summary>Resets everything back to its initial state when the level began. Note that the order in which 2DEntities 
        ///are added to the GameManager's entity2DList is important. Entities will be drawn in the order they are added to the
        ///list. This means that the first entity added will appear below every other entity drawn on the screen.</summary>
        ///*******************************************************************************************************************

        private static void reloadLevel()
        {
            invulnerabilityTimer.Reset();
            invulnerabilityTimer.Start();
            //starField = new Environment();
            player1Ship = new PlayerShip();
            targetSystem = new TargetingSystem(player1Ship);
            entity2DList = new List<Entity2D>();

            for(int i = 0; i < Level.getInitialNumberOfLargeAsteroids(); i++)
                entity2DList.Add(new Asteroid(Asteroid.SIZE.LARGE));

            for(int i = 0; i < Level.getInitialNumberOfMediumAsteroids(); i++)
                entity2DList.Add(new Asteroid(Asteroid.SIZE.MEDIUM));

            for(int i = 0; i < Level.getInitialNumberOfSmallAsteroids(); i++)
                entity2DList.Add(new Asteroid(Asteroid.SIZE.SMALL));

            //for(int i = 0; i < Level.getInitialNumberOfLargeEnemyShips(); i++)
            //    entity2DList.Add(new EnemyShip(false));

            //for(int i = 0; i < Level.getInitialNumberOfSmallEnemyShips(); i++)
            //    entity2DList.Add(new EnemyShip(true));

            entity2DList.Add(player1Ship);
            Enemy.setPlayerShip(player1Ship);
        }

        ///************************************************************************************************************
        ///<summary>Runs the Game-Over routine. During this process, all scores, levels, and other relevant static data
        ///is reset to its initial value.</summary>
        ///************************************************************************************************************
        
        private static void gameOver(bool playerFinishedTheLastLevel)
        {
            for(int i = 0; i < 100; i++)
            {
                music.setVolume(Math.Max(0.5 - i/100.0, 0));
                System.Threading.Thread.Sleep(10);
            }
            
            Level.setCurrentLevel(1);
            Scores.setScore(0);
            music.stopSound();
            finishedTheGame = true;
            Voices.gameOver.playSound(false);
            Thread.Sleep(3000);
        }

    }
}