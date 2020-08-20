using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientUdp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Bind(udpEndPoint);

            while(true)
            {
                Console.Write("Введите сообщение ");
                var message = Console.ReadLine();

                EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8080);

                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverEndPoint);

                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref serverEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
                
            }
        }
    }
}
