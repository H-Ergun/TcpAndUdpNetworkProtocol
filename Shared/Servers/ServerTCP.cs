using System.Net;
using System.Net.Sockets;
using System.Text;
using Shared.Abstract;

namespace Shared
{
    public class ServerTCP : Server
    {
        public int ExpectedDataSize { get; set; } = 1024;
        private TcpListener? listener { get; set; }
        private TcpClient? client { get; set; }
        private NetworkStream? stream { get; set; }

        private byte[]? buffer;

        public void SetEndPoint(string ipAddress, int port)
        {
            IPAddress iPAddress = IPAddress.Parse(ipAddress);

            listener ??= new TcpListener(iPAddress, port);

            listener.Start();
        }



        public void MessagePrint()
        {
            client = listener?.AcceptTcpClient() ?? throw new ArgumentNullException("listener cannot be null");

            // İstemciden gelen verileri işleme
            stream = client.GetStream();

            if (stream.CanRead)
            {

                buffer = new byte[ExpectedDataSize];

                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    var message = getMessage(buffer);

                    Console.WriteLine(message);

                    byte[] response = Encoding.ASCII.GetBytes("Mesajiniz alindi!");
                    stream.Write(response, 0, response.Length);

                }


            }

            // İstemci bağlantısını kapat
            client.Close();


        }
    }
}
