using Newtonsoft.Json;
using System.IO;

namespace HappiNESs
{
    /// <summary>
    /// Handles Json convertions
    /// </summary>
    public class JsonManager : ISerializer
    {
        /// <summary>
        /// Serialize an object to a Json string
        /// </summary>
        /// <param name="Data">The data to be converted</param>
        /// <returns></returns>
        public string Convert(object Data)
        {
            var sw = new StringWriter();

            // Use stream to convert
            using (var textWriter = new JsonTextWriter(sw))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(textWriter, Data);
            }

            return sw.ToString();
        }

        /// <summary>
        /// Restore a Json string to it Object
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="Json">The json string to be restored</param>
        /// <returns></returns>
        public object Restore<T>(string String)
        {
            return JsonConvert.DeserializeObject<T>(String);
        }
    }
}
