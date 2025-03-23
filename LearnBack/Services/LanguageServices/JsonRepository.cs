using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LanguageServices
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _filePath;

        public JsonRepository()
        {

            _filePath = Path.Combine("", "C:\\Hadary\\Personal\\Learn\\Learn\\Data\\Data.json");
        }

        public IEnumerable<T> ReadItems()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("JSON file not found.");
            }

            using (FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs))
            {
                string content = sr.ReadToEnd();
                return JsonSerializer.Deserialize<IEnumerable<T>>(content) ?? default;
            }

        }

        public bool UpdateItem(T obj)
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("JSON file not found.");
            }
            string json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<IEnumerable<T>>(json) ?? default;

            if (data == null) return false;

            var element = data.FirstOrDefault(x => GetPropertyValue(x,"Id") == GetPropertyValue(obj, "Id"));
            
            if (element == null) return false;

            json = JsonSerializer.Serialize(data.Except(new List<T> { element }).Append(obj));

            File.WriteAllText(_filePath, json);
            return true;
        }
        public static int? GetPropertyValue<T>(T obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");

            return (int)propertyInfo.GetValue(obj);
        }

    }
}
