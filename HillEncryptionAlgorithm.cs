using System.Text.RegularExpressions;

namespace CryptoApp
{
    internal class HillEncryption : BaseEncryptAlgorithm
    {
        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string RussianAlphabet = "абвгдзеёжзийклмнопрстуфхцчшщъыьэюя";
        static int[,] GetKeyMatrix(string key, string Alphabet)
        {
            int sqrtKey = (int)Math.Sqrt(key.Length);
            int[,] keyMatrix = new int[sqrtKey, sqrtKey];
            int k = 0;

            for (int i = 0; i < sqrtKey; i++)
            {
                for (int j = 0; j < sqrtKey; j++)
                {
                    keyMatrix[i, j] = Array.IndexOf(Alphabet.ToArray(), key[k]);
                    k++;
                }
            }

            return keyMatrix;
        }

        static int[,] GetMessageMatrix(string key, string msg, string Alphabet)
        {
            int sqrtKey = (int)Math.Sqrt(key.Length);
            int msgCol;

            if (msg.Length % sqrtKey == 0)
            {
                msgCol = (msg.Length) / sqrtKey;
            }
            else
            {
                msgCol = ((msg.Length) / sqrtKey) + 1;
            }

            int[,] messageMatrix = new int[sqrtKey, msgCol];
            char[] fullMsg = new char[(msgCol) * sqrtKey];
            int k = fullMsg.Length;

            for (int i = 0; i < msg.Length; i++)
            {
                fullMsg[i] = msg[i];
            }
            for (int i = msg.Length; i < fullMsg.Length; i++)
            {
                fullMsg[i] = ' ';
            }
            for (int i = 0; i <= msgCol; i++)
            {
                for (int j = 0; j < sqrtKey; j++)
                {
                    if (k > 0)
                    {
                        messageMatrix[j, i] = Array.IndexOf(Alphabet.ToArray(), fullMsg[fullMsg.Length - k]);
                        k--;
                    }
                }
            }
            return messageMatrix;
        }

        static string BaseEncrypt(int[,] key, int[,] message, string Alphabet)
        {
            int temp;
            string cipherText = "";
            int[,] cipher = new int[message.GetLength(0), message.GetLength(1)];

            for (int i = 0; i < key.GetLength(0); i++)
            {
                for (int j = 0; j < message.GetLength(1); j++)
                {
                    temp = 0;

                    for (int k = 0; k < key.GetLength(1); k++)
                    {
                        temp += key[i, k] * message[k, j];
                    }

                    cipher[i, j] = temp % Alphabet.Length;
                }
            }
            for (int i = 0; i < cipher.GetLength(1); i++)
            {
                for (int j = 0; j < cipher.GetLength(0); j++)
                {
                    cipherText += Alphabet.ToArray()[cipher[j, i]];
                }
            }

            return cipherText;
        }

        public override string Encryption(string SourceMessage, string[] KeyValues)
        {
            string EncryptedMessage = "";
            if ((Math.Sqrt(KeyValues[0].Length) % 1) == 0)
            {
                if (Regex.IsMatch(SourceMessage, @"^[а-я]+$"))
                {
                    int[,] keyMatrix = GetKeyMatrix(KeyValues[0], RussianAlphabet);
                    int[,] messageMatrix = GetMessageMatrix(KeyValues[0], SourceMessage, RussianAlphabet);
                    EncryptedMessage = BaseEncrypt(keyMatrix, messageMatrix, RussianAlphabet); 
                }
                else if (Regex.IsMatch(SourceMessage, @"^[a-z]+$"))
                {
                    int[,] keyMatrix = GetKeyMatrix(KeyValues[0], EnglishAlphabet);
                    int[,] messageMatrix = GetMessageMatrix(KeyValues[0], SourceMessage, EnglishAlphabet);
                    EncryptedMessage = BaseEncrypt(keyMatrix, messageMatrix, EnglishAlphabet);
                }
                else
                {
                    EncryptedMessage = "Пожалуйста, используйте кириллицу или латиницу!";
                }
            }
            else
            {
                EncryptedMessage = "Ключевое значение должно являтся полным квадратом целого числа!";
            }
            
            return EncryptedMessage;
        }
    }
}
