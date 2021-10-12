using System;
using System.Collections.Generic;
using MicroRepoManager.Objects;

namespace MicroRepoManager.Database
{
    /// <summary>
    /// Instantiate-able DB Object that saved in memory.
    /// </summary>
    public class MemDb : BaseDbClass
    {
        /// <summary>
        /// Dict object that store all objects.
        /// </summary>
        private static Dictionary<string, BaseObjectClass> _dbUnit = new();
        
        /// <summary>
        /// Add a new object to DB.
        /// </summary>
        /// <param name="objectId"> The Object's ID or Item's name</param>
        /// <param name="newObject"> The object instance.</param>
        /// <exception cref="ArgumentException">Throw exception whenever objectID is already found in DB.</exception>
        public override void AddObject(string objectId, BaseObjectClass newObject)
        {
            if (!CheckKey(objectId)) // if key does not exist, add to DB
            {
                _dbUnit.Add(objectId, newObject);
            }
            else // if already exist, throw exception.
            {
                throw new ArgumentException($"ItemName \"{objectId}\" already exist.");
            }
        }
        
        /// <summary>
        /// Safe implementation of AddObject
        /// </summary>
        /// <param name="objectId"> The Item's name</param>
        /// <param name="newObject"> The object instance.</param>
        /// <param name="code">Insertion status.</param>
        /// <seealso cref="MemDb.AddObject(string objectID, BaseObjectClass newObject)"/>
        public void AddObject(string objectId, BaseObjectClass newObject, out DbStatusCodes code)
        {
            if (!CheckKey(objectId)) // if key does not exist, add to DB
            {
                code = DbStatusCodes.Ok;
                _dbUnit.Add(objectId, newObject);
            }
            else // if already exist, set error codes
            {
                code = DbStatusCodes.DuplicateKeyError;
                // Does not throw an exception. Handled with status codes
            }
        }

        /// <summary>
        /// Get the object instance by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>Instance Object.</returns>
        /// <exception cref="ArgumentException">Throw exception whenever objectID is not found in DB.</exception>
        public override BaseObjectClass GetObject(string objectId)
        {
            switch (CheckKey(objectId))
            {
                case true:  // return the object
                    return _dbUnit[objectId];
                case false: // Exception if key not found
                    throw new ArgumentException($"ItemName \"{objectId}\" does not exist in DB.");
            }
        }

        /// <summary>
        /// Get the object content by itemName.
        /// <para>(This in an extra function to directly access object's content)</para>
        /// <para></para>
        /// Another way to get item content is by using <see cref="GetObject(string objectID)"/>.<see cref="BaseObjectClass.GetItemContent()"/>
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>The Item Content.</returns>
        public override string GetObjectContent(string objectId)
        {
            switch (CheckKey(objectId))
            {
                case true:  // return the object
                    return _dbUnit[objectId].GetItemContent();
                case false: // Exception if key not found
                    throw new ArgumentException($"ItemName \"{objectId}\" does not exist in DB.");
            }
        }

        /// <summary>
        /// Get the object content by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <param name="code">Retrieval Status.</param>
        /// <returns>The Item Content.</returns>
        public string GetObjectContent(string objectId, out DbStatusCodes code)
        {
            switch (CheckKey(objectId))
            {
                case true:  // return the object
                    code = DbStatusCodes.Ok;
                    return _dbUnit[objectId].GetItemContent();
                case false: // return null if data not found // todo careful with implementation (give null coalensing).
                    code = DbStatusCodes.KeyNotFoundError;
                    return null;
            }
        }
        
        
        /// <summary>
        /// Delete and Object from DB
        /// </summary>
        /// <param name="objectId">The name/id of the item/object to be deleted</param>
        public override void DeleteObject(string objectId)
        {
            if (CheckKey(objectId, throwable: true)) // Throw when key is not found.
            {
                _dbUnit.Remove(objectId);
            }

            #if TESTING
            Console.WriteLine($"Delete success : {delCheck}");
            #endif
        }

        /// <summary>
        /// Exception-less implementation of <see cref="MemDb.DeleteObject(string objectID)"/>
        /// </summary>
        /// <param name="objectId">The name/id of the item/object to be deleted</param>
        /// <param name="code">Deletion Status.</param>
        public void DeleteObject(string objectId, out DbStatusCodes code)
        {
            if (CheckKey(objectId))
            {
                _dbUnit.Remove(objectId);
                code = DbStatusCodes.Ok;
            }
            else
            {
                code = DbStatusCodes.KeyNotFoundError;
            }

#if TESTING
            Console.WriteLine($"Delete success : {delCheck}");
#endif
        }

        /// <summary>
        /// Check whether an itemName (or objectID) is present in DB or not
        /// </summary>
        /// <param name="objectId"> The item's name or objectID</param>
        /// <param name="throwable"> Enable Exception when key is not found (Default:false)</param>
        /// <returns>true if objectID exist, false if not exist.</returns>
        /// <exception cref="ArgumentException">Throw exception if objectID is not found (Default : Inactive).</exception>
        public override bool CheckKey(string objectId, bool throwable = false)
        {
            var isKeyExist = _dbUnit.ContainsKey(objectId);
            if (!throwable) // If not throwable, just return true or false
            {
                return isKeyExist;                
            }
            else // if exception is accepted, throw whenever key is not found
            {
                return isKeyExist
                    ? isKeyExist // if isKeyExist is True, return isKeyExist
                    : throw new ArgumentException($"ItemName \"{objectId}\" does not exist in DB."); // otherwise, throw exception
            }
        }
        
        /// <summary>
        /// Get DB's length.
        /// </summary>
        /// <returns>DB's item count</returns>
        public int GetDbLength()  // Development purposes.
        {
            return _dbUnit.Count;
        }
    }
}