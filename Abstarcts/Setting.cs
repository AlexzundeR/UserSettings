using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSettings
{
    public class Setting:ISetting
    {
        private string _name;
        private Object _value;

        public Setting(String name)
        {
            _name = name;
        }

        public Setting(String name, Object value)
        {
            _name = name;
            _value = value;
        }

        public virtual string GetName()
        {
            return _name;
        }

        public virtual Object GetValue()
        {
            return _value;
        }

        public virtual void SetValue(object value)
        {
            _value = value;
        }
    }
}
