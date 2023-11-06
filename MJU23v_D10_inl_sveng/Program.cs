namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        private static List<SweEngGloss> dictionary;


        class SweEngGloss
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
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
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
                    foreach (SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
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