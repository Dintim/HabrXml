using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HabrWpfApp
{
    public class Helper
    {
        public static List<Item> ReadXml(string url, out Channel channel)
        {
            List<Item> items = new List<Item>();
            Channel ch = new Channel();

            XmlDocument doc = new XmlDocument();
            doc.Load(url);

            var xChannel= doc.DocumentElement.FirstChild;
            

            foreach (XmlNode xItem in doc.DocumentElement.FirstChild.ChildNodes)
            {
                if (xItem.Name == "title")
                    ch.Title = xItem.InnerText;
                else if (xItem.Name == "description")
                    ch.Description = xItem.InnerText;
                else if (xItem.Name == "managingEditor")
                    ch.ManagingEditor = xItem.InnerText;
                else if (xItem.Name == "generator")
                    ch.Generator = xItem.InnerText;
                else if (xItem.Name == "pubDate")
                    ch.PubDate = xItem.InnerText;                
                else if (xItem.Name == "item")
                {
                    Item item = new Item();
                    foreach (XmlNode xChild in xItem.ChildNodes)
                    {
                        if (xChild.Name == "title")
                            item.Title = xChild.InnerText;
                        else if (xChild.Name == "link")
                            item.Link = xChild.InnerText;
                        else if (xChild.Name == "description")
                            item.Description = xChild.InnerText;
                        else if (xChild.Name == "pubDate")
                            item.PubDate = xChild.InnerText;

                        if (item.Title != null && item.Link != null && item.Description != null && item.PubDate != null)
                            break;
                    }

                    items.Add(item);
                }
            }

            channel = ch;
            return items;
        }

        public static bool SerializeItems(List<Item> items, out string message)
        {            
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Item>));
                using (FileStream fStream = new FileStream("items.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fStream, items);
                }

                message = "Коллекция сериализована успешно. См items.xml";
                return true;
            }
            catch (Exception)
            {
                message = "Произошла ошибка. Коллекция не сериализована.";
                return false;
            }
            
        }
    }
}
