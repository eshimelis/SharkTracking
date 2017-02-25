using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AUVState;

namespace AUVState
{
    public class AUVStateSim : AUVState
    {

        // Prop speed percent
        public const double DELTA_T = 0.5;  // Seconds
        public const double SPEED_CONSTANT = 0.1;
        public const double Kp_ANG = 2;

        private XYZCoordinate _positionPrev;
        private XYZCoordinate _velocityPrev;
        private XYZCoordinate _accelerationPrev;

        private double _headingPrev;

        public double _propSpeed = 0.15;
        public double _rudderAngle;

        private XYZCoordinate _prevAngVelocity;
        private XYZCoordinate _angVelocity;

        // Constructor
        public AUVStateSim(string name = "null",
                         double[] initLatLong = null,
                         double[] latLong = null,
                         double heading = 0, double depth = 0,
                         XYZCoordinate position = new XYZCoordinate(),
                         XYZCoordinate velocity = new XYZCoordinate(),
                         XYZCoordinate acceleration = new XYZCoordinate())
        {

            _name = name;

            _initLatLong = initLatLong == null ? DEFAULT_LATLONG : initLatLong;
            _latLong = latLong == null ? DEFAULT_LATLONG : latLong;

            _position = position;
            _velocity = velocity;
            _acceleration = acceleration;

            _heading = heading;
            _depth = depth;

        }

        // Simulated state 
        public bool UpdateState(double rudderAngle, double propSpeed)
        {

            // Update propspeed
            _propSpeed = propSpeed;

            // Update heading
            _angVelocity.Z = _angVelocity.Z - _propSpeed * SPEED_CONSTANT * (rudderAngle) * DELTA_T;
            _heading = _headingPrev + _angVelocity.Z * DELTA_T;

            // Update velocity in x and y
            _velocity.X = _propSpeed * SPEED_CONSTANT * Math.Cos(_heading);
            _velocity.Y = _propSpeed * SPEED_CONSTANT * Math.Sin(_heading);
            _velocity.Z = 0 * SPEED_CONSTANT;
            _depth = 0;

            // Update current position in x and y
            _position.X = _positionPrev.X + _velocity.X * DELTA_T;
            _position.Y = _positionPrev.Y + _velocity.Y * DELTA_T;
            _position.Z = 0;

            // Update previous values
            _positionPrev = _position;
            _velocityPrev = _velocity;
            _accelerationPrev = _acceleration;

            return true;
        }

        public double calcRudderAng(XYZCoordinate positionDes)
        {

            // Rudder angle in radians            
            double angOffset = Math.Atan2(positionDes.Y - _position.Y, positionDes.X - _position.X);

            _rudderAngle = Kp_ANG * (AngleDifference.angleDiff(angOffset, _heading));

            if (_rudderAngle > Math.PI/4)
            {
                _rudderAngle = Math.PI / 4;
            } else if(_rudderAngle < -Math.PI/4)
            {
                _rudderAngle = -Math.PI / 4;
            }

            return _rudderAngle;
        }
    }
}
