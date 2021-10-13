using System;
using System.Collections.Generic;
using MicroRepoManager.Objects;

namespace MicroRepoManager.Database
{
    /// <summary>
    /// Instantiate-able Db Object that saved in memory.
    /// </summary>
    public class MemDb : BaseDbClass
    {
        /// <summary>
        /// Dict object that store all objects.
        /// </summary>
        private Dictionary<string, BaseObjectClass> _dbUnit = new();
        
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
        /// Exception-less implementation of AddObject
        /// </summary>
        /// <param name="objectId"> The Item's name</param>
        /// <param name="newObject"> The object instance.</param>
        /// <param name="code">Insertion status.</param>
        /// <seealso cref="MemDb.AddObject(string, BaseObjectClass)"/>
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
        /// <para>(This in an extra function to directly access data objects)</para>
        /// Common way to get item content is by using <see cref="GetObjectContent(string)"/>
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>Instance Object.</returns>
        /// <exception cref="ArgumentException">Throw exception whenever objectID is not found in DB.</exception>
        /// <example>To get item content : <see cref="GetObject(string)"/>.<see cref="BaseObjectClass.GetItemContent()"/></example>
        // Might be better to set it to private (Change abstract class too) in order too maintain encapsulation ?
        public override BaseObjectClass GetObject(string objectId) 
        {
            switch (CheckKey(objectId))
            {
                case true:  // return the object
                    return _dbUnit[objectId];
                case false: // Exception if key not found
                    // -- Does not implement error code yet. Throwing... --
                    throw new ArgumentException($"ItemName \"{objectId}\" does not exist in DB.");
            }
        }

        /// <summary>
        /// Get the object's content by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>The Item Content.</returns>
        /// <remarks>Another way to get item content is by using <see cref="GetObject(string)"/>.<see cref="BaseObjectClass.GetItemContent()"/></remarks>
        // Might need to adapt return string with corresponding itemType (if necessary) - #FUTURE
        public override string GetObjectContent(string objectId)
        {
            return GetObject(objectId).GetItemContent();
        }

        /// <summary>
        /// Get the object's type by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>The Item type.</returns>
        public override int GetObjectType(string objectId)
        {
            return GetObject(objectId).GetItemType();
        } 
        
        /// <summary>
        /// Get the object content by itemName. <br></br>
        /// (An Exception-less implementation of <see cref="GetObjectContent(string)"/> with error codes)
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <param name="code">Retrieval Status.</param>
        /// <returns>The Item Content.</returns>
        // (Not optimized, [exception-less] GetObject() not implemented yet))
        public string GetObjectContent(string objectId, out DbStatusCodes code)
        {
            switch (CheckKey(objectId))
            {
                case true:  // return the object
                    code = DbStatusCodes.Ok;
                    return _dbUnit[objectId].GetItemContent();
                case false: // return null if data not found
                    code = DbStatusCodes.KeyNotFoundError;
                    return null;
            }
        }
        
        /// <summary>
        /// Delete an Object from DB
        /// </summary>
        /// <param name="objectId">The name/id of the item/object to be deleted</param>
        public override void DeleteObject(string objectId)
        {
            if (CheckKey(objectId, true)) // Throw when key is not found.
            {
                _dbUnit.Remove(objectId);
            }
        }

        /// <summary>
        /// Exception-less implementation of <see cref="MemDb.DeleteObject(string)"/>
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

            if (isKeyExist) // If key truly exist, return true
            {
                return true;
            }
            else // Otherwise throw exception (no error code yet, only throw)
            {
                throw new ArgumentException($"ItemName \"{objectId}\" does not exist in DB."); // otherwise, throw exception
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