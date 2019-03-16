using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/********************************************************
 * Bomb class
 * by Bryce Graham
 * Describes the behavior of the Missile weapon
*********************************************************/

namespace BryceGrahamProject7a
{
    class Missile : Laser
    {
        public Missile(bool laserIsFromPlayerShip)
            :base(laserIsFromPlayerShip)
        {

        }
    }
}
