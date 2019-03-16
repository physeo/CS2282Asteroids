using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * EnemyShip.cs
 * Autors: Rob Merrick
 * This class emulates an enemy ship (both large and small) that
 * will attack the player's ship.
 */

namespace BryceGrahamProject7a
{
    class EnemyShip : Enemy
    {

        //Variable declarations
        private bool isSmallShip;
        private stateOfBeing currentState;
        private Sound blowUpSound;
        private Sound thrusterSound;

        ///***************************************************************************************
        ///<sumary>Use this enumeration to represent the current state of the enemy ship.</sumary>
        ///***************************************************************************************

        private enum stateOfBeing
        {
            attack, pursue, retreat, idle, die
        };

        ///*******************************************************************
        ///<summary>Creates a new instance of the EnemyShip() class.</summary>
        ///*******************************************************************

        public EnemyShip()
        {

        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public override void update()
        {
            //Place update code here.
            updateWithRespectToPlayer();
        }

        ///**************************************************************************************************
        ///<summary>Increases the current score by the set amount of points for a small enemy ship.</summary>
        ///**************************************************************************************************

        protected override void awardPoints()
        {
            Scores.addPoints(Scores.SMALL_ENEMY_SHIP_POINTS);
        }

        ///************************************************************************
        ///<summary>Call this method if the enemy ship is in attack mode.</summary>
        ///************************************************************************

        private void attack()
        {

        }

        ///************************************************************************
        ///<summary>Call this method if the enemy ship is in pursue mode.</summary>
        ///************************************************************************

        private void pursue()
        {

        }

        ///*************************************************************************
        ///<summary>Call this method if the enemy ship is in retreat mode.</summary>
        ///*************************************************************************

        private void retreat()
        {

        }

        ///**********************************************************************
        ///<summary>Call this method if the enemy ship is in idle mode.</summary>
        ///**********************************************************************

        private void idle()
        {

        }

        ///*********************************************************************
        ///<summary>Call this method if the enemy ship is in die mode.</summary>
        ///*********************************************************************

        private void die()
        {

        }

    }
}