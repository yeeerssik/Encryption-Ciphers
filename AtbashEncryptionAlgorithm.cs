using System.Text.RegularExpressions;

namespace CryptoApp
{
    internal class AtbashEncryption : BaseEncryptAlgorithm
    {
        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string RussianAlphabet = "абвгдзеёжзийклмнопрстуфхцчшщъыьэюя";
    
        private string changedSymbol (char i, string alphabet)
        {
            string changedSymbol = (char.IsUpper(i) ?
                                    (((!alphabet.Contains(i)) ?
                                        i : alphabet[alphabet.Length - alphabet.IndexOf(i) - 1]).ToString()).ToUpper() :
                                    (((!alphabet.Contains(i)) ?
                                        i : alphabet[alphabet.Length - alphabet.IndexOf(i) - 1]).ToString()).ToLower());

            return changedSymbol;
        }

        public override string Encryption(string sourceMessage, string[] keyValue)
        {
            string encryptedMessage = "";

            if (!Regex.IsMatch(sourceMessage, @"/[\P{IsCyrillic}\P{Pc}]/gu"))
            {
                foreach (char c in sourceMessage)
                {
                    encryptedMessage += changedSymbol(c, RussianAlphabet);
                }
            }
            else if (!Regex.IsMatch(sourceMessage, @"/[\P{IsBasicLatin}\P{Pc}]/gu"))
            {
                foreach (char c in sourceMessage)
                {
                    encryptedMessage += changedSymbol(c, EnglishAlphabet);
                }
            }
            return encryptedMessage;
        }
    }
}
