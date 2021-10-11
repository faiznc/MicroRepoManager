namespace MicroRepoManager.Objects
{
    /// <summary>
    /// A class to store JSON object.
    /// </summary>
    public class JsonObject : BaseObjectClass, IBaseObjectMem
    {
        /// <summary>
        /// Constructor of JsonObject (Default to base class <see cref="BaseObjectClass"/>)
        /// </summary>
        /// <param name="itemName"> Item's identifier (string)</param>
        /// <param name="itemContent">Item's content (string)</param>
        /// <param name="itemType">Item's type (int)</param>
        public JsonObject(string itemName, string itemContent, int itemType) : base(itemName, itemContent, itemType)
        {
        }
    }
}