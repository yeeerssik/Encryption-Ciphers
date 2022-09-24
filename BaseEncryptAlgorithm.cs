namespace CryptoApp
{
    internal abstract class BaseEncryptAlgorithm
    {
        public abstract string Encryption(string SourceMessage, string[] KeyValues);
    }
}
