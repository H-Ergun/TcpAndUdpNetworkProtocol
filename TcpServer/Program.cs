using Shared;
using Shared.Consted;

Console.WriteLine("TCP Server");

ServerTCP tcpServer = new();

tcpServer.SetEndPoint(EndPointData.LocalIPAddress, EndPointData.TcpPort);

do
{
    tcpServer.MessagePrint();
}
while (true);

