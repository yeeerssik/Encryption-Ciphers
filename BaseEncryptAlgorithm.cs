using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp
{
    internal abstract class BaseEncryptAlgorithm
    {
        public abstract string Encryption(string SourceMessage, string[] KeyValues);
        //public abstract string Decryption(string CryptedMessage, string[] KeyValues);
    }
}
