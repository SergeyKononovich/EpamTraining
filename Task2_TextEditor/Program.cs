using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2_TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] separators = {".", "!", "?", "(.){3}"};
            string input = "First sentence!?     Second a. sentence!    Third sentence...  Yes 12.3.  ";

            Array.Sort(separators, (x, y) => -x.Length.CompareTo(y.Length));
            string pattern = separators.Aggregate("", (current, sep) => current + $"(?<=[{sep}]) | ");
            pattern = pattern.Remove(pattern.Length - 3, 3);

            Regex rgx = new Regex(@"(?<=[\.!\?]+)(?=[^\.!\?»\d])", RegexOptions.IgnoreCase);
            string[] sentences = rgx.Split(input).Select(s => s.Trim()).Where(s => s != "").ToArray(); ;
            foreach (var word in sentences)
            {
                Console.WriteLine(word);
            }

            Console.ReadKey();
        }
    }
}




