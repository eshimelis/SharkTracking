using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

using AUVState;
using System.IO;

namespace AUVState
{

    public class AUVState       
    {
        // AUV name
        protected string _name;

        // GPS
        protected double[] _initLatLong;
        protected double[] _latLong;
        protected double[] DEFAULT_LATLONG = new double[2] {0, 0};

        // Positional information
        protected XYZCoordinate _position;
        protected XYZCoordinate _velocity;
        protected XYZCoordinate _acceleration;

        // Additional data
        protected double _heading;
        protected double _depth;

        private enum servoStates { topFin, botFin, portFin, starFin, motor }
        private enum compassStates { compHeading, pitch, roll, compTemp, depth }
        private List<int> _servoStates;
        private List<double> _compassStates;
        private List<int> DEFAULT_LIST_INT = new List<int>(new[] { 0, 0, 0, 0, 0 });
        private List<double> DEFAULT_LIST_DOUBLE = new List<double>(new[] { 0.0, 0.0, 0.0, 0.0, 0.0 });

        // For debugging purposes
        protected bool _succesfulUpdate;

        // Serial communication
        protected SerialPort _serialPort;
        protected string _portName = "COM4";
        protected int _baudRate = 9600;
        //private Parity _portParity;
        //private Handshake _portHandshake;

        private string _mode;
        private string _errorState;

        private ackMessage _ackMsg;
        private ackMessage DEFAULT_ACKMESSAGE = new ackMessage("$ACK,0,0,0,Default,0,0,0*0");
        
        // Constructor
        public AUVState (string name = "null",
                         double[] initLatLong = null,
                         double[] latLong = null,
                         double heading = 0, double depth = 0,
                         XYZCoordinate position = new XYZCoordinate(),
                         XYZCoordinate velocity = new XYZCoordinate(),
                         XYZCoordinate acceleration = new XYZCoordinate(),
                         List<int> servoStates = null,
                         List<double> compassStates = null,
                         string mode = "",
                         string errorState = "",
                         ackMessage ackMsg = null)
        {

            _name = name;

            _initLatLong= initLatLong == null ? DEFAULT_LATLONG : initLatLong;
            _latLong = latLong == null ? DEFAULT_LATLONG : latLong;

            _position = position;
            _velocity = velocity;
            _acceleration = acceleration;

            _heading = heading;
            _depth = depth;

            _servoStates = servoStates == null ? DEFAULT_LIST_INT : servoStates;
            _compassStates = compassStates == null ? DEFAULT_LIST_DOUBLE : compassStates;

            _mode = mode;
            _errorState = errorState;

            _ackMsg = ackMsg == null ? DEFAULT_ACKMESSAGE : ackMsg;

            _serialPort = new SerialPort();

            this.SerialSetup();
        }

        // Setup serial communication with Ocean Server software
        private void SerialSetup()
        {
            // Setup serial communication
            this._serialPort.PortName = this._portName;
            this._serialPort.BaudRate = this._baudRate;

            // Add here
        }

        // Reads sensors and updates current AUV state
        // Returns true if entire state is succesfully updated
        public bool UpdateState()
        {
            
            this._succesfulUpdate = true;

            // Update Helper functions (Possibly different things to update)
            this.UpdatePosition();
            this.UpdateVelocity();
            this.UpdateHeading();
            this.UpdateDepth();

            return this._succesfulUpdate;
        }

        // 
        public void UpdatePosition()
        {
            // Add code 
        }

        //
        public void UpdateVelocity()
        {
            // Add code
        }

        //
        public void UpdateAcceleration()
        {
            // Add code
        }

        //
        public void UpdateHeading()
        {
            // Add code
        }

        //
        public void UpdateDepth()
        {
            // Add code
        }


        /// <summary>
        /// returns pitch value of the current Iver state
        /// </summary>
        public double Pitch
        {
            get
            {
                return Convert.ToDouble(_compassStates[(int)(compassStates.pitch)]);
            }
        }

        /// <summary>
        /// returns roll value of the current Iver state
        /// </summary>
        public double Roll
        {
            get
            {
                return Convert.ToDouble(_compassStates[(int)(compassStates.roll)]);
            }
        }

        /// <summary>
        /// returns depth value of the current Iver state
        /// </summary>
        public double Depth { get { return _position.Z; } }


        // Display Port values and prompt user to enter a port.
        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }

        /// <summary>
        /// Used to convert the data within vehicle state to a loggable form.
        /// </summary>
        /// <returns>A string to be added to the log file. </returns>
        public override String ToString()
        {
            StringBuilder accum = new StringBuilder();

            accum.Append(_name + ", ");
            accum.Append(DateTime.Now + ", ");
            accum.Append(_latLong[0].ToString() + ", ");
            accum.Append(_latLong[1].ToString() + ", ");

            accum.Append(_heading.ToString() + ", ");
            accum.Append(_depth.ToString() + ", ");

            accum.Append(_position.ToString() + ", ");
            accum.Append(_velocity.ToString() + ", ");
            accum.Append(_acceleration.ToString() + ", ");

            for (int i = 0; i < _servoStates.Count; i++)
            {
                accum.Append(_servoStates[i].ToString() + ", ");
            }

            for (int i = 0; i < _compassStates.Count; i++)
            {
                accum.Append(_compassStates[i].ToString() + ", ");
            }

            accum.Append(_mode + ", ");
            accum.Append(_errorState + ", ");

            accum.Append(_ackMsg.ToString());

            return accum.ToString();
        }

        // Print details about current state of vehicle8
        public void PrintState()
        {
            Console.WriteLine();
            Console.WriteLine("AUV Name:\t \t \t {0}", this._name);
            Console.WriteLine("Current Date/Time:\t \t {0}", DateTime.Now);
            Console.WriteLine("Elapsed Time (ms):\t \t {0}", DateTime.Now.Millisecond);
            Console.WriteLine("Initial LatLong:\t \t {0}, {1}", this._initLatLong[0], this._initLatLong[1]);
            Console.WriteLine("Current LatLong:\t \t {0}, {1}", this._latLong[0], this._latLong[1]);
            Console.WriteLine("Heading (rad?):\t \t \t {0}", this._heading);
            Console.WriteLine("Depth (m):\t \t \t {0}", this._depth);
            Console.WriteLine("XYZ Position (m):\t \t {0}", this._position.ToString());
            Console.WriteLine("XYZ Velocity (m/s):\t \t {0}", this._velocity.ToString());
            Console.WriteLine("XYZ Acceleration (m/s^2):\t {0}", this._acceleration.ToString());
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            // Create simulated AUV
            AUVStateSim simIver = new AUVStateSim("Small Iver", null, null, 20, 5, new XYZCoordinate(1, 2, 3), new XYZCoordinate(2, 3, 4), new XYZCoordinate(0, 0, 0));

            // Logging parameters
            var csv = new StringBuilder();
            int i = 0;
            int iterations = 1000;

            // Simulation parameters
            var positionDes = new XYZCoordinate();
            positionDes.X = 1.0;
            positionDes.Y = 1.0;
            positionDes.Z = 0;
       
            simIver._propSpeed = 0.3;

            // Shark randomness
            Random random = new Random();

            while (i < iterations)
            {
                // Calculate rudder Ang
                simIver.UpdateState(-simIver.calcRudderAng(positionDes), simIver._propSpeed);

                // Log data
                var newLine = string.Format("{0},{1},{2},{3},{4},{5}", simIver._position.X, simIver._position.Y, positionDes.X, positionDes.Y, simIver._heading, simIver._rudderAngle);
                csv.AppendLine(newLine);

                // Point tracking stop condition
                if (positionDes.DistanceTo(simIver._position) < .1)
                {
                    // newIver._propSpeed = 0;
                }

                // Move desired point
                positionDes.X += 0.01;
                positionDes.Y += Math.Sin(positionDes.X)*0.005 - 0.005;

                ++i;
            }

            string filePath = "\\Users\\Eyassu\\Desktop\\LAIR\\SimData\\PTrackingTest.csv";
            File.WriteAllText(filePath, csv.ToString());

        }
    }
}
