using MicroRepoManager.Objects;

namespace MicroRepoManager.Database
{
    /// <summary>
    /// Basic template for all DB class.
    /// (Including LocalDB)
    /// </summary>
    public abstract class BaseDbClass
    {
        /// <summary>
        /// Add a new object to DB.
        /// </summary>
        /// <param name="objectId"> The Object's ID or Item's name</param>
        /// <param name="newObject"> The object instance.</param>
        public abstract void AddObject(string objectId, BaseObjectClass newObject);
        
        /// <summary>
        /// Get the object instance by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>Instance Object.</returns>
        public abstract BaseObjectClass GetObject(string objectId);

        /// <summary>
        /// Get the object content by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>The Item Content.</returns>
        public abstract string GetObjectContent(string objectId);
        
        /// <summary>
        /// Get the object type by itemName.
        /// </summary>
        /// <param name="objectId">the ID of the object to retrieve.</param>
        /// <returns>The Item type.</returns>
        public abstract int GetObjectType(string objectId);
        
        /// <summary>
        /// Delete and Object from DB
        /// </summary>
        /// <param name="objectId">The name/id of the item/object to be deleted</param>
        public abstract void DeleteObject(string objectId);
        
        /// <summary>
        /// Check whether an itemName (or objectID) is present in DB or not
        /// </summary>
        /// <param name="objectId">The Object id or Key to search</param>
        /// <param name="throwable">If true, will throw if key not found. If false, only return true or false.</param>
        /// <returns>Object existence in database.</returns>
        public abstract bool CheckKey(string objectId, bool throwable = false);

    }
}