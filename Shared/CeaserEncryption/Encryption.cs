namespace Shared.CeaserEncryption
{
    public class Encryption
    {
        /// <summary>
        /// Klasik Ingilizce karakter destekli şifreleme
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] CeaserEncryption(string text, int offset)
        {
            int length = text.Length;

            var characters = new byte[length];

            for (int i = 0; i < length; i++)
            {
                characters[i] = (byte)(text[i] + offset);
            }

            return characters;
        }

        public static string CeaserDecryption(byte[] encryptionMessage, int shiftKey)
        {

            var length = encryptionMessage.Length;

            char[] textMessage = new char[length];

            for (int i = 0; i < length; i++)
            {
                textMessage[i] = (char)(encryptionMessage[i] - shiftKey);
            }

            return new string(textMessage);

        }


        /// <summary>
        /// Mesaj datasını 2 byte'lık kümeler oalrak paketler Türkçe karakter desteği için.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] CeaserEncryptionUTF8(string text, int offset)
        {
            //mesaj uzunluğu hesaplanıyor => mesaj uzunluğu bilgisi (2 byte) + mesaj + (shift key + UTF8 uyumluluğu (1 byte))   
            int length = text.Length * 2;

            byte[] resultByte = new byte[length];

            for (int i = 0; i < length; i += 2)
            {
                resultByte[i] = (byte)(text[i / 2] + offset >> 8 & 0xFF);
                resultByte[i + 1] = (byte)(text[i / 2] + offset & 0xFF);
            }

            return resultByte;
        }

        public static string CeaserDecryptionUTF8(byte[] encryptionMessage, int shiftKey)
        {
            var length = encryptionMessage.Length;

            char[] textMessage = new char[length];

            for (int i = 0; i < length; i += 2)
            {
                textMessage[i / 2] = (char)((encryptionMessage[i] << 8) + encryptionMessage[i + 1] - shiftKey);
            }

            return new string(textMessage);
        }


    }
}
