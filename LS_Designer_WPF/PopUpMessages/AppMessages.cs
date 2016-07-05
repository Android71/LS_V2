using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.PopUpMessages
{
    public class AppMessages
    {
        public static string LE_EditingMsg()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Редактирование невозможно.");
            sb.AppendLine("");
            sb.AppendLine("Для редактирования удалите связь между LightElement и ControlChannel");
            return sb.ToString();
        }  
    }
}
