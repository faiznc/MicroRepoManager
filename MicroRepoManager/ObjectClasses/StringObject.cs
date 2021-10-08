namespace MicroRepoManager.ObjectClasses
{
    /// <summary>
    /// A class to store string object.
    /// </summary>
    /// <param name="itemName"> Item's identifier (string)</param>
    /// <param name="itemContent">Item's content (string)</param>
    /// <param name="itemType">Item's type (int)</param>
    public class StringObject : BaseObjectClass, IBaseObjectMem
    {

        /// <summary>
        /// Constructor of StringObject (Default to BaseObjectClass)
        /// </summary>
        /// <param name="itemName"> Item's identifier (string)</param>
        /// <param name="itemContent">Item's content (string)</param>
        /// <param name="itemType">Item's type (int)</param>
        public StringObject(string itemName, string itemContent, int itemType) : base(itemName, itemContent, itemType)
        {
        }

        public string FungsiAsal()
        {
            return "String aja";
        }
    }
}