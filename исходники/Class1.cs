using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Test_Class
    {

        static ConcurrentDictionary<string, int> unique = new ConcurrentDictionary<string, int>(101,50); // изменил кол-во бакетов и локеров, подобрал оптимальный вариант.
                                                                                                         // производительность стала лучше
        
        public ConcurrentDictionary<string, int> UniqueWords(string[] input)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.ForEach(input, ForUniqieWords);
            stopwatch.Stop();
            Console.WriteLine("Time for compilation= " + stopwatch.ElapsedMilliseconds + "ms");
            return unique;
        }
        static void ForUniqieWords(string str)
        {
            str = Regex.Replace(str, "[.?!)(,:…«»;„“№]", "");
            if (!unique.ContainsKey(str.ToLower()))
            {
                unique.TryAdd(str.ToLower(), 1); //если еще такого слова нет в словаре, то мы создадим пару ключ + значение = 1
            }
            else
            {
                //достаем значение ключа по самому ключу и ув. его на 1
                int cur_count;
                unique.TryGetValue(str.ToLower(), out cur_count);
                cur_count++;
                unique[str.ToLower()] = cur_count;
            }
        }
    }
}
