namespace MicroRepoManager.Objects
{
    /// <summary>
    /// A class to store XML object.
    /// </summary>
    public class XmlObject : BaseObjectClass, IBaseObjectMem
    {

        /// <summary>
        /// Constructor of XmlObject (Default to base class <see cref="BaseObjectClass"/>)
        /// </summary>
        /// <param name="itemName"> Item's identifier (string)</param>
        /// <param name="itemContent">Item's content (string)</param>
        /// <param name="itemType">Item's type (int)</param>
        public XmlObject(string itemName, string itemContent, int itemType) : base(itemName, itemContent, itemType)
        {
        }
    }
}