using System;
using System.Text;

namespace CryptoApp
{
    class Program
    {
        public static void Main(string[] Args)
        {
            string[] keyArray = { "люди" };
            HillEncryption test = new();
            Console.WriteLine(test.Encryption("приветмир", keyArray));
            //Console.WriteLine(test.GetBinaryData("fuck", keyArray));

        }
    }
}
