using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserSettings.Exceptions;

namespace UserSettings.Test
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void SettingLoadingTest()
        {
            SettingsStorage settings = new SettingsStorage(new MockSettingLoader());
            //settings.Load();
            Assert.IsTrue(settings.IsLoaded);
            Assert.AreEqual(settings.GetSettingsCount(), 1);
            settings.AddSetting(new Setting("Y", 10));
            Assert.AreEqual(settings.GetSetting("Y").GetValue(),10);

            FailedWith(() => settings.AddSetting(new Setting("Y", 10)),
                typeof(AddSettingException));

            settings.SetSetting("X", 111);
            Assert.AreEqual(settings.GetSetting("X").GetValue(),111);

            FailedWith(()=>settings.SetSetting("A",123),typeof (SettingNotFoundException));
        }

        [TestMethod]
        public void XmlSettingsTest()
        {
            SettingsStorage settings = new SettingsStorage(new XmlSettingsProvider(true));
            Assert.IsTrue(File.Exists(Path.GetFullPath("settings.xml")));
            //settings.Load();
            Assert.AreEqual(settings.GetSettingsCount(), 0);
            settings.AddSetting(new Setting("X","Y"));

            SettingsStorage settings1 = new SettingsStorage(new XmlSettingsProvider(true));
            //settings1.Load();
            Assert.AreNotEqual(settings1.GetSettingsCount(), 0);
        }

        public void FailedWith(Action action, Type exceptionType)
        {
            try
            {
                action.Invoke();
                Assert.Fail("Action is not failed!");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, exceptionType);
            }
        }

    }

    public class MockSettingLoader:ISettingsProvider
    {
        public bool Load()
        {
            return true;
        }

        public ArrayList GetSettings()
        {
            return new ArrayList()
                       {
                           new Setting("X",2),
                       };
        }

        public void WriteSetting(ISetting setting)
        {
            
        }

        public void EraseSetting(ISetting setting)
        {
            
        }
    }

}
