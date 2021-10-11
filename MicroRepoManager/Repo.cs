#define TESTING
using System;
using MicroRepoManager.Database;
using MicroRepoManager.Objects;

namespace MicroRepoManager
{
    public enum ItemTypes // Maybe better to make new static class to save static variables... ?
    {
        Json=1,
        Xml=2
    }
    
    /// <summary>
    /// Main class to accomodate user interaction (Multiple connection to accomodate multiple users ?)
    /// </summary>
    public static class Repo
    {

        /// <summary>
        /// Repo's main menu (For Manual Console User Interaction) - Class is NOT IMPLEMENTED YET
        /// </summary>
        public static void WelcomeMenu()
        {
            string WelcomeMessage = "Welcome to the MicroRepoManager !";
            string OptionsMessage = "What to do?\n 1. Insert data \n 2. Get data \n 3. Delete data";
            string InputMessage = "(1-3) : ";
            
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine(OptionsMessage);
            Console.Write(InputMessage);
            
            var chosenOptionInt = Console.Read();
            switch (chosenOptionInt) // ASCII
            {
                case 49:
                    Console.WriteLine("Opsi 1 ( Insert Data chosen )");
                    break;
                case 50:
                    Console.WriteLine("Opsi 2 ( Get Data )");
                    break;
                case 51:
                    Console.WriteLine("Opsi 3 ( Delete Data)");
                    break;
            }
        }

        /// <summary>
        /// Register a new object to the repo.
        /// </summary>
        /// <param name="itemName">(string)</param>
        /// <param name="itemContent">(string)</param>
        /// <param name="itemType">(int) 1. Json 2. Xml</param>
        public static void Register(string itemName, string itemContent, int itemType)
        {
            // First, check whether itemType is exist in itemTypes enum
            var typeExist = Enum.IsDefined(typeof(ItemTypes), itemType);
            if (!typeExist) 
            {
                Console.WriteLine("Item Type is not registered yet.");
                return;
            }

            #region ~Testing~
#if TESTING
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Object identified as {(ItemTypes) itemType}");
            Console.ResetColor();
#endif
            #endregion
            
            // todo data checking here
            // check validity of json or xml (either here or their base class) 
            
            // todo NEW IDEA >>> dynamic code instead of switch, using previous enum to get item type
            // still NO IDEA on how to efficiently code this section, but for now *KISS* principle
            
            switch (itemType)
            {
                case 1:
                    LocalDb.AddObject(itemName, new JsonObject(itemName, itemContent, itemType));
                    break;
                case 2:
                    LocalDb.AddObject(itemName, new XmlObject(itemName, itemContent, itemType));
                    break;
            }
        }

        /// <summary>
        /// Retrieve item's content/data from DB 
        /// </summary>
        /// <param name="itemName"> The name of the item to retrieve.</param>
        /// <returns>The itemContent or Data</returns>
        public static string Retrieve(string itemName)
        {
            // todo adapt return string with corresponding itemType (if necessary) - #FUTURE
            return LocalDb.GetObject(itemName).GetItemContent();
        }
        
        /// <summary>
        /// Get the type  of the item. 
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <returns>The item types (Json = 1 ,Xml = 2, etc...).</returns>
        public static int GetType(string itemName)
            // Still confused whether it is better to implement this method in here or DB class.
        {
            return LocalDb.GetObject(itemName).GetItemType();
        }
        
        /// <summary>
        /// Deregister an item from DB.
        /// </summary>
        /// <param name="itemName"> The name of the item to be deleted.</param>
        public static void Deregister(string itemName)
        {
            LocalDb.DeleteObject(itemName);
        }
    }
}