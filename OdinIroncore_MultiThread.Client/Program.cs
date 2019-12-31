using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace YazlabP2_Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Client client = new Client();

            client.InitializeClient2();

            Thread threadSend = new Thread(client.SendMessage);
            Thread threadRecieve = new Thread(client.RecieveMessage);
            threadSend.Start();
            threadRecieve.Start();

            //var startTimeSpan = TimeSpan.Zero;
            //var periodTimeSpan = TimeSpan.FromSeconds(.5);

            //var timer = new System.Threading.Timer((e) =>
            //{
            //    client.Istemci2();
            //}, null, startTimeSpan, periodTimeSpan);

            Console.ReadKey();

            client.CloseClient2();
        }
    }
}