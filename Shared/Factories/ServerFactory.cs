using Shared.Abstract;

namespace Shared.Factories
{
    public class ServerFactory
    {

        public static Server createTcpServer() => new ServerTCP();

        public static Server createUdpServer() => new ServerUDP();
    }
}
