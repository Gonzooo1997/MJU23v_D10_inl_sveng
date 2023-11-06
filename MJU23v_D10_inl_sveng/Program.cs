using System;
using System.Collections.Generic;
using System.IO;

namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        private static List<SweEngGloss> dictionary;

        private class SweEngGloss
        {
            public string WordSwe, WordEng;

            public SweEngGloss(string wordSwe, string wordEng)
            {
                WordSwe = wordSwe;
                WordEng = wordEng;
            }

            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                WordSwe = words[0];
                WordEng = words[1];
            }
        }

        private static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] arguments = Console.ReadLine().Split();
                string command = arguments[0];

                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (command == "load")
                {
                    LoadDictionary(arguments);
                }
                else if (command == "list")
                {
                    ListDictionary();
                }
                else if (command == "new")
                {
                    AddNewWord(arguments);
                }
                else if (command == "delete")
                {
                    DeleteWord(arguments);
                }
                else if (command == "translate")
                {
                    TranslateWord(arguments);
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            } while (true);
        }

        private static void LoadDictionary(string[] arguments)
        {
            string file = (arguments.Length == 2) ? arguments[1] : defaultFile;

            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    dictionary = new List<SweEngGloss>();
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: '{file}'");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        private static void ListDictionary()
        {
            if (dictionary == null)
            {
                Console.WriteLine("Dictionary not loaded. Use 'load' command.");
                return;
            }

            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.WordSwe,-10}  - {gloss.WordEng,-10}");
            }
        }

        private static void AddNewWord(string[] arguments)
        {
            if (arguments.Length == 3)
            {
                dictionary.Add(new SweEngGloss(arguments[1], arguments[2]));
            }
            else if (arguments.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string s = Console.ReadLine();
                Console.Write("Write word in English: ");
                string e = Console.ReadLine();
                dictionary.Add(new SweEngGloss(s, e));
            }
        }

        private static void DeleteWord(string[] arguments)
        {
            if (arguments.Length == 3)
            {
                int index = FindIndex(arguments[1], arguments[2]);
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Word not found in the dictionary.");
                }
            }
            else if (arguments.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string s = Console.ReadLine();
                Console.Write("Write word in English: ");
                string e = Console.ReadLine();
                int index = FindIndex(s, e);
                if (index != -1)
                {
                    dictionary.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Word not found in the dictionary.");
                }
            }
        }

        private static int FindIndex(string wordSwe, string wordEng)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.WordSwe == wordSwe && gloss.WordEng == wordEng)
                {
                    return i;
                }
            }
            return -1;
        }

        private static void TranslateWord(string[] arguments)
        {
            if (arguments.Length == 2)
            {
                TranslateAndPrint(arguments[1]);
            }
            else if (arguments.Length == 1)
            {
                Console.WriteLine("Write word to be translated: ");
                string s = Console.ReadLine();
                TranslateAndPrint(s);
            }
        }

        private static void TranslateAndPrint(string word)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.WordSwe == word)
                {
                    Console.WriteLine($"English for {gloss.WordSwe} is {gloss.WordEng}");
                    return;
                }
                if (gloss.WordEng == word)
                {
                    Console.WriteLine($"Swedish for {gloss.WordEng} is {gloss.WordSwe}");
                    return;
                }
            }
            Console.WriteLine("Word not found in the dictionary.");
        }
    }
}