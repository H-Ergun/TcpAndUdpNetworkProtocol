using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared.Abstract;

namespace Shared
{
    public class ClientTCP : Client
    {
        private TcpClient? client { get; set; }

        public override void SetEndPoint(string ipAddress, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ipAddress);

            client ??= new TcpClient();

            client.Connect(iPAddress, port);
        }

        public override void SendEncryptedMessage(string message, int shiftKey)
        {
            if (client != null)
            {
                byte[] data = ConvertEncryptedMessage(message, shiftKey);

                AddMetaData(ref data, shiftKey);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                var responseData = new byte[1024];

                int responseLength = stream.Read(responseData, 0, responseData.Length);

                string response = Encoding.ASCII.GetString(responseData, 0, responseLength);

                Response = response;

                return;

            }

            throw new ArgumentNullException("Client cannot be null. First try running the Connect method.");
        }









    }
}
