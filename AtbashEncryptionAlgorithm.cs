using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoApp
{
    internal class AtbashEncryption : BaseEncryptAlgorithm
    {
        private const string EnglishAlphabet = "abcdefghijklmnopqrstuvwxyz";

        private const string RussianAlphabet = "абвгдзеёжзийклмнопрстуфхцчшщъыьэюя";
    
        private string changedSymbol (char i, string Alphabet)
        {
            string changedSymbol = (char.IsUpper(i) ?
                                    (((!Alphabet.Contains(i)) ?
                                        i : Alphabet[Alphabet.Length - Alphabet.IndexOf(i) - 1]).ToString()).ToUpper() :
                                    (((!Alphabet.Contains(i)) ?
                                        i : Alphabet[Alphabet.Length - Alphabet.IndexOf(i) - 1]).ToString()).ToLower());

            return changedSymbol;
        }

        public override string Encryption(string SourceMessage, string[] KeyValue)
        {
            string EncryptedMessage = "";

            if (!Regex.IsMatch(SourceMessage, @"/[\P{IsCyrillic}\P{Pc}]/gu")) 
            {
                foreach (char i in SourceMessage)
                {
                    EncryptedMessage += changedSymbol(i, RussianAlphabet);

                }
            }
            else if (!Regex.IsMatch(SourceMessage, @"/[\P{IsBasicLatin}\P{Pc}]/gu"))
            {
                foreach (char i in SourceMessage)
                {
                    EncryptedMessage += changedSymbol(i, EnglishAlphabet);
                }
            }
            else
            {
                return "Пожалуйста, используйте кириллицу или латиницу!";
            }
            
            return EncryptedMessage;
        }
    }
}
