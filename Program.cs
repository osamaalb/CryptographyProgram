using System;
using System.IO;
using NCalc;

namespace Cryptography_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To encrypt or decrypt write your text in 'Input.txt'.");
            Console.WriteLine();
            string key;
            char[] keyArray;
            char answer;
            bool b0 = false;
            while (b0 == false)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("A: Generate keys.");
                Console.WriteLine("B: Encrypt text.");
                Console.WriteLine("C: Decrypt text.");
                answer = Convert.ToChar(Console.ReadLine());
                switch (answer)
                {
                    case 'a':
                    case 'A':
                        generateKeys();
                        b0 = done(b0);
                        break;
                    case 'b':
                    case 'B':
                        Console.WriteLine("Enter your key");
                        key = Console.ReadLine();
                        Console.WriteLine("The encrypted text has been wrote in 'Output.txt'.");
                        keyArray = key.ToCharArray();
                        foreach (char keyChar in keyArray)
                            encrypt(Convert.ToInt32(keyChar));
                        b0 = done(b0);
                        break;
                    case 'c':
                    case 'C':
                        string input;
                        Console.WriteLine("Enter your key");
                        key = Console.ReadLine();
                        Console.WriteLine("Enter the reverse equation.");
                        input = Console.ReadLine();
                        if (f(10, input) == 10)
                        {
                            Console.WriteLine("You can't enter an equation that will not change the key.");
                            Console.ReadLine();
                            return;
                        }
                        Console.WriteLine("The decrypted text has been wrote in 'Output.txt'.");
                        keyArray = key.ToCharArray();
                        foreach (char keyChar in keyArray)
                        {
                            int decChar = f(Convert.ToInt32(keyChar), input);
                            while (decChar > 127)
                                decChar -= 127;
                            while (decChar < 0)
                                decChar += 127;
                            decrypt(decChar);
                        }
                        b0 = done(b0);
                        break;
                    default:
                        Console.WriteLine("Invalid input! try agin.");
                        break;
                }
            }
            Console.ReadLine();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private static bool done(bool b)
        {
            char answer;
            Console.WriteLine("Done? [y/n]");
            answer = Convert.ToChar(Console.ReadLine());
            switch (answer)
            {
                case 'y':
                case 'Y':
                    b = true;
                    break;
                case 'n':
                case 'N':
                    break;
                default:
                    break;
            }
            return b;
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private static void generateKeys()
        {
            string key;
            string input;
            char[] keyArray;
            Console.WriteLine("Enter a public key (used for encryption).");
            key = Console.ReadLine();
            Console.WriteLine("Enter an equation to generate a private key. [Example: k + 3]");
            input = Console.ReadLine();
            if (f(10, input) == 10)
            {
                Console.WriteLine("You can't enter an equation that will not change the key.");
                return;
            }
            keyArray = key.ToCharArray();
            Console.WriteLine("the private key (used for decryption).");
            foreach (char keyChar in keyArray)
            {
                int encChar = f(Convert.ToInt32(keyChar), input);
                while (encChar > 127)
                    encChar -= 127;
                while (encChar < 0)
                    encChar += 127;
                Console.Write(Convert.ToChar(encChar));
            }
            Console.WriteLine();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private static void encrypt(int key)
        {
            StreamReader myReader = new StreamReader("Input.txt");
            string line = "";
            using (StreamWriter myWriter = new StreamWriter("Output.txt"))
            {
                while (line != null)
                {
                    line = myReader.ReadLine();
                    if (line != null)
                    {
                        char[] charArray = line.ToCharArray();
                        foreach (char lineChar in charArray)
                        {
                            if (Convert.ToInt32(lineChar) + key > 127)
                                myWriter.Write(Convert.ToChar(Convert.ToInt32(lineChar) + key - 127));
                            else
                                myWriter.Write(Convert.ToChar(Convert.ToInt32(lineChar) + key));
                        }
                        myWriter.WriteLine("");
                    }
                }
                myWriter.Close();
            }
            myReader.Close();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private static void decrypt(int key)
        {
            StreamReader myReader = new StreamReader("Input.txt");
            string line = "";
            using (StreamWriter myWriter = new StreamWriter("Output.txt"))
            {
                while (line != null)
                {
                    line = myReader.ReadLine();
                    if (line != null)
                    {
                        char[] charArray = line.ToCharArray();
                        foreach (char lineChar in charArray)
                        {
                            if (Convert.ToInt32(lineChar) - key < 0)
                                myWriter.Write(Convert.ToChar(Convert.ToInt32(lineChar) - key + 127));
                            else
                                myWriter.Write(Convert.ToChar(Convert.ToInt32(lineChar) - key));
                        }
                        myWriter.WriteLine("");
                    }
                }
                myWriter.Close();
            }
            myReader.Close();
        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        private static int f(int k, string input)
        {
            Expression e = new Expression(input);
            e.Parameters["k"] = k;
            return Convert.ToInt32(e.Evaluate());
        }
    }
}