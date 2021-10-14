// #define TESTING // For General Testing
// #define TESTING_VALIDS // Testing data type validity (simple) log

using System;
using MicroRepoManager.Database;
using MicroRepoManager.Objects;

namespace MicroRepoManager
{
    /// <summary>
    /// Main class to accomodate repo management <br></br>
    /// <para></para>
    /// This class control a Db object and manage it accordingly.
    /// </summary>
    public class Repo
    {
        /// <summary>
        /// Target db object to control.
        /// </summary>
        private MemDb _dbObject;
        
        public Repo(ref MemDb dbObject)
        {
            #if TESTING
            var equals = ReferenceEquals(_dbObject, dbObject);
            Console.WriteLine($"Reference DB Before set, is_equal = {equals}");
            
            _dbObject = dbObject; // Setting the same reference (Before and After Checking)
            
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
        /// Register a new object to the repo.
        /// </summary>
        /// <param name="itemName">(string)</param>
        /// <param name="itemContent">(string)</param>
        /// <param name="itemType">(int) 1. Json 2. Xml</param>
        /// <example><c>Register("ItemIdentifier", "{...}", 1);</c></example>
        public void Register(string itemName, string itemContent, int itemType)
        {
            // First, check whether itemType is exist in itemTypes enum
            // Might be better to implement general checking in another methods
            var typeExist = Enum.IsDefined(typeof(ItemTypes), itemType);
            var inputDataType = (ItemTypes) itemType;
            
            if (!typeExist) // Stop operation if type is not exist. 
            {
                Console.WriteLine("Item Type is not registered yet.");
                return; 
            }

            #region (Testing)
            #if TESTING
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Object identified as {(ItemTypes) itemType}");
            Console.ResetColor();
            #endif
            #endregion
            
            var isValid = Validator.Validate(itemContent, inputDataType);
            
            #region (Testing Data Validity)
            #if TESTING_VALIDS
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"itemName \"{itemName}\" Valid = {isValid}");
            Console.ResetColor();
            #endif
            #endregion
            
            if (!isValid) // Stop operation when data is not valid.
            {
                Console.WriteLine($"String is incorrectly formatted {inputDataType}.");
                return; 
            }
            
            switch (inputDataType)
            {
                case ItemTypes.Json:
                    _dbObject.AddObject(itemName, new JsonObject(itemName, itemContent, itemType));
                    break;
                case ItemTypes.Xml:
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
            return _dbObject.GetObjectContent(itemName);
        }
        
        /// <summary>
        /// Get the type of the item. 
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <returns>The item types (Json = 1 ,Xml = 2, etc...).</returns>
        /// <example>GetType("Object1Name");</example>
        public int GetType(string itemName) 
        {
            return _dbObject.GetObjectType(itemName);
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
    }
}