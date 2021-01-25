using System;
using System.Linq;

namespace Strings_manip_Pig_Latin
{
    class Program
    {
        public static char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        static void Main(string[] args)
        {
            Console.WriteLine("This is a Pig Latin Translator");
            bool isRunning = true;
            while (isRunning)
            {
                Console.Write("Enter a line to be translated: ");
                string input = Console.ReadLine();

                if (input.Any()) // Checks to see if user entered anything
                {
                    string[] sentence = input.Split();
                    foreach (string word in sentence)
                    {
                        string caseOrder = CasingFormat(word);
                        string pigLatinWord;
                        if (IsImproperWord(word))
                        {
                            Console.Write("{0} ", word);
                        }
                        else
                        {
                            if (HasPunctuation(word))
                            {
                                pigLatinWord = PigConversion((word.Substring(0,word.Length-1)).ToLower()) + word.Substring(word.Length-1);
                            }
                            else
                            {
                                pigLatinWord = PigConversion(word.ToLower()); // changes to lowercase for converter method
                            }

                            //checking case order to print the string in matching case 
                            for (int i = 0; i < caseOrder.Length; i++)
                            {
                                if (caseOrder.Substring(i, 1) == "1")
                                {
                                    Console.Write($"{pigLatinWord.Substring(i, 1).ToUpper()}");
                                }
                                else
                                {
                                    Console.Write($"{pigLatinWord.Substring(i, 1)}");
                                }
                            }
                            //the ay remains lowercase so no need to check
                            Console.Write($"{pigLatinWord.Substring(caseOrder.Length)} "); 
                        }

                       
                    }
                    Console.WriteLine(); //formats output
                }
                isRunning = GoAgain();
            }
        }

        // Identifies if the user wants to run the program again or not
        public static bool GoAgain()
        {
            Console.WriteLine("Translate another line? (y/n)");
            string answer = Console.ReadLine().ToLower();
            if(answer == "y")
            {
                return true;
            }
            else if(answer == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input, please try agian");
                return GoAgain();
            }
        }
        // identify is a gvien string begins with a vowel or not(barring y)
        public static bool StartsWithVowel(string s)
        {
            foreach (char c in vowels)
            {
                if (s.StartsWith(c))
                    return true;
            }
            return false;
        }

        // method converts a given string to pig lating format and returns it
        // ignores y when acting as a vowel
        public static string PigConversion(string s)
        {
            int firstVowel = -1;
            if(StartsWithVowel(s)) // conversion process for words beginning in vowels
            {
                return $"{s}way";
            }
            else  //conversion process for words beginning in constonants
            {
                foreach (char letter in vowels)
                {
                    int i = s.IndexOf(letter);
                    if (firstVowel == -1)
                    {
                        firstVowel = i;
                    }
                    else if (i != -1 && i < firstVowel)
                    {
                        firstVowel = i;
                    }
                }
                int yVowel = s.IndexOf("y"); // used for finding y acting as vowels
                if (yVowel != -1 && yVowel !=0 && yVowel != s.Length)
                {
                 if(firstVowel == -1)
                 {
                        firstVowel = yVowel;
                 }
                 foreach (char letter in vowels)
                 {
                        if(s.Substring(yVowel-1,1) != letter.ToString() && s.Substring(yVowel + 1, 1) != letter.ToString())
                        {
                            if(yVowel<firstVowel)
                            {
                                firstVowel = yVowel;
                            }
                        }
                 }

                }

                if(firstVowel == -1) // given string might have y for the vowel or none
                {
                    firstVowel = s.IndexOf("y"); // Equates to -1 if no y

                    if (firstVowel == -1)
                    {
                        return s;
                    }
                }

                return $"{s.Substring(firstVowel)}{s.Substring(0,firstVowel)}ay";
            }
        } 
        // method identifies if a given string contains any special characters or numbers
        // contained within the string
        // also checks for normal punctuation not being at the end of a word
        public static bool IsImproperWord(string s)
        {
            if(s.Contains("?"))
            {
                return !s.EndsWith("?");
            }
            if (s.Contains(","))
            {
                return !s.EndsWith(",");
            }
            if (s.Contains("."))
            {
                return !s.EndsWith(".");
            }
            if (s.Contains("!"))
            {
                return !s.EndsWith("!");
            }
            return (s.Any(char.IsSymbol) || s.Any(char.IsDigit) || s.Contains("@"));
        }
        // method identifies if a given word has punctiation at the end
        public static bool HasPunctuation(string s) 
        {
            return (s.EndsWith("?") || s.EndsWith(",") || s.EndsWith(".") || s.EndsWith("!"));
        }

        // this method helps identify the casing order of a string,
        // if a word is all lowercase, starts with a capital, etc
        // outputs format in a string of 0 and 1s, 0 being lowercase and 1 upper
        public static string CasingFormat(string s)
        {
            string caseFormat = "";

            foreach(char letter in s)
            {
                if(char.IsUpper(letter))
                {
                    caseFormat += "1";
                }
                else
                {
                    caseFormat += "0";
                }
            }

            return caseFormat;
        }

    }
    
}
