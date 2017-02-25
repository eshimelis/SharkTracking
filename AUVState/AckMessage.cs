using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AUVState
{
    public class ackMessage
    {
        int messageType;
        /* Message Types
         * 1 = ACK of OMSTOP command
         * 2 = ACK of OMLOAD command
         * 3 = ACK of OMSTART command
         * 4 = ACK of OLOGD command
         * 5 = ACK of OLOGL command 
         * 6 = ACL of OPOS command
         * 7 = ACK of OJW command
         * 8 = ACK of OSD command
         * 9 = ACK of OMS command
         * 10 = ACK of OMP command
         * 11 = ACK of OPK command
         * 12 = ACK of ORWSET command
        **/
        int status;
        /* Status
         * 0 = NO error
         * 1 = Error occurred
        **/
        int errorNumber;
        /* Error Types
         * 1 = Mission file not found
         * 2 = Folder directory not found
         * 3 = Errors occurred loading a mission
         * 4 = Sonar COM port not configured (UVC)
         * 5 = Sonar COM port not configured (Sonar Control)
         * 6 = UVC COM port not configured
         * 7 = INI file not found, load defaults (Sonar Control)
         * 8 = Error occurred when loading INI file (Sonar Control)
         * 9 = Video COM port not configured (UVC)
         * 10 = UVC COM port not configured (Video Control)
         * 11 = INI file not found, load defaults (Video Control)
         * 12 = Error occurred when loading INI file (Video Control)
         * 13 = Mandatory COM port not configured (UVC)
         * 14 = Mission NOT started due to compass data age
         * 15 = Mission NOT started due to GPS data age
         * 16 = Mission NOT started due to sounder data age
         * 17 = Errors occurred during Starting a mission
         * 18 = OPOS incorrect formation/values
         * 19 = OJW incorrect formation/values
         * 20 = OMS incorrect formation/values
         * 21 = OMP incorrect formation/values
         * 22 = OPK incorrect formation/values
         * 23 = ORWSET incorrect formation/values
         * 24 = OLOGL incorrect formation/values
         * 25 = OLOGD incorrect formation/values
         * 26 = Safety rule engaged, command not processed
         * 27 = OSD incorrect formation/values
         * 28 = OMSTOP incorrect formation/values
         * 29 = OMLOAD incorrect formation/values
         * 30 = OMSTART incorrect formation/values
         * 31 = OMLOAD SRP mission not found or bad mission file
         * 32 = Pressure malfunction...the vehicle will stop the mission
         **/
        string usrSet; // User setting name
        int usrNum; // Range from 0 to 3
        string usrVal;
        /* USRVAL States
         * 0 = disable
         * 1 = enable
         * VALUE1 = ""
         * VALUE2 = ""
         **/

        public ackMessage(string data)
        {
            string[] splitData;

            splitData = data.Split(new char[] { ',' });

            string[] messageEnd = splitData[splitData.Length - 1].Split(new char[] { '*' });

            messageType = Convert.ToInt16(splitData[1]);
            status = Convert.ToInt16(splitData[2]);
            errorNumber = Convert.ToInt16(messageEnd[0]);

            if (splitData.Length > 5)
            {
                usrSet = splitData[4];
                usrNum = Convert.ToInt16(splitData[5]);
                usrVal = splitData[6];
            }
            else
            {
                usrSet = "none";
                usrNum = 99;
                usrVal = "none";
            }
        }

        public override string ToString()
        {
            return "Message Type: " + messageType.ToString() + " Status: " +
                    status.ToString() + " Error Number: " + errorNumber.ToString()
                    + " User Settings: " + usrSet + " User Numbers: " + usrNum.ToString()
                    + " User Values: " + usrVal;
        }

        public int getMessageType()
        {
            return messageType;
        }
        public int getStatus()
        {
            return status;
        }
        public int getErrorNumber()
        {
            return errorNumber;
        }
    }
}
