using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LanguageServices
{
    public class XmlRepository<T> : IRepository<T>
    {
        private readonly string _filePath;

        public XmlRepository()
        {

            _filePath = Path.Combine("", "C:\\Hadary\\Personal\\Learn\\Learn\\Data\\Data.xml");
        }
        public IEnumerable<T>? ReadItems()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IEnumerable<T>), new XmlRootAttribute("Words"));

            using (FileStream fs = new FileStream(_filePath, FileMode.Open))
            {
                return (IEnumerable<T>?)serializer.Deserialize(fs)?? default(IEnumerable<T>);
            }
        }
    }
}
