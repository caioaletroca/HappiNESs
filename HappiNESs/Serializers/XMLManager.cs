using System.IO;
using System.Xml.Serialization;

namespace HappiNESs
{
    /// <summary>
    /// Handles XML conversions
    /// </summary>
    public class XMLManager : ISerializer
    {
        /// <summary>
        /// Serialize an object to a string
        /// </summary>
        /// <param name="Data">The data to be converted</param>
        /// <returns></returns>
        public string Convert(object Data)
        {
            var xmlSerializer = new XmlSerializer(Data.GetType());
            
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, Data);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// Restore a string to it Object
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="Json">The json string to be restored</param>
        /// <returns></returns>
        public object Restore<T>(string Json)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var textReader = new StringReader(Json))
            {
                return xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
