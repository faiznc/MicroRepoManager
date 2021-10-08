using System;
using System.Collections.Generic;

namespace MicroRepoManager
{
    // todo need downcast to be able properly use a class (string object for example) methods
    // because of the saved one is the baseclass
    
    // nanti register di class baru yg ngontrol kedua komponen ini (DB dan Object nya)
    
    public static class LocalDB
    {
        private static Dictionary<string, BaseObjectClass> MemDB = new Dictionary<string, BaseObjectClass>();

        public static void AddObject(string objectID, BaseObjectClass newObject)
        {
            MemDB.Add(objectID, newObject);
        }

        public static bool CheckKey(string objectID)    // return true if exist, false if not exist
        {
            bool tempCheckKey;
            tempCheckKey = MemDB.ContainsKey(objectID);
            return tempCheckKey;
        }

        public static BaseObjectClass GetObject(string objectID)
        {
            switch (CheckKey(objectID))
            {
                case true:  // return the object
                    return MemDB[objectID];
                case false: // return null if data not found // todo careful with implementation
                    return null;
            }
        }
        
        
    }
}