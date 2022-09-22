using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Dictionary<string, char> generateDecryptionSquare(string text , string alphabet)
        {
            Dictionary<string, char> dict = new Dictionary<string, char>();
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
                        if (dict.ContainsKey((adfgx[i].ToString() + adfgx[j].ToString())))
                        {

                        }
                        else
                        {
                            if (addedChars.IndexOf(selectedChars[charsCounter]) > -1)
                            {
                                dict.Add((adfgx[i].ToString() + adfgx[j].ToString()), selectedChars[charsCounter]);
                                addedChars = addedChars + selectedChars[charsCounter];
                            }
                            else
                            {
                                j--;
                            }
                        }
                        charsCounter++;
                    }
                    else
                    {
                        if (j % 2 > 0 && numCounter < numbers.Length)
                        {
                            if (dict.ContainsKey((adfgx[i].ToString() + adfgx[j].ToString())))
                            {
                                numCounter++;
                                continue;
                            }
                            dict.Add((adfgx[i].ToString() + adfgx[j].ToString()), numbers[numCounter]);
                            addedChars = addedChars + numbers[numCounter];
                            numCounter++;
                        }
                        else
                        {
                            for (int k = 0; k < alphabet.Length; k++)
                            {

                                if (addedChars.IndexOf(alphabet[k]) < 0)
                                {
                                    if (dict.ContainsKey((adfgx[i].ToString() + adfgx[j].ToString())))
                                    {
                                        continue;
                                    }
                                    dict.Add((adfgx[i].ToString() + adfgx[j].ToString()), alphabet[k]);
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

        private List<string> unsortMatrix(List<string> sorted, string key)
        {
            List<string> unsorted = new List<string>(key.Length);

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < sorted.Count; j++)
                {
                    if (sorted[j].ToCharArray()[0] == key[i])
                    {
                        unsorted.Add(sorted[j]);
                    }
                }
            }
            return unsorted;
        }

        private List<string> fitTextToSquare(string text, string key)
        {
            List<string> keyMatrix = new List<string>(key.Length);

            for (int i = 0; i < key.Length; i++)
            {
                keyMatrix.Add(key[i].ToString());
            }
            var sizesDict = new Dictionary<char, int>();
            foreach (char c in key)
            {
                sizesDict.Add(c, 0);
            }

            int idx = 0;
            foreach (char _ in text)
            {
                sizesDict[key[idx]]++;
                idx = idx == key.Length - 1 ? 0 : idx + 1;
            }
            int matrixSpot = 0;
            for (int i = 0; i < text.Length; i += 2)
            {
                keyMatrix[matrixSpot] += text[i].ToString() + text[i + 1].ToString();

                if (keyMatrix[matrixSpot].Length == sizesDict[keyMatrix[matrixSpot][0]])
                {
                    matrixSpot++;
                }
            }
            return keyMatrix;
        }

        public override string Decryption(string CryptedMessage, string[] KeyValues)
        {
            string DecryptedMessage = "";
            Dictionary<string, char> square = generateDecryptionSquare(KeyValues[0], EnglishAlphabet);
            string key = KeyValues[0];
            List<string> keyMatrix = new List<string>(key.Length);

            foreach (char k in key)
            {
                keyMatrix.Add(k.ToString());
            }
            keyMatrix.Sort();
            string sortedKey = "";
            foreach (string k in keyMatrix )
            {
                sortedKey += k;
            }

            List<string> fittedText = fitTextToSquare(CryptedMessage, sortedKey);
            List<string> unsortedList = unsortMatrix(fittedText, KeyValues[0]);
            string unsorted = "";
            for (int i = 0; i < unsortedList.Count; i++)
            {
                unsorted += unsortedList[i].Substring(1);
            }
            for (int i = 0; i < unsorted.Length; i += 2)
            {
                DecryptedMessage = DecryptedMessage + square[unsorted[i].ToString() + unsorted[i + 1].ToString()];
            }
            return DecryptedMessage;
        }

        public override string Encryption(string SourceMessage, string[] KeyValues)
        {
            string EncryptedMessage = "";
            Dictionary<char, string> square = generateSquare(KeyValues[0], EnglishAlphabet);

            List<char> someList = new List<char>();
            foreach (char c in square.Keys)
            {
                someList.Add(c);
            }

            for (int i = 0; i < SourceMessage.Length; i++)
            {
                EncryptedMessage += square[SourceMessage[i]];
            }
            string key = KeyValues[0];
            List<string> keyMatrix = sortMatrix(EncryptedMessage, key);
            for (int i = 0; i < keyMatrix.Count; i++)
            {
                EncryptedMessage += keyMatrix[i].Substring(1);
            }
            return EncryptedMessage;
        }
    }
}
