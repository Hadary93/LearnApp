using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ContentProcessing.Lib
{
    public class HTMLreader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath">folder container the target files</param>
        /// <param name="dataType">data type with the trailing dot</param>
        public static IEnumerable<(HtmlDocument htmlDoc, string fileName)>? ReadFiles(string folderPath, params string[] dataType)
        {
            if (Directory.Exists(folderPath))
            {
                return Directory.EnumerateFiles(folderPath).Where(x => dataType.Contains(new FileInfo(x).Extension)).Select(x =>
                {
                    var html = new HtmlDocument();
                    html.Load(x);
                    return (html, new FileInfo(x).Name);
                });
            };
            return default;
        }
    }
}
