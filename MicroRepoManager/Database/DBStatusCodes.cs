namespace MicroRepoManager.Database
{
    /// <summary>
    /// Status codes for DB error in action
    /// </summary>
    public enum DbStatusCodes
    {
        Ok=0,
        GeneralError=1,
        KeyNotFoundError=2,
        DuplicateKeyError=3
    }
}