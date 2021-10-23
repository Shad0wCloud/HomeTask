using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        static ConcurrentDictionary<string, int> unique = new ConcurrentDictionary<string, int>(101, 50);

        public Dictionary<string, int> UniqueWords(string[] str)
        {
            str = str.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            Parallel.ForEach(str, ForUniqieWords);
            List<KeyValuePair<string, int>> convert = unique.ToList();
            var output = unique.ToDictionary(entry => entry.Key,entry => entry.Value);
            return output;
        }

        private void ForUniqieWords(string str)
        {
            str = Regex.Replace(str, "[.?!)\"(,:…«»;„“№]", "");
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
