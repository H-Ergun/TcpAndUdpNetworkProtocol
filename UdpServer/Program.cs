using Shared;
using Shared.Consted;
using System.Net;

Console.WriteLine("UDP Server");

var ipAddress = IPAddress.Any;

ServerUDP udpServer = new();

udpServer.SetEndPoint(ipAddress,EndPointData.UdpPort);

do
{
    udpServer.MessagePrint();

} while (true);
