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
    internal class Client
    {
        private int portNo = 16332;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        private StreamReader streamReader;
        private StreamWriter streamWriter;

        private Timer timer1;
        private Random random;

        public void Istemci1()
        {
            //CheckForIllegalCrossThreadCalls = false;
            try
            {
                tcpClient = new TcpClient("localhost", portNo);
                networkStream = tcpClient.GetStream();

                byte[] buffer = Encoding.ASCII.GetBytes("text");
                byte[] receiveBuffer = new byte[tcpClient.ReceiveBufferSize];

                if (networkStream.CanWrite)
                {
                    networkStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    return;
                }
                if (networkStream.CanRead)
                {
                    networkStream.Read(receiveBuffer, 0, (int)tcpClient.ReceiveBufferSize);
                    string DurumMesaj = Encoding.ASCII.GetString(receiveBuffer);
                }
                else
                {
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Mesaj Gönderme İşlemi Başarısız");
            }
            networkStream.Close();
            tcpClient.Close();
        }

        public void PrintLine()
        {
            for (int i = 0; i < 25; i++)
                Console.Write("-");

            Console.WriteLine();
        }

        public void InitializeClient2()
        {
            random = new Random();
            tcpClient = new TcpClient("localhost", portNo);
            networkStream = tcpClient.GetStream();

            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);
        }

        public void RecieveMessage()
        {
            while (true)
            {
                string sunucuMesaji = streamReader.ReadLine();
                Console.WriteLine(DateTime.Now + "  Gelen Mesaj : " + sunucuMesaji);
            }
        }

        public void SendMessage()
        {
            while (true)
            {
                String istemciMesaji = random.Next(100).ToString();
                streamWriter.WriteLine(istemciMesaji);
                streamWriter.Flush();
                Console.WriteLine(DateTime.Now + "  Giden Mesaj : " + istemciMesaji);
            }
        }

        public void CloseClient2()
        {
            try
            {
                streamWriter.Close();
                streamReader.Close();

                networkStream.Close();
                tcpClient.Close();
            }
            catch
            {
                Console.WriteLine("Program Doğru Kapanamıyor");
            }
        }
    }
}