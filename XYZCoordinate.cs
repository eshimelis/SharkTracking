using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace State
{
    public struct XYZCoordinate
    {
        private double x_;
        private double y_;
        private double z_;

        public XYZCoordinate(XYZCoordinate copy)
        {
            x_ = copy.X;
            y_ = copy.Y;
            z_ = copy.Z;
        }

        public XYZCoordinate(double x, double y, double z)
        {
            this.x_ = x;
            this.y_ = y;
            this.z_ = z;
        }

        public void Move(double x, double y, double z)
        {
            x_ += x;
            y_ += y;
            z_ += z;
        }

        public double DistanceTo(XYZCoordinate arg)
        {
            return Math.Pow((Math.Pow(x_ - arg.X, 2) +
                             Math.Pow(y_ - arg.Y, 2) +
                             Math.Pow(z_ - arg.Z, 2)), 0.5);
        }

        public override string ToString()
        {
            return x_ + ", " + y_ + ", " + z_;
        }

        /// <summary>
        /// True if x, y and z are all equal
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool Equals(XYZCoordinate coord)
        {
            return coord.x_ == x_ && coord.y_ == y_ && coord.z_ == z_;
        }

        public double X
        {
            get { return x_; }
            set { x_ = value; }
        }

        public double Y
        {
            get { return y_; }
            set { y_ = value; }
        }

        public double Z
        {
            get { return z_; }
            set { z_ = value; }
        }

    }
}
