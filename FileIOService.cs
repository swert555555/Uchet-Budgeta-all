using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace WpfAppPract4
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public T Deserialize<T>()
        {
            string text = File.ReadAllText(PATH);
            T result = JsonConvert.DeserializeObject<T>(text);
            return result;
        }
        public void Serialize<T>(T tasks)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string json = JsonConvert.SerializeObject(tasks);
                writer.WriteLine(json);
            }
        }
    }
}
