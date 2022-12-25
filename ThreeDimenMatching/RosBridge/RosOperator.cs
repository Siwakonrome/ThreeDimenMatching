using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Sockets;


namespace ThreeDimenMatching.RosBridge
{
    class RosOperator
    {
        public static void advertise_topic(Dictionary<string, string> msgs_topic_dict, Socket Rosbridge_sender)
        {
            foreach (KeyValuePair<string, string> item in msgs_topic_dict)
            {
                var json_advertise = new
                {
                    op = "advertise",
                    topic = item.Key,
                    type = item.Value
                };
                string json_advertise_string = JsonConvert.SerializeObject(json_advertise);
                byte[] advertise_pub = Encoding.ASCII.GetBytes(json_advertise_string);
                Rosbridge_sender.Send(advertise_pub);
            }
        }

        public static string subscribe(Socket Rosbridge_sender)
        {
            byte[] buffer_sub = new byte[Rosbridge_sender.ReceiveBufferSize];
            int bytesRec = Rosbridge_sender.Receive(buffer_sub);
            string web_sub = Encoding.ASCII.GetString(buffer_sub, 0, bytesRec);
            return web_sub;
        }

        public static Socket subscribe_req(string input_topic, Socket Rosbridge_sender)
        {
            var json_sub_web = new
            {
                op = "subscribe",
                topic = input_topic
            };
            string json_sub_web_string = JsonConvert.SerializeObject(json_sub_web);
            byte[] web_pub = Encoding.ASCII.GetBytes(json_sub_web_string);
            Rosbridge_sender.ReceiveTimeout = 5000;
            Rosbridge_sender.Send(web_pub);
            return Rosbridge_sender;
        }
    }
}
