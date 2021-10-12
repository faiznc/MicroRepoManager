using System;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MicroRepoManager
{
    // Future use...
    public enum ValidityStatus
    {
        Valid=1,
        Invalid=0,
        Null=2
    }
    public static class Validator
    {
        /// <summary>
        /// Check whether a string is a valid json
        /// </summary>
        /// <param name="strInput">string to check</param>
        /// <returns>true if string is valid, otherwise false.</returns>
        public static bool IsValidJson(string strInput)
            {
            if (string.IsNullOrWhiteSpace(strInput)) { return false;}
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    JToken.Parse(strInput);
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

        public static bool IsValidXml(string xmlString)
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