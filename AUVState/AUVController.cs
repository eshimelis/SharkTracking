using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

using AUVState;

namespace AUVState
{
    class AUVController
    {
        // General Control Variables
        private double _rDes;   //Yaw rate   (rad/s?)
        private double _uDes;   //Serge      (m/s)
        private double _qDes;   //Pitch rate (rad/s?)
        private double _pDes;   //Roll rate  (rad/s?)
        private double _depthDes;
        private string _backseatCommand;
        private AUVState _remoteAuvState;

        private XYZCoordinate _lastTargetFix;

        // Timing variables
        private double DELTA_T = 0.5;   // Seconds

    }
}
