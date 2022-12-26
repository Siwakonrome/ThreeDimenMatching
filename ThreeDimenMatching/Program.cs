using System;
using System.Collections.Generic;
/*using ThreeDimenMatching.Constant;
using ThreeDimenMatching.Halper;
using System.Windows.Forms;*/
//using HalconDotNet;
using ThreeDimenMatching.Models;
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
using Newtonsoft.Json;

namespace ThreeDimenMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string SERVER_IP = "";
            IPAddress ipAddr = IPAddress.Parse(SERVER_IP);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 0);
            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(localEndPoint);
            try
            {
                string topic = "/api/object_manipulation/req";
                Dictionary<string, string> msgs_topic_dict = new Dictionary<string, string>()
                {
                    { topic, "std_msgs/String" },
                };
                RosOperator.advertise_topic(msgs_topic_dict, sender);
                Socket api_req = RosOperator.subscribe_req(topic, sender);
                while (true)
                {
                    try
                    {
                        string result = RosOperator.subscribe(api_req);
                        RosSubFormat rosSubFormat = JsonConvert.DeserializeObject<RosSubFormat>(result);
                        WebReqEvent addGripper = JsonConvert.DeserializeObject<WebReqEvent>(rosSubFormat.msg.data);
                    }
                    catch
                    {
                        
                    }
                }
            }catch{}*/


            
            string dataReceived = String.Empty;
            TcpClient client;
            IPAddress localAdd = IPAddress.Parse("");
            TcpListener listener = new TcpListener(localAdd, 0);
            try
            {
                listener.Start();
                while (true)
                {
                    try { client = listener.AcceptTcpClient(); } catch { continue; }
                    NetworkStream nwStream = client.GetStream();
                    Socket clientStream = client.Client;
                    try
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                        dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    }
                    catch { continue; }
                    if (dataReceived != String.Empty)
                    {
                        DataReceived received = JsonConvert.DeserializeObject<DataReceived>(dataReceived);
                        



                        /*List<Pose> mrs = new List<Pose>();
                        string textToSend = String.Empty;
                        // Sort high object to 0 index.
                        mrs = matching_method(dataReceived, true);
                        if (mrs.Count == LocalConstant.number0)
                        {
                            textToSend = "{" + $"\"modelName\":\"{String.Empty}\", \"x\":0.0, \"y\":0.0, \"z\":0.0, \"rx\":0.0, \"ry\":0.0, \"rz\":0.0, \"score\":0.0" + "}";
                        }
                        else
                        {
                            textToSend = "{" + $"\"modelName\":\"{mrs[0].modelName}\", \"x\":{mrs[0].x}, \"y\":{mrs[0].y}, \"z\":{mrs[0].z}, \"rx\":{mrs[0].rx}, \"ry\":{mrs[0].ry}, \"rz\":{mrs[0].rz}, \"score\":{mrs[0].score}" + "}";
                        }
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);*/
                    }
                }
            }
            catch
            {
            }

        }
    }
}


