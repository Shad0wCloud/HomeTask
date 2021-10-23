using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
         
            string path_file = "input.txt";
            string path_result = "result.txt";

            StreamReader sr = new StreamReader(path_file);
            string[] text_by_words = sr.ReadToEnd().Split(new char[] { ' ',' ', '—', '–', '-', '\n' }); //разделим строки 
            sr.Close();

            Dictionary<string, int> text = client.UniqueWords(text_by_words);
            client.Close();

            List<KeyValuePair<string, int>> convert = text.ToList();
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
