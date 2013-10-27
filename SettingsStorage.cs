using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.Exceptions;

namespace UserSettings
{
    public class SettingsStorage
    {
        private readonly ISettingsProvider _provider;

        public SettingsStorage(ISettingsProvider provider)
        {
            _provider = provider;
            Load();
        }

        public  Boolean IsLoaded;
        public ArrayList Settings;

        private void Load()
        {
            IsLoaded = _provider.Load();
            if (IsLoaded)
            {
                Settings = _provider.GetSettings();
            }
            else
            {
                throw new Exception("Не получилось загрузить");
            }
        }

        public ISetting GetSetting(String name)
        {
            foreach (ISetting setting in Settings)
            {
                if (setting.GetName().Equals(name))
                {
                    return setting;
                }
            }
            return new NullSetting();
        }

        public void AddSetting(ISetting setting)
        {
            if (!(GetSetting(setting.GetName()) is NullSetting))
            {
                throw new AddSettingException();
            }
            Settings.Add(setting);
            _provider.WriteSetting(setting);
        }

        public void SetSetting(String name, Object value)
        {
            ISetting setting = GetSetting(name);
            if ((setting is NullSetting))
            {
                setting = new DefaultSetting(name, value);
                AddSetting(setting);
            }
            else
            {
                setting.SetValue(value);
                _provider.EraseSetting(setting);
                _provider.WriteSetting(setting);
            }
        }

        public Int32 GetSettingsCount()
        {
            return Settings.Count;
        }

        private class DefaultSetting:Setting
        {
            public DefaultSetting(string name, object value) : base(name, value)
            {
            }
        }

        private class NullSetting:ISetting
        {
            public string GetName()
            {
                return null;
            }

            public object GetValue()
            {
                return null;
            }

            public void SetValue(object value)
            {
                
            }
        }
    }
}
