using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp
{
    internal class XOREncryption : BaseEncryptAlgorithm
    {
        private string KeyValueGeneration(string KeyValue, int SourceLength)
        {
            string UncutKeyValue = string.Concat(Enumerable.Repeat(KeyValue, SourceLength / 2));
            string GeneratedKeyValue = UncutKeyValue.Substring(0, SourceLength);
            return GeneratedKeyValue;
        }

        public static String ToBinary(string str)
        {
            Byte[] data = Encoding.ASCII.GetBytes(str);
            return string.Join("", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }
        public string GetBinaryData(string CryptedMessage, string[] KeyValues)
        {
            string DecryptedMessage = "";
            string keyValue = KeyValueGeneration(KeyValues[0], CryptedMessage.Length);
            string binaryKey = ToBinary(keyValue);
            string binaryMsg = ToBinary(CryptedMessage);

            for (int i = 0; i < binaryMsg.Length; i++)
            {
                DecryptedMessage += (binaryKey[i] ^ binaryMsg[i]);
            }

            return DecryptedMessage;
        }
        //public override string Decryption(string CryptedMessage, string[] KeyValues)
        //{
        //    string DecryptedMessage = "";

        //    string keyValue = KeyValueGeneration(KeyValues[0], CryptedMessage.Length);

        //    for (int i = 0; i < CryptedMessage.Length; i++)
        //    {
        //        DecryptedMessage += char.ToString((char)(CryptedMessage[i] ^ keyValue[i]));
        //    }

        //    return DecryptedMessage;
        //}

        public override string Encryption(string SourceMessage, string[] KeyValues)
        {
            string EncryptedMessage = "";

            string keyValue = KeyValueGeneration(KeyValues[0], SourceMessage.Length);

            for (int i = 0; i < SourceMessage.Length; i++)
            {
                EncryptedMessage += char.ToString((char)(SourceMessage[i] ^ keyValue[i]));
            }

            return EncryptedMessage;
        }
    }
}
