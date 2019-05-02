using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Services
{
    public interface INobetUstGrupSessionService
    {
        NobetUstGrupDetay GetNobetUstGrup();
        void SetNobetUstGrup(NobetUstGrupDetay nobetUstGrupDetay);
    }
}
