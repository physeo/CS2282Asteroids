using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

/********************************************************
 * RocketFlame class
 * by Bryce Graham
 * Creates a RocketFlame for the rear of the ship when
 * accelerating
*********************************************************/

namespace BryceGrahamProject7a
{
    class RocketFlame : Entity2D
    {
        private bool shipIsAccelerating;
        System.Diagnostics.Stopwatch flameEffectTimer;

        public RocketFlame()
        {
            flameEffectTimer = new System.Diagnostics.Stopwatch();
            flameEffectTimer.Start();
        }

        public bool ShipIsAccelerating
        {
            get { return shipIsAccelerating; }
            set { shipIsAccelerating = value; }
        }

        // Bryce Graham
        // Draws the rocket flames in accordance with the location and rotation of the ship
        public override void draw()
        {
            if (isEntityShown())
            {
                SolidBrush blueBrush = new SolidBrush(Color.Blue);
                PointF upperLeft = new PointF(this.getXLocation() - 15F, this.getYLocation() + 20F);
                PointF upperRight = new PointF(this.getXLocation() + 15F, this.getYLocation() + 20F);
                PointF lowerLeft = new PointF(this.getXLocation(), this.getYLocation() + 50F);
                PointF middle = new PointF(this.getXLocation(), this.getYLocation() + 25F);
                PointF[] newPoints = { upperLeft, middle, upperRight, lowerLeft };
                Matrix transformMatrix = new Matrix();
                transformMatrix.RotateAt(this.getRotation(), new PointF(this.getXLocation(), this.getYLocation()));
                transformMatrix.TransformPoints(newPoints);
                GameManager.canvas.FillPolygon(blueBrush, newPoints);
            }
        }


        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        // Bryce Graham
        // Destroys the rocket flame if the ship is no longer accelerating
        public override void update()
        {
            setPosition(getXLocation(), getYLocation(), false);

            if (flameEffectTimer.ElapsedMilliseconds % 10 == 0)
                this.setEntityIsShown(false);
            else this.setEntityIsShown(true);
            if (!shipIsAccelerating)
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
