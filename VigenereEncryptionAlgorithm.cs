using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp
{
    internal class VigenereEncryption : BaseEncryptAlgorithm
    {
        private string KeyValueGeneration(string KeyValue, int SourceLength)
        {
            string UncutKeyValue = string.Concat(Enumerable.Repeat(KeyValue, SourceLength / 2));
            string GeneratedKeyValue = UncutKeyValue.Substring(0, SourceLength);
            return GeneratedKeyValue;
        }
        public override string Decryption(string CryptedMessage, string[] KeyValues)
        {
            string DecryptedMessage = "";
            string KeyValue = KeyValueGeneration(KeyValues[0], CryptedMessage.Length);
            for (int i = 0; i < CryptedMessage.Length;i++)
            {
                int x = (CryptedMessage[i] - KeyValue[i] + 26) % 26;
                x += 'A';

                DecryptedMessage += (char)(x);
            }
            return DecryptedMessage;
        }

        public override string Encryption(string SourceMessage, string[] KeyValues)
        {
            string EncryptedMessage = "";
            string KeyValue = KeyValueGeneration(KeyValues[0], SourceMessage.Length);
            for(int i = 0;i<SourceMessage.Length;i++)
            {
                int x = (SourceMessage[i] + KeyValue[i]) % 26;
                x += 'A';

                EncryptedMessage += (char)(x);
            }
            return EncryptedMessage;
        }
    }
}
