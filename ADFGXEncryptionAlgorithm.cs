namespace CryptoApp
{
    internal class ADFGXEncryption : BaseEncryptAlgorithm
    {
        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private static string adfgx = "ADFGX";

        private static string numbers = "0123456789";
        private Dictionary<char, string> generateSquare(string text, string alphabet)
        {
            Dictionary<char, string> dict = new Dictionary<char, string>();
            string selectedChars = text.Substring(0, adfgx.Length);
            string addedChars = "";
            int charsCounter = 0;
            int numCounter = 0;
            for (int i = 0; i < adfgx.Length; i++)
            {
                for (int j = 0; j < adfgx.Length; j++)
                {
                    if (charsCounter < adfgx.Length)
                    {
                        if (addedChars.IndexOf(selectedChars[charsCounter]) > -1)
                        {
                            dict.Add(selectedChars[charsCounter], (adfgx[i].ToString() + adfgx[j].ToString()));
                            addedChars = addedChars + selectedChars[charsCounter];
                        }
                        else
                        {
                            j--;
                        }
                        charsCounter++;
                    }
                    else
                    {
                        if (j % 2 > 0 && numCounter < numbers.Length)
                        {
                            dict.Add(numbers[numCounter], (adfgx[i].ToString() + adfgx[j].ToString()));
                            addedChars = addedChars + numbers[numCounter];
                            numCounter++;
                        }
                        else
                        {
                            for (int k = 0; k < alphabet.Length; k++)
                            {
                                if (addedChars.IndexOf(alphabet[k]) < 0)
                                {
                                    dict.Add(alphabet[k], (adfgx[i].ToString() + adfgx[j].ToString()));
                                    addedChars = addedChars + alphabet[k];
                                    break;
                                }
                            }
                        }
                    }

                }
            }
            return dict;
        }

        private List<string> sortMatrix(string prevStepResult, string key)
        {
            string sorted = "";
            List<string> keyMatrix = new List<string>(key.Length);

            for (int i = 0; i < key.Length; i++)
            {
                keyMatrix.Add(key[i].ToString());
            }
            int matrixSpot = 0;
            for (int i = 0; i < prevStepResult.Length; i += 2)
            {
                keyMatrix[matrixSpot] += prevStepResult[i].ToString() + prevStepResult[i + 1].ToString();
                if (matrixSpot == keyMatrix.Count - 1)
                {
                    matrixSpot = 0;
                }
                else
                {
                    matrixSpot++;
                }
            }

            keyMatrix.Sort();

            return keyMatrix;
        }

        public override string Encryption(string sourceMessage, string[] keyValues)
        {
            string encryptedMessage = "";
            Dictionary<char, string> square = generateSquare(keyValues[0], EnglishAlphabet);

            List<char> someList = new List<char>();
            foreach (char c in square.Keys)
            {
                someList.Add(c);
            }

            for (int i = 0; i < sourceMessage.Length; i++)
            {
                encryptedMessage += square[sourceMessage[i]];
            }
            string key = keyValues[0];
            List<string> keyMatrix = sortMatrix(encryptedMessage, key);
            for (int i = 0; i < keyMatrix.Count; i++)
            {
                encryptedMessage += keyMatrix[i].Substring(1);
            }
            return encryptedMessage;
        }
    }
}
