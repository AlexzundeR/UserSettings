using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSettings
{
    public interface ISetting
    {
        String GetName();
        Object GetValue();
        void SetValue(Object value);
    }
}
