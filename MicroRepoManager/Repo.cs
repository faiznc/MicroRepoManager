// #define TESTING
#define TESTING_VALIDS
using System;
using System.Data;
using MicroRepoManager.Database;
using MicroRepoManager.Objects;


// coba dipikir lagi
// masak ada multiple repo untuk ngakses multiple DB ?
// padahal kan adanya multiple DB untuk 1 repo aja..

// atau nanti kita pake "out" untuk nge reference ke DB nya lgsg 
// jadi nanti kita set objek memory mana yg akan di aksesk gitu...

// kalo bisa jgn pake out sih, soale implementasinya bakal ada di semua kodingan
// kalo bisa pake setter getter aja memory nya...

namespace MicroRepoManager
{
    /// <summary>
    /// Main class to accomodate repo management
    /// </summary>
    public class Repo
    {

        private MemDb _dbObject;
        public Repo(ref MemDb dbObject)
        {
            #if TESTING
            var equals = ReferenceEquals(_dbObject, dbObject);
            Console.WriteLine($"Reference DB Before set, is_equal = {equals}");
            
            _dbObject = dbObject; // Setting the same reference
            
            equals = ReferenceEquals(_dbObject, dbObject);
            Console.WriteLine($"Reference DB After set is_equal = {equals}");
            
            #else
            _dbObject = dbObject; // Setting the same reference
            #endif
        }

        /// <summary>
        /// Set the new DB reference for repo's driver.
        /// </summary>
        /// <param name="newDbRef">DB Object</param>
        public void SetDbRef(ref MemDb newDbRef)
        {
            _dbObject = newDbRef;
        }
        
        /// <summary>
        /// Repo's main menu (For Manual Console User Interaction) - Class is NOT IMPLEMENTED YET
        /// </summary>
        public static void WelcomeMenu()
        {
            string welcomeMessage = "Welcome to the MicroRepoManager !";
            string optionsMessage = "What to do?\n 1. Insert data \n 2. Get data \n 3. Delete data";
            string inputMessage = "(1-3) : ";
            
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(optionsMessage);
            Console.Write(inputMessage);
            
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
        /// <example><c>Register("ItemIdentifier", "{...}", 1);</c></example>
        public void Register(string itemName, string itemContent, int itemType)
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
            bool isValid;
            
            switch (itemType)
            {
                case 1: // JSON Object
                    #if TESTING_VALIDS
                    isValid = Validator.IsValidJson(itemContent);
                    Console.WriteLine($"Valid JSON = {isValid}");
                    #endif
                    
                    _dbObject.AddObject(itemName, new JsonObject(itemName, itemContent, itemType));
                    break;
                case 2: // XML Object
                    #if TESTING_VALIDS
                    isValid = Validator.IsValidXml(itemContent);
                    Console.WriteLine($"Valid XML = {isValid}");
                    #endif
                    _dbObject.AddObject(itemName, new XmlObject(itemName, itemContent, itemType));
                    break;
            }
        }

        /// <summary>
        /// Retrieve item's content/data from DB 
        /// </summary>
        /// <param name="itemName"> The name of the item to retrieve.</param>
        /// <example>Retrieve("Object1Name");</example>
        /// <returns>The itemContent or Data</returns>
        public string Retrieve(string itemName)
        {
            // todo adapt return string with corresponding itemType (if necessary) - #FUTURE
            return _dbObject.GetObject(itemName).GetItemContent();
        }
        
        /// <summary>
        /// Get the type  of the item. 
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <returns>The item types (Json = 1 ,Xml = 2, etc...).</returns>
        /// <example>GetType("Object1Name");</example>
        public int GetType(string itemName)
            // Still confused whether it is better to implement this method in here or DB class.
        {
            return _dbObject.GetObject(itemName).GetItemType();
        }
        
        /// <summary>
        /// Deregister an item from DB.
        /// </summary>
        /// <param name="itemName"> The name of the item to be deleted.</param>
        /// <example>Deregister("Object1Name");</example>
        public void Deregister(string itemName)
        {
            _dbObject.DeleteObject(itemName);
        }
        
        // todo (Future) Add function to check some information about repo's driver >> GetInfo(){...}
    }
    
    
}