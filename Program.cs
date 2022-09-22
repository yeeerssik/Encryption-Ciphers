using System;
using System.Text;

namespace CryptoApp
{
    class Program
    {
        public static void Main(string[] Args)
        {
            string[] keyArray = { "gybnglfld" };
            HillEncryption test = new();
            Console.WriteLine(test.Encryption("hello", keyArray));
            //Console.WriteLine(test.Decryption("FFDFGXGXXXGXFFGXXXDF", keyArray));

        }
    }
}
