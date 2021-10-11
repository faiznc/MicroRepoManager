namespace MicroRepoManager
{
    /// <summary>
    /// Basic interface for objects that saved in (Volatile) memory.
    /// </summary>
    public interface IBaseObjectMem
    {
        /// <summary>
        /// Default method to get item name
        /// </summary>
        string GetItemName();
        /// <summary>
        /// Default method to get item content on (string) data type
        /// </summary>
        /// <returns></returns>
        string GetItemContent();
        /// <summary>
        /// Default method to get item type
        /// </summary>
        /// <returns>(int) itemType</returns>
        int GetItemType();
    }
}