using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace Task2_TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + @"\TextSample.txt";

            try
            {
                using (var tr = new StreamReader(new FileStream(filename, FileMode.Open)))
                {
                    var tp = new SimpleTextParser(PunctuationContainer.Default);
                    Text text = tp.Parse(tr);


                    // Sample 1
                    string sample = "Вывести все предложения заданного текста в порядке возрастания " +
                                    "количества слов в каждом из них..\n";
                    WriteLine("Sample 1: " + sample);
                    var res1 = from sentence in text.OfType<Sentence>()
                               let wordsCount = sentence.OfType<Word>().Count()
                               orderby wordsCount
                               select sentence;

                    foreach (var sentence in res1)
                        WriteLine(sentence);
                    WriteLine("///////////////////////////////////////////////////////////////////////\n");


                    // Sample 2
                    int wordLength = 4;
                    sample = "Во всех вопросительных предложениях текста найти и напечатать " +
                            $"без повторений слова заданной длины ({wordLength}).\n";
                    var res2 = (from sentence in text.OfType<Sentence>()
                                where sentence.IsInterrogative
                                from word in sentence
                                where word.CharsLength == wordLength
                                select word).Distinct().ToArray();

                    foreach (var word in res2)
                        WriteLine(word);
                    WriteLine("///////////////////////////////////////////////////////////////////////\n");


                    // Sample 3
                    wordLength = 4;
                    sample = $"Из текста удалить все слова заданной длины ({wordLength}), начинающиеся " +
                              "на согласную букву. \n";
                    WriteLine("Sample 3: " + sample);
                    Text copy = text.Clone() as Text;

                    var res3 = (from sentence in copy.OfType<Sentence>()
                                from word in sentence.OfType<Word>()
                                where word.CharsLength == wordLength && word.Chars[0].IsСonsonant()
                                select new {Sentence = sentence, Word = word}).ToArray();

                    foreach (var r in res3)
                        r.Sentence.Remove(r.Word);
                    WriteLine(copy);
                    WriteLine("///////////////////////////////////////////////////////////////////////\n");


                    // Sample 4
                    wordLength = 6;
                    string expression = "(FACT: Children aged 1 to 3 months, crying without tears),";
                    var sentenceItems = tp.ParseExpression(expression).ToArray();
                    sample = $"В некотором предложении текста слова заданной длины ({wordLength}) заменить " +
                             $"указанной подстрокой \"{expression}\", длина которой может не совпадать с длиной слова. \n";
                    WriteLine("Sample 4: " + sample);
                    copy = text.Clone() as Text;

                    var res4 = (from sentence in copy.OfType<Sentence>()
                                from word in sentence.OfType<Word>()
                                where word.CharsLength == wordLength
                                select new { Sentence = sentence, Word = word }).ToArray();

                    foreach (var r in res4)
                    {
                        int index = r.Sentence.IndexOf(r.Word);
                        r.Sentence.RemoveAt(index);
                        r.Sentence.Insert(index, sentenceItems);
                    }

                    WriteLine(copy);
                    WriteLine("///////////////////////////////////////////////////////////////////////\n");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }

            ReadKey();
        }
    }
}




