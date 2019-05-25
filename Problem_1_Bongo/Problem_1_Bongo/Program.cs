using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Problem_1_Bongo
{
    class Program
    {
        private static Dictionary<KeyValuePair<string, string>, int> dict = new Dictionary<KeyValuePair<string, string>, int>();
        static void Main(string[] args)
        {
            string input;
            input = GetInput();
            var inputObj = JObject.Parse(input);
            dfs(inputObj, 1);
            GetOutput();
        }
        static string GetInput()
        {
            string input = @" { 
                                    'key1': '1', 
                                    'key2': { 
                                              'key3': '1', 
                                              'key4': { 
                                                        'key5': '4' 
                                                      } 
                                             } 
                               } ";

            return input;
        }
        static void GetOutput()
        {
            KeyValuePair<string, string> temp;
            foreach (KeyValuePair<KeyValuePair<string, string>, int> kvp in dict)
            {
                temp = kvp.Key;
                Console.WriteLine("{0} {1}", temp.Key, kvp.Value);
            }
        }
        static void dfs(JObject jsonObj, int depth)
        {
            var tempObj = jsonObj;
            foreach (var item in tempObj)
            {
                string key = item.Key;
                dict.Add(new KeyValuePair<string, string>(key, System.Guid.NewGuid().ToString()), depth);
                JToken value = item.Value;
                if (item.Value.HasValues == true)
                {
                    dfs((JObject)value, depth + 1);
                }
            }
        }
    }
}
