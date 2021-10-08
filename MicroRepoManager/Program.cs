using System;
using MicroRepoManager.ObjectClasses;

namespace MicroRepoManager
{
    class Program
    {
        private const string Marker = "-----------------------------"; 
        private static void Main(string[] args)
        {
            // todo class BARU untuk atur input / sebagai main menu nya
            
            Console.WriteLine("Hello World!");
            
            StringObject data1 = new StringObject("1", "Content 1", 2);

            #region TestPrint data1
            Console.WriteLine(Marker);
            Console.WriteLine(data1.GetItemName());
            Console.WriteLine(data1.GetItemContent());
            Console.WriteLine(data1.GetItemType());
            Console.WriteLine(Marker);
            #endregion            
            
            //todo set itemName dan objectID nya sama - for future usage...
            LocalDB.AddObject("1", data1);
            
            var newObj = LocalDB.GetObject("1");
            Console.WriteLine(newObj.GetType());        // MicroRepoManager.StringObject

            //try downcasting todo wait INI KOK UDAH lgsg STRINGOBJECT ya ?!?!
            StringObject MyString = (StringObject) newObj;
            Console.WriteLine(MyString.GetType());      // MicroRepoManager.StringObject

        }
    }
}