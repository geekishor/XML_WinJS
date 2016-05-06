using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage;

namespace WinRuntimes
{
    public sealed class Service
    {
        public IAsyncOperation<string> ReadXML()
        {
            return ReadXMLHelper().AsAsyncOperation();
        }

        private async Task<string> ReadXMLHelper()
        {
            try
            {
                StorageFile xmlFile = await Package.Current.InstalledLocation.GetFileAsync("test.xml");
                string xmlFileContent = await FileIO.ReadTextAsync(xmlFile);
                JObject obj = new JObject();

                // XML Read Operation
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlFileContent);
                
                XmlElement root = doc.DocumentElement;
                string account = root.GetAttribute("Account");
                string number = root.GetAttribute("Number");
                string dateTime = root.GetAttribute("DateTime");

                XmlNodeList nodes = root.SelectNodes("User");
                
                foreach (XmlElement item in nodes)
                {
                    var UserId = item.SelectNodes("UserId")[0].InnerText;
                    var UserName = item.SelectNodes("UserName")[0].InnerText;
                    
                    obj.Add("UserId", UserId);
                    obj.Add("UserName", UserName);
                }
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
