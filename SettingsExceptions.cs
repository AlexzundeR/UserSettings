using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSettings.Exceptions
{
    public class AddSettingException:Exception
    {
        public AddSettingException():base("Невозможно добавить настройку! Настройка с таким именем уже есть в списке!")
        {
        }
    }

    public class SettingNotFoundException:Exception
    {
        public SettingNotFoundException()
            : base("Настройки с таким именем не существует!")
        {
        }
    }
}
