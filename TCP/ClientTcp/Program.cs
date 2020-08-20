using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало диалога");
            const string ip = "127.0.0.1";
            const int port = 8080;

            while (true)
            {
                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.Write("Введите сообщение: ");
                var message = Console.ReadLine();

                var data = Encoding.UTF8.GetBytes(message);

                tcpSocket.Connect(tcpEndPoint);

                tcpSocket.Send(data);

                var buffer = new byte[256];
                var size = 0;
                var answer = new StringBuilder();

                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (tcpSocket.Available > 0);

                Console.WriteLine(answer);

                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
            }
        }
    }
}
