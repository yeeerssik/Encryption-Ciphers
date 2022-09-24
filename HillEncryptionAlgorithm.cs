using System.Text.RegularExpressions;

namespace CryptoApp
{
    internal class HillEncryption : BaseEncryptAlgorithm
    {
        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string RussianAlphabet = "абвгдзеёжзийклмнопрстуфхцчшщъыьэюя";
        static int[,] GetKeyMatrix(string key, string alphabet)
        {
            int sqrtKey = (int)Math.Sqrt(key.Length);
            int[,] keyMatrix = new int[sqrtKey, sqrtKey];
            int k = 0;

            for (int i = 0; i < sqrtKey; i++)
            {
                for (int j = 0; j < sqrtKey; j++)
                {
                    keyMatrix[i, j] = Array.IndexOf(alphabet.ToArray(), key[k]);
                    k++;
                }
            }

            return keyMatrix;
        }

        static int[,] GetMessageMatrix(string key, string msg, string alphabet)
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
                        messageMatrix[j, i] = Array.IndexOf(alphabet.ToArray(), fullMsg[fullMsg.Length - k]);
                        k--;
                    }
                }
            }
            return messageMatrix;
        }

        static string BaseEncrypt(int[,] key, int[,] message, string alphabet)
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

                    cipher[i, j] = temp % alphabet.Length;
                }
            }
            for (int i = 0; i<key.GetLength(0); i++)
            {
                for (int j = 0; j<key.GetLength(1); j++)
                {
                    cipherText += alphabet.ToArray()[cipher[j, i]];
                }
            }

            return cipherText;
        }

        public override string Encryption(string sourceMessage, string[] keyValues)
        {
            string encryptedMessage = "";
            if ((Math.Sqrt(keyValues[0].Length) % 1) == 0)
            {
                if (Regex.IsMatch(sourceMessage, @"^[а-я]+$"))
                {
                    int[,] keyMatrix = GetKeyMatrix(keyValues[0], RussianAlphabet);
                    int[,] messageMatrix = GetMessageMatrix(keyValues[0], sourceMessage, RussianAlphabet);
                    encryptedMessage = BaseEncrypt(keyMatrix, messageMatrix, RussianAlphabet); 
                }
                else if (Regex.IsMatch(sourceMessage, @"^[a-z]+$"))
                {
                    int[,] keyMatrix = GetKeyMatrix(keyValues[0], EnglishAlphabet);
                    int[,] messageMatrix = GetMessageMatrix(keyValues[0], sourceMessage, EnglishAlphabet);
                    encryptedMessage = BaseEncrypt(keyMatrix, messageMatrix, EnglishAlphabet);
                }
                else
                {
                    encryptedMessage = "Пожалуйста, используйте кириллицу или латиницу!";
                }
            }
            else
            {
                encryptedMessage = "Ключевое значение должно являтся полным квадратом целого числа!";
            }
            
            return encryptedMessage;
        }
    }
}
