﻿namespace MicroRepoManager.ObjectClasses
{
    public class JSONObject : BaseObjectClass, IBaseObjectMem
    {
        /// <summary>
        /// Constructor of StringObject (Default to BaseObjectClass)
        /// </summary>
        /// <param name="itemName"> Item's identifier (string)</param>
        /// <param name="itemContent">Item's content (string)</param>
        /// <param name="itemType">Item's type (int)</param>
        public JSONObject(string itemName, string itemContent, int itemType) : base(itemName, itemContent, itemType)
        {
        }
    }
}