using System.Net;
using System.Net.Sockets;
using Shared.Abstract;

namespace Shared
{
    public class ServerUDP : Server
    {
        UdpClient udpServer = new();

        IPEndPoint? remoteEndPoint;

        public void SetEndPoint(IPAddress iPAddress,int port)
        {
            remoteEndPoint = new(iPAddress, port);

            udpServer.Client.Bind(remoteEndPoint);
        }
        public void MessagePrint()
        {

            byte[] receivedBytes = udpServer.Receive(ref remoteEndPoint);

            var message = getMessage(receivedBytes);

            Console.WriteLine(message);

        }

    }
}
