using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UserSettings
{
    public class XmlSettingsProvider : ISettingsProvider
    {
        private const String _path = "settings.xml";
        private XDocument _xml;
        private XElement _settingsElement;
        private XmlSettingPolicy _policy;

        public XmlSettingsProvider(Boolean createXmlIfNotExist)
        {
            _policy = new XmlSettingPolicy();
            if (createXmlIfNotExist)
            {
                var filePath = Path.GetFullPath(_path);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                    CreateXml();
                }
            }
        }

        private void CreateXml()
        {
            var xml = new XDocument();
            xml.Add(new XElement("Settings", ""));
            xml.Save(_path);
        }

        public bool Load()
        {
            try
            {
                _xml = XDocument.Load(_path);
                _settingsElement = _xml.Element("Settings");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ArrayList GetSettings()
        {
            var xmlSettings = GetAllXmlSettings();
            var settingsList = new ArrayList();
            foreach (var xmlSetting in xmlSettings)
            {
                ISetting setting = _policy.ConvertFromXml(xmlSetting);
                settingsList.Add(setting);
            }
            return settingsList;
        }

        public void WriteSetting(ISetting setting)
        {
            _settingsElement.Add(_policy.ConvertToXml(setting));
            _xml.Save(_path);
        }

        public void EraseSetting(ISetting setting)
        {
            var xmlSettings = GetAllXmlSettings();
            var currentXmlSetting = _policy.ConvertToXml(setting);
            foreach (var xmlSetting in xmlSettings)
            {
                if (_policy.AreEqual(xmlSetting, currentXmlSetting))
                {
                    xmlSetting.Remove();
                }
            }
        }

        private IEnumerable<XElement> GetAllXmlSettings()
        {
            return _settingsElement.Elements("Setting");
        }

        private sealed class XmlSettingPolicy
        {
            public XElement ConvertToXml(ISetting setting)
            {
                XElement xmlSetting = new XElement("Setting");
                xmlSetting.SetAttributeValue("Name", setting.GetName());
                xmlSetting.Value = setting.GetValue().ToString();
                return xmlSetting;
            }

            public ISetting ConvertFromXml(XElement xml)
            {
                var name = xml.Attribute("Name").Value;
                var value = xml.Value;

                Setting setting = new Setting(name, value);
                return setting;
            }

            public Boolean AreEqual(XElement xml1, XElement xml2)
            {
                return GetName(xml1).Equals(GetName(xml2));
            }

            private String GetName(XElement xml)
            {
                return xml.Attribute("Name").Value;
            }
        }
    }
}
