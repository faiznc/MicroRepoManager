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
            
            // Add StringObject intstance
            StringObject data1 = new StringObject("1", "Content 1", 2);
            // Add JSONObject intstance
            JSONObject data2 = new JSONObject("2", "Content 2", 1);
            
            #region TestPrint data1
            Console.WriteLine(Marker);
            Console.WriteLine(data1.GetItemName());
            Console.WriteLine(data1.GetItemContent());
            Console.WriteLine(data1.GetItemType());
            Console.WriteLine(Marker);
            #endregion            
            
            //todo set itemName dan objectID nya sama - for future usage...
            // maksudnya, di main function nya nanti, itemName sama objectID nya disamain aja
            
            // add data1 ke db >> STRING
            LocalDB.AddObject("1str", data1);
            
            // add data2 ke db >> JSON
            LocalDB.AddObject("2json", data2);
            
            // ambil OBJECT nya
            var newSTRObj = LocalDB.GetObject("1str");
            Console.WriteLine(newSTRObj.GetType());        // MicroRepoManager.StringObject
            // Console.WriteLine(newSTRObj.FungsiAsal());      // ERROR DISINI (NO DOWNCAST)

            var newJSONObj = LocalDB.GetObject("2json");
            Console.WriteLine(newJSONObj.GetType());

            //try downcasting
            // todo wait INI KOK UDAH lgsg STRINGOBJECT ya ?!?!
            // TETAP PENTING YAA (Coba liat implementasi MyString dan newSTRObj)
            // apakah ngaruh ke fungsi (autocomplete?)
            StringObject MyString = (StringObject) newSTRObj;
            Console.WriteLine(MyString.GetType());      // MicroRepoManager.StringObject
            Console.WriteLine(MyString.FungsiAsal());   // DISINI GAK ERROR (Setelah downcast)

            // skrg joba JSONObject
            
        }
    }
}