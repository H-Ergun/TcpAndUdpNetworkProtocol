using Shared.Enums;
using Shared.CeaserEncryption;

namespace Shared.Abstract
{
    public abstract class Client
    {   

        /// <summary>
        /// Server dan gelen cevap
        /// </summary>
        public string? Response { get; set; }
        public MessagingStandart MessagingStandart { get; set; } = MessagingStandart.Classic;

        /// <summary>
        /// Mesaj datasına meta data ekler => Data uzunluğu, shift key, türkçe karakter uyumluluğu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="shiftKey"></param>
        /// <param name="messagingStandart"></param>
        /// <returns></returns>
        protected void AddMetaData(ref byte[] data, int shiftKey)
        {
            var withMetaDataLength = data.Length + 3;

            var withMetaData = new byte[withMetaDataLength];

            //Mesajın uzunluk bilgisi 2 byte olarak mesaj datasına ekleniyor.
            withMetaData[0] = (byte)(withMetaDataLength >> 8 & 0xFF);
            withMetaData[1] = (byte)(withMetaDataLength & 0xFF);

            //mesaj datası ekleniyor
            for (int i = 0; i < data.Length; i++)
            {
                withMetaData[i + 2] = data[i];
            }
            //shift key ve utf8 destek bilgisi son byte'ın içine ekleniyor 8.bit UTF8 desteğini belirtir 1 ise UTF8 destekler. 
            //0b1000 1000 UFT8 destekler shift key değeri 8 dir
            var lastMetaData = MessagingStandart == MessagingStandart.UTF8 ? shiftKey + 128 : shiftKey;
            withMetaData[withMetaDataLength - 1] = (byte)lastMetaData;
            data = withMetaData;
        }


        /// <summary>
        /// Mesajı MessagingStandart enum değişkenine göre şifreler.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="shiftKey"></param>
        /// <returns></returns>
        protected byte[] ConvertEncryptedMessage(string message, int shiftKey)
        {
            return MessagingStandart switch
            {
                MessagingStandart.UTF8 => Encryption.CeaserEncryptionUTF8(message, shiftKey),
                _ => Encryption.CeaserEncryption(message, shiftKey),
            };
        }
        public abstract void SetEndPoint(string ipAddress, int port);
        public abstract void SendEncryptedMessage(string message, int shiftKey);
    }
}
