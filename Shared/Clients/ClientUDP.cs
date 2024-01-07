using System.Net.Sockets;
using Shared.Abstract;

namespace Shared
{
    public class ClientUDP : Client
    {
        private UdpClient? udpClient { get; set; }

        public override void SetEndPoint(string ipAddress, int port)
        {
            udpClient ??= new UdpClient();

            udpClient.Connect(ipAddress, port);
        }

        public override void SendEncryptedMessage(string message, int shiftKey)
        {

            if (udpClient != null)
            {
                var data = ConvertEncryptedMessage(message, shiftKey);

                AddMetaData(ref data, shiftKey);

                udpClient.Send(data, data.Length);
            }
        }






    }
}
