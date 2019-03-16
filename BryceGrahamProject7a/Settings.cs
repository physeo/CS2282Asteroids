using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Settings.cs
 * Autors: Rob Merrick
 * This class is used to manage gameplay settings.
 */

/********************************************************
 * Settings class
 * by Bryce Graham
 * I am going to add settings to allow for mouse control,
 * keyboard control or a combination. I will also provide
 * options for a debugging mode with the bounding boxes
 * made visible and to turn the targeting system on or off
*********************************************************/

namespace BryceGrahamProject7a
{
    class Settings
    {

        //Variable Declarations
        private static bool useMouseControls = true;
        private static bool debugMode = false;

        ///**********************************************************************************
        ///<summary>Do not allow public instantiation. This is a static-only class.</summary>
        ///**********************************************************************************
        
        private Settings()
        {

        }

        ///***************************************************************************************************************
        ///<summary>Sets the static value which indicates whether or not the user is going to use the mouse to control the
        ///spaceship. If set to false, keyboard controls will be used.</summary>
        ///***************************************************************************************************************

        public static void setUseMouseControls(bool useMouse)
        {
            useMouseControls = useMouse;
        }

        ///***************************************************************************************************************
        ///<summary>Returns true if the user has selected to use the mouse to control the spaceship, false if the keyboard
        ///should be used instead.</summary>
        ///***************************************************************************************************************

        public static bool getUseMouseControls()
        {
            return useMouseControls;
        }

        public static bool DebugMode
        {
            get { return debugMode; }
            set { debugMode = value; }
        }

    }
}