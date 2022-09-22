namespace CryptoApp
{
    internal class VigenereEncryption : BaseEncryptAlgorithm
    {
        private string KeyValueGeneration(string keyValue, int sourceLength)
        {
            string uncutKeyValue = string.Concat(Enumerable.Repeat(keyValue, sourceLength / 2));
            string generatedKeyValue = uncutKeyValue.Substring(0, sourceLength);
            return generatedKeyValue;
        }

        public override string Encryption(string sourceMessage, string[] keyValues)
        {
            string encryptedMessage = "";
            string keyValue = KeyValueGeneration(keyValues[0], sourceMessage.Length);

            for(int i = 0; i < sourceMessage.Length ;i++)
            {
                int x = (sourceMessage[i] + keyValue[i]) % 26;
                x += 'A';

                encryptedMessage += (char)(x);
            }
            return encryptedMessage;
        }
    }
}
