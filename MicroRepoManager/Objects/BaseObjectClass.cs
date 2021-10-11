using System;

namespace MicroRepoManager.Objects
{
    /// <summary>
    /// Base class for Objects in this repository.
    /// </summary>
    public abstract class BaseObjectClass
    {
        private readonly string _itemName;
        private readonly string _itemContent;
        private readonly byte _itemType;
        
        /// <summary>
        /// Base Class object initialization
        /// </summary>
        /// <param name="itemName"> Item's identifier (string)</param>
        /// <param name="itemContent">Item's content (string)</param>
        /// <param name="itemType">Item's type (int)</param>
        protected BaseObjectClass(string itemName, string itemContent, int itemType)
        {
            _itemName = itemName;
            _itemContent = itemContent;
            _itemType = (byte) itemType;
        }

        /// <summary>
        /// Get the ItemName or identifier
        /// </summary>
        /// <returns>itemName (string)</returns>
        public string GetItemName()
        {
            return _itemName;
        }
        
        /// <summary>
        /// Get the item Content
        /// </summary>
        /// <returns>itemContent (string)</returns>
        public string GetItemContent()
        {
            return _itemContent;
        }
        /// <summary>
        /// Get the Item's type
        /// </summary>
        /// <returns>itemType (int)</returns>
        public int GetItemType()
        {
            return _itemType;
        }
    }
}