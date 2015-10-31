using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Task2_TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filename = @"E:\Projects\EpamTraining\Task2_TextEditor\TextSample.txt";

            try
            {
                using (TextReader tr = new StreamReader(new FileStream(filename, FileMode.Open)))
                {
                    SimpleTextParser tp = new SimpleTextParser(PunctuationContainer.Default);
                    Text text = tp.Parse(tr);

                    // Sample 1
                    /*Вывести все предложения заданного текста в порядке возрастания количества слов в 
                    каждом из них.*/
                    var result = from sentence in text.OfType<Sentence>()
                                 let wordsCount = (from sentenceItem in sentence
                                                   where sentenceItem is Word
                                                   select sentenceItem).Count()
                                 orderby wordsCount
                                 select sentence;

                    foreach (var sentence in result)
                    {
                        Console.WriteLine(sentence);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}




