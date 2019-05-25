using System;
using System.Collections.Generic;
using System.IO;

namespace Problem_3_Bongo
{
    public class node
    {
        public int value;
        public node parent;
    }
    class Program
    {
        public static string val;
        public static int nodeNumber;
        public static List<node> myTree = new List<node>();
        public static List<int> parentList = new List<int>();
        public static string textFile = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\input.txt"));
        public static StreamReader fileObj;
        public static void GetInput()
        {
            int u, v;
            string[] tmp;
            if (File.Exists(textFile))
            {
                fileObj = new StreamReader(textFile);

                Console.Write("Enter number of node: ");
                val = fileObj.ReadLine();
                Console.WriteLine(val);
                nodeNumber = Convert.ToInt32(val);
                for (int i = 0; i <= nodeNumber; i++)
                {
                    myTree.Add(new node() { value = i, parent = null });
                }

                Console.WriteLine("Enter tree information: ");
                Console.WriteLine("Parent node     Child node: ");
                for (int i = 1; i < nodeNumber; i++)
                {
                    tmp = fileObj.ReadLine().Split(" ");
                    Console.WriteLine("{0} {1}", tmp[0], tmp[1]);
                    u = Convert.ToInt32(tmp[0]);
                    v = Convert.ToInt32(tmp[1]);
                    CreateTree(u, v);
                }
            }
            else
            {
                Console.WriteLine("Input file not exists");
                System.Environment.Exit(1);
            }

        }
        public static void CreateTree(int u, int v)
        {
            myTree[v].parent = myTree[u];
        }
        public static void DeleteTree()
        {
            myTree.Clear();
        }
        public static void CreateParentList(node u)
        {
            parentList.Add(u.value);
            if (u.parent == null)
            {
                return;
            }                
            CreateParentList(u.parent);
        }
        public static void DeleteParentList()
        {
            parentList.Clear();
        }
        public static void FindLca(node u, node v)
        {
            int lca = -1;
            DeleteParentList();
            CreateParentList(u);
            lca = FindLca(v);
            Console.WriteLine("LCA of {0} and {1} is {2}", u.value, v.value, lca);
        }
        public static int FindLca(node v)
        {
            if (parentList.Contains(v.value) == true)
            {
                return v.value;
            }

            if (v.parent == null)
            {
                return -1;
            }

            return FindLca(v.parent);
        }
        public static void FindLca()
        {
            int u, v;
            string[] tmp;
            Console.WriteLine("Find LCA of");
            Console.WriteLine("Node u   node v");
            tmp = fileObj.ReadLine().Split(" ");
            Console.WriteLine("{0} {1}", tmp[0], tmp[1]);
            u = Convert.ToInt32(tmp[0]);
            v = Convert.ToInt32(tmp[1]);
            FindLca(myTree[u], myTree[v]);
        }
        static void Main(string[] args)
        {
            string temp;
            GetInput();
            while (true)
            {
                Console.WriteLine("Want to find LCA between two node? (Y/N)");
                temp = fileObj.ReadLine();
                Console.WriteLine(temp);
                if (temp.ToLower() != "y")
                {
                    break;
                }                    
                FindLca();
            }
            DeleteTree();
        }
    }
}
