using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSettings
{
    public interface ISettingsProvider
    {
        Boolean Load();
        ArrayList GetSettings();
        void WriteSetting(ISetting setting);

        void EraseSetting(ISetting setting);
    }
}
