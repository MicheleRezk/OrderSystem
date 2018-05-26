using Newtonsoft.Json;
using OrderSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application.Services
{
    public class JsonService : IJsonService
    {
        /// <summary>
        /// Read Json file and deserlize it to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath">path of the json file</param>
        /// <returns></returns>
        public T ReadJsonFile<T>(string filePath) where T : class
        {
            T result = default(T);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Cannot locate the Json file:\n   \"" + filePath + "\"\nPlease check the path and try again.");
            else
            {
                var jsonContent = File.ReadAllText(filePath);
                if (jsonContent.Length > 0)
                {
                    result = JsonConvert.DeserializeObject<T>(jsonContent);
                }
            }
            return result;
        }

        public void WriteJsonFile<T>(T data, string filePath) where T : class
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Cannot locate the Json file:\n   \"" + filePath + "\"\nPlease check the path and try again.");
            else
            {
                File.Delete(filePath);
                using (StreamWriter file = File.CreateText(filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, data);
                }
            }
        }
    }
}
