using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
namespace Sapp
{
    static class Settings
    {
        public static bool QuickSwitch = true;
        public static string Library = "G15";
        public static List<MessageSet> MessageSets;

        private static readonly string settingsFileName = "settings.xml";

        public static void Load()
        {
            MessageSets = new List<MessageSet>();

            //Read file
            FileStream file = new FileStream(settingsFileName, FileMode.OpenOrCreate);
            
            XDocument doc;
            try
            {
                doc = XDocument.Load(file);
            }
            catch (Exception)
            {
                doc = new XDocument();
            }

            //Get root
            XElement settings = doc.Element("settings");
            if (settings == null)
            {
                settings = new XElement("settings");
                doc.Add(settings);
            }

            //read quickswitch
            XElement quickswitch = settings.Element("quickswitch");
            if (quickswitch == null)
            {
                quickswitch = new XElement("quickswitch", QuickSwitch.ToString());
                settings.Add(quickswitch);
            }
            Boolean.TryParse(quickswitch.Value, out QuickSwitch);

            //Read library
            XElement library = settings.Element("library");
            if (library == null)
            {
                library = new XElement("library", Library);
                settings.Add(library);
            }
            Library = library.Value;

            XElement sets = settings.Element("sets");
            if (sets == null)
            {
                sets = new XElement("sets");
                settings.Add(sets);
            }

            foreach (XElement set in sets.Elements("set"))
            {
                XAttribute setName = set.Attribute("name");
                XAttribute setName1 = set.Attribute("n1");
                XAttribute setContent1 = set.Attribute("v1");
                XAttribute setName2 = set.Attribute("n2");
                XAttribute setContent2 = set.Attribute("v2");

                MessageSets.Add(new MessageSet(
                    setName == null ? "Unknown" : setName.Value,
                    setName1 == null ? "" : setName1.Value,
                    setContent1 == null ? "" : setContent1.Value,
                    setName2 == null ? "" : setName2.Value,
                    setContent2 == null ? "" : setContent2.Value));
            }
            file.SetLength(0);
            doc.Save(file);
            file.Close();
        }

        public static void Save()
        {
            //Read file
            FileStream file = new FileStream(settingsFileName, FileMode.OpenOrCreate);
            XDocument doc = new XDocument();

            //Get root
            XElement settings = new XElement("settings");
            doc.Add(settings);

            //read quickswitch
            XElement quickswitch = new XElement("quickswitch", QuickSwitch.ToString());
            settings.Add(quickswitch);
        
            //Read library
            XElement library = new XElement("library", Library);
            settings.Add(library);

            XElement sets = new XElement("sets");
            settings.Add(sets);

            //Message sets
            foreach (MessageSet m in MessageSets)
            {
                XElement ms = new XElement("set");
                ms.SetAttributeValue("name", m.Name);
                ms.SetAttributeValue("n1", m.MessageOneName);
                ms.SetAttributeValue("v1", m.MessageOneContent);
                ms.SetAttributeValue("n2", m.MessageTwoName);
                ms.SetAttributeValue("v2", m.MessageTwoContent);

                sets.Add(ms);
            }

            file.SetLength(0);
            doc.Save(file);
            file.Close();

        }

        public static bool FileExists()
        {
            return File.Exists(settingsFileName);
        }
    }
}
