using System;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MicroRepoManager
{
    public enum ValidityStatus // Error code usage, not implemented yet...
    {
        Valid=1, // Data is valid
        Invalid=0, // Data is invalid
        Null=2 // Data is null
    }
    
    public static class Validator
    {
        /// <summary>
        /// Main function to validate given string with its <see cref="ItemTypes"/>. <seealso cref="ItemTypes"/>
        /// </summary>
        /// <param name="dataInput">String to check</param>
        /// <param name="type">Input data type.</param>
        /// <returns></returns>
        public static bool Validate(string dataInput, ItemTypes type)
        {
            var isValid = false;
            switch (type)
            {
                case ItemTypes.Json:
                    isValid = IsValidJson(dataInput);
                    break;
                case ItemTypes.Xml:
                    isValid = IsValidXml(dataInput);
                    break;
            }
            return isValid;
        }
        
        
        /// <summary>
        /// Check whether a string is a valid json
        /// </summary>
        /// <param name="jsonInput">string to check</param>
        /// <returns>true if string is valid json, otherwise false.</returns>
        private static bool IsValidJson(string jsonInput)
            {
            if (string.IsNullOrWhiteSpace(jsonInput)) { return false;}
            jsonInput = jsonInput.Trim();
            if ((jsonInput.StartsWith("{") && jsonInput.EndsWith("}")) || //For object
                (jsonInput.StartsWith("[") && jsonInput.EndsWith("]"))) //For array
            {
                try
                {
                    JToken.Parse(jsonInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
            }

        
        /// <summary>
        /// Check whether a string is a valid xml
        /// </summary>
        /// <param name="xmlString">string to check</param>
        /// <returns>true if string is valid xml, otherwise false.</returns>
        private static bool IsValidXml(string xmlString)
        {
            // In here we use try catch because ...
            // "The small overhead from catching an exception drowns compared to parsing the XML."
            // https://stackoverflow.com/questions/86292/how-to-check-for-valid-xml-in-string-input-before-calling-loadxml
            try
            {
                new XmlDocument().LoadXml(xmlString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}