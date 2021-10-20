using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MyLibrary;

namespace unique_words
{
    class Program
    {
        static void Main(string[] args)
        {
            string path_file = "idiot.txt";
            string path_result = "result.txt";
            StreamReader sr = new StreamReader(path_file);
            string[] text_by_words = sr.ReadToEnd().Split(new char[] { ' ', '—', '–', '-', '\n' }); //разделим строки 
            sr.Close();
            text_by_words = text_by_words.Where(x => !string.IsNullOrEmpty(x)).ToArray(); // избавимся от пустых элементов массива
            
            MyLibrary.Test_Class inst = new MyLibrary.Test_Class();
            var returnValue = inst.UniqueWords(text_by_words);
            List<KeyValuePair<string, int>> convert = returnValue.ToList();
            convert.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value)); //сортировка по убыванию
            StreamWriter sw = new StreamWriter(path_result, false, System.Text.Encoding.Default);
            foreach (var kv in convert)
            {
                sw.WriteLine($"{kv.Key} - {Convert.ToString(kv.Value)}");
            }
            sw.Close();
            Console.WriteLine("Done!");
        }
    }
}
