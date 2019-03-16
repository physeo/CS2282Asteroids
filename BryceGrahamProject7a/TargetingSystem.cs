using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

/********************************************************
 * Targeting Systems class
 * by Bryce Graham
 * The targeting system for the ship that calculates
 * where the ship needs to aim in order to hit the center
 * of the asteroid
*********************************************************/

namespace BryceGrahamProject7a
{
    class TargetingSystem : Entity2D
    {
        private static PlayerShip ship;
        public TargetingSystem(PlayerShip _playerShip)
        {
            ship = _playerShip;
        }

        // Bryce Graham
        // Draws the blue line and red circle for the target
        public override void draw()
        {
            if (isEntityShown())
            {
                Pen bluePen = new Pen(Color.Blue, 5F);
                Pen redPen = new Pen(Color.Red, 5F);
                PointF point1;
                PointF point2;
                foreach (Entity2D a in GameManager.getAll2DEntities())
                {
                    if (a is Asteroid)
                    {
                        point1 = new PointF(a.getXLocation(), a.getYLocation());
                        point2 = calculateTarget(a);
                        GameManager.canvas.DrawLine(bluePen, point1, point2);
                        GameManager.canvas.DrawEllipse(redPen, point2.X - 10F, point2.Y - 10F, 20F, 20F);
                    }
                }
            }
        }

        // Bryce Graham
        // Calculates the point at which the target is drawn so that the laser
        // will intersect the asteroid when fired at the target.
        public PointF calculateTarget(Entity2D asteroid)
        {
            Laser laser = new Laser(false);
            PointF targetPoint;
            targetPoint = new PointF();

            float xDelta = asteroid.getXLocation() - ship.getXLocation();
            float yDelta = asteroid.getYLocation() - ship.getYLocation();
            float targetDistance = (float)(Math.Sqrt((xDelta * xDelta) + (yDelta * yDelta)));
            float correctedRotation = (float)((ship.getRotation() - 90) * Constants.DEGREES_TO_RADIANS);
            float laserXSpeed = (float)(Math.Cos(correctedRotation) * 50);
            float laserYSpeed = (float)(Math.Sin(correctedRotation) * 50);
            float timeToCollide = (float)(targetDistance / Math.Sqrt((laserXSpeed * laserXSpeed) + (laserYSpeed * laserYSpeed)));
            targetPoint.X = asteroid.getXLocation() + asteroid.getXSpeed() * timeToCollide;
            targetPoint.Y = asteroid.getYLocation() + asteroid.getYSpeed() * timeToCollide;

            return targetPoint;
        }

        public override void update()
        {

        }

        public override void destroy()
        {
            GameManager.remove2DEntity(this);
        }
    }
}
