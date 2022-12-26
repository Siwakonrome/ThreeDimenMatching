using System;
using System.IO;
using System.Net;
using System.Text;
using HalconDotNet;
using Newtonsoft.Json;
using System.Net.Sockets;
//using ThreeDimenMatching.Halper;
using System.Collections.Generic;
using ThreeDimenMatching.Constant;

namespace ThreeDimenMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataReceived = String.Empty;
            TcpClient client;
            IPAddress localAdd = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAdd, 8800);
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
                        try
                        {
                            string resp = String.Empty;
                            if (dataReceived == TcpRecivedType.score)
                            {
                                resp = "{\"score\":\"0.2\"}";
                            }
                            else if (dataReceived == TcpRecivedType.load)
                            {
                                resp = "{\"load\":200}";
                            }
                            else if (dataReceived == TcpRecivedType.match)
                            {
                                resp = "{\"match\":\"1.0/1.0/1.0/1.0/1.0/1.0\"}";
                            }
                            else if (dataReceived == TcpRecivedType.camera)
                            {
                                resp = "{\"camera\":1}";
                            }
                            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(resp);
                            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                        }
                        catch { }                   
                    }
                }
            }catch{ }
        }
    }
}


