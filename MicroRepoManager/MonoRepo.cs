// Single DB Implementation of MicroRepoManager
// Updated to support multiple DB configuration
// (Does not included in Unit Testing)
// Maybe useful if single DB implementation is a must.

// #define TESTING
using System;
using MicroRepoManager.Database;
using MicroRepoManager.Objects;

namespace MicroRepoManager
{
    /// <summary>
    /// Main class to accomodate user interaction
    /// </summary>
    public static class MonoRepo
    {
        /// <summary>
        /// Register a new object to the repo.
        /// </summary>
        /// <param name="itemName">(string)</param>
        /// <param name="itemContent">(string)</param>
        /// <param name="itemType">(int) 1. Json 2. Xml</param>
        /// <example><c>Register("ItemIdentifier", "{...}", 1);</c></example>
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
        /// <example>Retrieve("Object1Name");</example>
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
        /// <example>GetType("Object1Name");</example>
        public static int GetType(string itemName)
            // Still confused whether it is better to implement this method in here or DB class.
        {
            return LocalDb.GetObject(itemName).GetItemType();
        }
        
        /// <summary>
        /// Deregister an item from DB.
        /// </summary>
        /// <param name="itemName"> The name of the item to be deleted.</param>
        /// <example>Deregister("Object1Name");</example>
        public static void Deregister(string itemName)
        {
            LocalDb.DeleteObject(itemName);
        }
    }
}