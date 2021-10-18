using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Test_Class
    {


        private Dictionary<string, int>UniqueWords(string[] input)
        {
            Dictionary<string, int> unique = new Dictionary<string, int>();
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], "[.?!)(,:…«»;„“№]", "");//избавимся от знаков припинания
                if (!unique.ContainsKey(input[i].ToLower()))
                {
                    unique.Add(input[i].ToLower(), 1); //если еще такого слова нет в словаре, то мы создадим пару ключ + значение
                }
                else
                {
                    //достаем значение ключа по самому ключу и ув. его на 1
                    int cur_count;
                    unique.TryGetValue(input[i].ToLower(), out cur_count);
                    cur_count++;
                    unique[input[i].ToLower()] = cur_count;
                }
            }
            return unique;
        }
    }
}
