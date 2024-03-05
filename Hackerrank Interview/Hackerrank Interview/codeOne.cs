using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hackerrank_Interview
{
    internal class CodeOne
    {
        static void Main(string[] args)
        {
            var inputCommands = Console.ReadLine();

            while (inputCommands != null)
            {
                var inputs = inputCommands.Split(';');

                switch (inputs[0])
                {
                    case "S":
                        Console.WriteLine(splitter(inputs[2]));
                        break;
                    case "C":
                        Console.WriteLine(combiner(inputs[1], inputs[2]));
                        break;
                }
                inputCommands = Console.ReadLine();
            }
        }

        static string splitter(string word)
        {
            word = char.ToUpper(word[0]) + word.Substring(1);
            string[] words = Regex.Matches(word, @"([A-Z][a-z]+)").Cast<Match>().Select(x => x.Value).ToArray();
            string firstWord = word.Substring(0, word.IndexOf(words[0]));
            return (firstWord + " " + string.Join(" ", words)).ToLower().Trim();
        }

        static string combiner(string stringType, string word)
        {
            word = word.ToLower();
            string[] words = word.Split(' ');
            for (int i = words.Length - 1; i > 0; i--)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }
            string combinedWords = string.Join("", words);
            switch (stringType)
            {
                case "M":
                    return combinedWords + "()";
                case "V":
                    return string.Join("", words);
                case "C":
                    return char.ToUpper(combinedWords[0]) + combinedWords.Substring(1);
                default:
                    return combinedWords;
            }
        }
    }
}
