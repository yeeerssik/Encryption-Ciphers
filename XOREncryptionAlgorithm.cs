using System.Text;

namespace CryptoApp
{
    internal class XOREncryption : BaseEncryptAlgorithm
    {
        private string KeyValueGeneration(string keyValue, int SourceLength)
        {
            string uncutKeyValue = string.Concat(Enumerable.Repeat(keyValue, SourceLength / 2));
            string generatedKeyValue = uncutKeyValue.Substring(0, SourceLength);
            return generatedKeyValue;
        }

        public static String ToBinary(string str)
        {
            Byte[] data = Encoding.ASCII.GetBytes(str);
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }
        public string GetBinaryData(string sourceMessage, string[] KeyValues)
        {
            string binaryData = "";
            string keyValue = KeyValueGeneration(KeyValues[0], sourceMessage.Length);
            string binaryKey = ToBinary(keyValue);
            string binaryMsg = ToBinary(sourceMessage);

            for (int i = 0; i < binaryMsg.Length; i++)
            {
                binaryData += (binaryKey[i] ^ binaryMsg[i]);
            }

            return binaryData;
        }

        public override string Encryption(string sourceMessage, string[] keyValues)
        {
            string encryptedMessage = "";

            string keyValue = KeyValueGeneration(keyValues[0], sourceMessage.Length);

            for (int i = 0; i < sourceMessage.Length; i++)
            {
                encryptedMessage += char.ToString((char)(sourceMessage[i] ^ keyValue[i]));
            }

            return encryptedMessage;
        }
    }
}
