using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Word_Composition_Problem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Trie trie = new Trie();


            //read data from file and store into list
            List<Word> words = ReadFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"C:\Users\naray\source\repos\Word_Composition_Problem\Word_Composition_Problem\words1.txt"));
            foreach (Word word in words)
            {
                //insert word line by line on tree
                trie.Insert(word.Name);
            }
            foreach (Word word in words)
            {
                word.Search(trie);
            }

            //get longest two words from list of complete words
            List<Word> completewords = words.Where(x => x.IsComplete()).ToList();
            List<Word> topTwo = completewords
                .OrderByDescending(x => x.Name.Length)
                .ThenByDescending(x => x.Name.Length)
                .Take(2)
                .ToList();


            //output
            Console.WriteLine(string.Format("1. Compound Word: {0}", topTwo[0].Name));
            Console.WriteLine(string.Format("2. Compound Word: {0}", topTwo[1].Name));
            Console.ReadKey();
        }

        private static List<Word> ReadFile(string filePath)
        {
            //read data from file line by line and send when data read operation is completed.
            List<Word> words = new List<Word>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string word in lines.OrderBy(w => w).ToList())
            {
                if (!string.IsNullOrEmpty(word))
                {
                    words.Add(new Word(word));
                }
            }
            return words;
        }
    }
}
