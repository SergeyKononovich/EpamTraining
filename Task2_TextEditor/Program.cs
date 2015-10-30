using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Task2_TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            TextParser tp = new TextParser(TextSeparatorContainer.Default);
            TextReader tr = new StreamReader(new FileStream(@"E:\Projects\EpamTraining\Task2_TextEditor\TextSample.txt", FileMode.Open));
            Text text = tp.Parse(tr);

            Console.WriteLine(text.ToString());
            //foreach (var sentence in text.Sentences)
            //{
            //    Console.WriteLine(sentence);
            //}

            Console.ReadKey();
        }
    }
}




