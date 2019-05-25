using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Problem_2_Bongo
{
    class Person
    {
        public string _first_name, _last_name;
        public object _father;
        public Person(string first_name, string last_name, object father)
        {
            this._first_name = first_name;
            this._last_name = last_name;
            this._father = father;
        }
    }
    class Program
    {
        private static Dictionary<KeyValuePair<string, string>, int> dict = new Dictionary<KeyValuePair<string, string>, int>();
        public static Dictionary<string, Object> listOfPredefinedObj = new Dictionary<string, Object>();
        static void Main(string[] args)
        {
            Person person_a = new Person("User", "1", "none");
            Person person_b = new Person("User", "1", person_a);

            AddObjectToAlist(listOfPredefinedObj, "person_a", person_a);
            AddObjectToAlist(listOfPredefinedObj, "person_b", person_b);

            string input;
            input = GetInput();
            try
            {
                var inputObj = JObject.Parse(input);
                dfs(inputObj, 1);
                GetOutput();
                ClearObjList(listOfPredefinedObj);
            }
            catch
            {
                Console.Write("Invalid Input");
            }

        }
        static void AddObjectToAlist(Dictionary<string, Object> objList, string objName, Object aObject)
        {
            objList.Add(objName, aObject);
        }
        static void ClearObjList(Dictionary<string, Object> objList)
        {
            objList.Clear();
        }
        static string GetInput()
        {
            string input = @" { 
                                'key1' : '1', 
                                'key2' : { 
                                            'key3' : '1', 
                                            'key4' : { 
                                                        'key5' : '4',
                                                        'user' : 'person_b'
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
                else
                {
                    if (listOfPredefinedObj.ContainsKey((string)value))
                    {
                        Object temp = (object)listOfPredefinedObj[(string)value];
                        JObject tempJObj = (JObject)JToken.FromObject(temp);
                        dfs(tempJObj, depth + 1);
                    }
                }
            }
        }
    }
}
