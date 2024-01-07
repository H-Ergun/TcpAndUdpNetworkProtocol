
using Shared.CeaserEncryption;

namespace Shared.Abstract
{
    public abstract class Server
    {

        /// <summary>
        /// Shift key değerini getirir 7 bitlik bir değerdir.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static int getShiftKey(byte data) => data & 127;

        /// <summary>
        /// Türkçe karakter destekleyip desteklemediğine kontrol eder.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static bool isUTF8(byte data) => (data & 128) == 128;

        /// <summary>
        /// Data uzunluğunu hesaplar (Clienttan gelen datanın ilk 2 byte değeri data uzunluğunu verir)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static int getMessageLength(byte[] data) => (data[0] << 8) + data[1];

        /// <summary>
        /// Clienttan gelen data dan ham mesajı getirir.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        protected string getMessage(byte[] data)
        {
            var messageLength = getMessageLength(data);

            var baseMessageLength = messageLength - 3;

            var isUTF8 = Server.isUTF8(data[messageLength - 1]);

            var shiftKey = getShiftKey(data[messageLength - 1]);

            var baseData = new byte[baseMessageLength];

            //mesaj datası ekleniyor
            for (int i = 0; i < baseMessageLength; i++)
            {
                baseData[i] = data[i + 2];
            }


            if (isUTF8)
            {
                return Encryption.CeaserDecryptionUTF8(baseData, shiftKey);
            }
            else
            {
                return Encryption.CeaserDecryption(baseData, shiftKey);
            }
        }


    }
}
