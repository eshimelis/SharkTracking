using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUVState
{
    class Vector
    {
        private XYZCoordinate _vector;

        public Vector(double x, double y, double z)
        {
            _vector = new XYZCoordinate(x, y, z);
        }

        public Vector(XYZCoordinate xyzCord)
        {
            _vector = new XYZCoordinate(xyzCord);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns>The radian difference between two vectors. </returns>
        public double vectorAngle(Vector arg)
        {
            return
                Math.Acos(
                (_vector.X * arg._vector.X + _vector.Y * arg._vector.Y + _vector.Z * arg._vector.Z)
                / (Magnitude() * arg.Magnitude()));
        }

        public void Normalize()
        {
            double mag = Magnitude();
            _vector.X = _vector.X / mag;
            _vector.Y = _vector.Y / mag;
            _vector.Z = _vector.Z / mag;
        }

        public double Magnitude()
        {
            return Math.Pow(
                (Math.Pow(_vector.X, 2) + Math.Pow(_vector.Y, 2) + Math.Pow(_vector.Z, 2)), 0.5);
        }

        public override string ToString()
        {
            return _vector.ToString();
        }
    }
}
