// #define TESTING

using System;
using System.Collections.Generic;
using MicroRepoManager.Objects;

namespace MicroRepoManager.Database
{
    // todo FUTURE - need downcast to be able properly use a class (string object for example) methods because DB is only saving the BaseObjectClass type
    
    /// <summary>
    /// Simple DB to accomodate data saving. (Saved on Volatile Memory) 
    /// </summary>
    public static class LocalDb
    {
        /// <summary>
        /// Local Dict that store all objects.
        /// </summary>
        private static Dictionary<string, BaseObjectClass> MemDB = new();

        /// <summary>
        /// Add a new object to DB.
        /// </summary>
        /// <param name="objectID"> The Object's ID or Item's name</param>
        /// <param name="newObject"> The object instance.</param>
        /// <exception cref="ArgumentException">Throw exception whenever objectID is already found in DB.</exception>
        public static void AddObject(string objectID, BaseObjectClass newObject)
        {
            if (!CheckKey(objectID)) // if key does not exist, add to DB
            {
                MemDB.Add(objectID, newObject);
            }
            else // if already exist, throw exception.
            {
                throw new ArgumentException($"ItemName \"{objectID}\" already exist.");
            }
        }

        /// <summary>
        /// Get the object instance by itemName.
        /// </summary>
        /// <param name="objectID"></param>
        /// <returns></returns>
        public static BaseObjectClass GetObject(string objectID)
        {
            switch (CheckKey(objectID))
            {
                case true:  // return the object
                    return MemDB[objectID];
                case false: // return null if data not found // todo careful with implementation (give null coalensing).
                    return null;
            }
        }
        /// <summary>
        /// Delete and Object from DB
        /// </summary>
        /// <param name="objectID">The name/id of the item/object to be deleted</param>
        public static void DeleteObject(string objectID)
        {
            var exist = CheckKey(objectID);
            if (!exist) return;
            var delCheck = MemDB.Remove(objectID);
            
#if TESTING
            Console.WriteLine($"Delete success : {delCheck}");
#endif
        }

        /// <summary>
        /// Check whether an itemName (or objectID) is present in DB or not
        /// </summary>
        /// <param name="objectID"> The item's name or objectID</param>
        /// <returns>true if objectID exist, false if not exist.</returns>
        private static bool CheckKey(string objectID)    // return true if exist, false if not exist
        {
            return MemDB.ContainsKey(objectID);
        }
        
        /// <summary>
        /// Get DB's length.
        /// </summary>
        /// <returns>DB's item count</returns>
        public static int GetDBLength()  // Testing purposes.
        {
            return MemDB.Count;
        }
    }
}