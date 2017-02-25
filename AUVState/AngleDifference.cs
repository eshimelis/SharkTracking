using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUVState
{
    class AngleDifference
    {

        // Angle difference in degrees, bounded it between -180 and 180
        public static double angleDiffDegrees(double a, double b)
        {
            double d = a - b;
            while (d > 180)
            {
                d = d - 360;
            }
            while (d < -180)
            {
                d = d + 360;
            }

            return d;
        }

        // Angle difference in radians, bounded between -pi to pi
        public static double angleDiff(double a, double b)
        {
            double d = a - b;
            while (d > Math.PI)
            {
                d = d - (2 * Math.PI);
            }
            while (d < -Math.PI)
            {
                d = d + (2 * Math.PI);
            }

            return d;
        }
    }
}
