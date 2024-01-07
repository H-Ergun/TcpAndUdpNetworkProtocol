using Shared.Abstract;

namespace Shared.Factories
{
    public class ClientFactory
    {

        public static Client createTcpClient() => new ClientTCP();

        public static Client createUdpClient() => new ClientUDP();
    }
}
