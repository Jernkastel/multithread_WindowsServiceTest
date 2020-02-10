using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APR400_lab4_max_jern_TimmerApp
{
    
    class ReaderClass
    {
        //Set folder path
        private static string changeablePath = @"INSERT PROJECT FOLDER PATH HERE";
        private static string readerFilePath = changeablePath + @"\APR400_lab4_max_jern_TimmerApp\ReaderFile\test.txt";
        //Creates file info
        private static FileInfo testFile = new FileInfo(readerFilePath);
        //Initialize counters
        private static int wCounter, cCounter, uCounter, lCounter, oCounter, uWord, lWord;

        public static async Task StreamReader()
        {
            await ReadText();
            Console.ReadKey();
        }
        private static async Task ReadText()
        {
            await Task.Run(() =>
            {
                //Prints filename
                Console.WriteLine($"Content of {testFile.Name}: \n");
                //Prints content
                Console.WriteLine($"'{File.ReadAllText(readerFilePath)}'");
                string text = File.ReadAllText(readerFilePath);

                string[] words = Regex.Split(text.Trim(), "\\s+");

                //Counts letters
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i])) uCounter++;
                    else if (char.IsLower(text[i])) lCounter++;
                    else oCounter++;
                }

                cCounter = uCounter + lCounter + oCounter;
                Console.WriteLine("\n" + $"Number of characters: {cCounter}");
                Console.WriteLine($"Number of irregular characters: {oCounter}");
                Console.WriteLine($"Number of uppercase letters: {uCounter}");
                Console.WriteLine($"Number of lowercase letters: {lCounter}");

                //Counts words
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].ToUpper().Equals(words[j])) uWord++;
                    if (words[j].ToLower().Equals(words[j])) lWord++;
                }

                wCounter = uWord + lWord;
                Console.WriteLine("\n" + $"Amount of words: {wCounter}");
                Console.WriteLine($"Amount of uppercase words: {uWord}");
                Console.WriteLine($"Amount of lowercase words: {lWord}");
            });

        }
    }
}
