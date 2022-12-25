using System;
using System.Collections.Generic;
/*using ThreeDimenMatching.Constant;
using ThreeDimenMatching.Models;
using ThreeDimenMatching.Halper;
using System.Windows.Forms;*/
//using HalconDotNet;
using System.Linq;
using System.Data;
using System.IO;
using System.Threading;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using ThreeDimenMatching.RosBridge;
using System.Reflection;
//using Newtonsoft.Json;
//using MongoDB.Driver;

namespace ThreeDimenMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            string SERVER_IP = "";
            IPAddress ipAddr = IPAddress.Parse(SERVER_IP);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 0);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(localEndPoint);
            try
            {
                Dictionary<string, string> msgs_topic_dict = new Dictionary<string, string>()
                {
                    { "/object_manipulation/api/model/insert", "std_msgs/String" },
                    { "/object_manipulation/api/gripper/insert", "std_msgs/String" },
                    { "/object_manipulation/api/model/update", "std_msgs/String" },
                    { "/object_manipulation/api/gripper/update", "std_msgs/String" },
                    { "/object_manipulation/api/model/upload", "std_msgs/String" },
                    { "/object_manipulation/api/model/score", "std_msgs/String" },
                };
                RosOperator.advertise_topic(msgs_topic_dict, sender);

                Socket api_model_insert = RosOperator.subscribe_req("/webinterface/req", sender);
                Socket api_gripper_insert = RosOperator.subscribe_req("/webinterface/req", sender);
                Socket api_model_update = RosOperator.subscribe_req("/webinterface/req", sender);
                Socket api_gripper_update = RosOperator.subscribe_req("/webinterface/req", sender);
                while (true)
                {
                    try
                    {
                        string model_insert = RosOperator.subscribe(api_model_insert);
                    }
                    catch { }
                    try
                    {
                        string gripper_insert = RosOperator.subscribe(api_gripper_insert);
                    }
                    catch { }
                    try
                    {
                        string model_update = RosOperator.subscribe(api_model_update);
                    }
                    catch { }
                    try
                    {
                        string gripper_update = RosOperator.subscribe(api_gripper_update);
                    }
                    catch { }
                }
            }catch{}
        }
    }
}


