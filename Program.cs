using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace unique_words
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь для считывания текстового файла. \nПример: d:\u005C\u005Cidiot.txt");
            string path_file = Console.ReadLine();
            Console.WriteLine("Введите путь для вывода результата: ");
            string path_result = Console.ReadLine();
            //string path_file = @"d:\idiot.txt";
            //string path_result = @"c:\result.txt";
            
            Dictionary<string, int> unique = new Dictionary<string, int>();
            StreamReader sr = new StreamReader(path_file);
            
            string[] text_by_words = sr.ReadToEnd().Split(new char[]{' ','—','–','-','\n'}); //разделим строки 
            sr.Close();
            text_by_words = text_by_words.Where(x => !string.IsNullOrEmpty(x)).ToArray(); // избавимся от пустых элементов массива
            
            for (int i = 0; i < text_by_words.Length; i++)
            {
                text_by_words[i] = Regex.Replace(text_by_words[i], "[.?!)(,:…«»;„“№]", "");//избавимся от знаков припинания
                if (!unique.ContainsKey(text_by_words[i].ToLower()))
                {
                    unique.Add(text_by_words[i].ToLower(), 1); //если еще такого слова нет в словаре, то мы создадим пару ключ + значение
                }
                else
                {
                    //достаем значение ключа по самому ключу и ув. его на 1
                    int cur_count;
                    unique.TryGetValue(text_by_words[i].ToLower(), out cur_count);
                    cur_count++;
                    unique[text_by_words[i].ToLower()] = cur_count;
                }
            }

            List<KeyValuePair<string, int>> convert = unique.ToList(); //конвентируем словарь в лист, чтобы использовать List.sort
            convert.Sort((pair1,pair2) => pair2.Value.CompareTo(pair1.Value)); //сортировка по убыванию
            StreamWriter sw = new StreamWriter(path_result, false, System.Text.Encoding.Default);
            foreach (var kv in convert)
            {
                sw.WriteLine($"{kv.Key} - {Convert.ToString(kv.Value)}");
            }
            sw.Close();
        }
    }
}
