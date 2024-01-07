using Shared.Abstract;
using Shared.Consted;
using Shared.Enums;
using Shared.Factories;

Client clientTCP = ClientFactory.createTcpClient();

Client clientUDP = ClientFactory.createUdpClient();

clientUDP.SetEndPoint(EndPointData.LocalIPAddress, EndPointData.UdpPort);

clientTCP.SetEndPoint(EndPointData.LocalIPAddress, EndPointData.TcpPort);

do
{
    Console.Write("Mesajlaşmak istedğiniz protokolü seçiniz TCP(T) UDP(U): ");

    var selectionProtocol = Console.ReadLine();

    switch (selectionProtocol)
    {
        case "T":
            sendMessage(sendTcpMessage);
            break;
        case "U":
            sendMessage(sendUdpMessage);
            break;
        default:
            Console.WriteLine("Lütfen geçerli bir değer giriniz.");
            break;
    }

}
while (true);



//Menüden seçilen değere göre türkçe dil desteğini set edilmesini sağlıyor.
void setSelectionLanguageSupport()
{

    var isBreak = true;  


    while (isBreak)
    {
        Console.Write("Türkçe karakter desteğini açmak ister misiniz? Evet(E) Hayır(H): ");

        var selectionLanguageSupport = Console.ReadLine();

        switch (selectionLanguageSupport)
        {
            case "E":
                clientTCP.MessagingStandart = MessagingStandart.UTF8;
                clientUDP.MessagingStandart = MessagingStandart.UTF8;
                isBreak = false;
                break;
            case "H":
                clientTCP.MessagingStandart = MessagingStandart.Classic;
                clientUDP.MessagingStandart = MessagingStandart.Classic;
                isBreak = false;
                break;
            default:
                Console.WriteLine("Lütfen geçerli bir değer giriniz.");
                break;
        }
    }

}


//Menüden seçilen değere göre tcp veya udp servara mesaj gönderiyor 
void sendMessage(Action<int> proccess)
{
    setSelectionLanguageSupport();

    while (true)
    {
        Console.Write("Shift key giriniz: ");

        var sK = Console.ReadLine();

        if (sK != "")
            while (true)
                if (int.TryParse(sK, out int shiftKey) && shiftKey < 129 && shiftKey > 0)
                {
                    proccess(shiftKey);
                }
                else
                {
                    Console.WriteLine("Lütfen 129 dan küçük 0 dan büyük bir sayı giriniz. ");
                    break;
                }
        break;

    }


}

void sendUdpMessage(int shiftKey)
{
    Console.WriteLine("Mesajınızı giriniz: ");

    var message = Console.ReadLine() ?? "";

    clientUDP.SendEncryptedMessage(message, shiftKey);
}

void sendTcpMessage(int shiftKey)
{
    Console.WriteLine("Mesajınızı giriniz: ");

    var message = Console.ReadLine() ?? "";

    clientTCP.SendEncryptedMessage(message, shiftKey);

    Console.WriteLine(clientTCP.Response);
}





