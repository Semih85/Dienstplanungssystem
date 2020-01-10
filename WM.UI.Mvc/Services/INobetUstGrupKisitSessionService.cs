using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Utilities.Mvc.Session;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Services
{
    public interface INobetUstGrupKisitSessionService : ISessionServiceBase<NobetUstGrupKisitDetay>
    {
        List<NobetUstGrupKisitDetayDegisimTakip> GetSessionList(string sessionAdi, int nobetUstGrupId);
        void AddSessionList(
            NobetUstGrupKisitDetay nobetUstGrupDetayOnce,
            NobetUstGrupKisitDetay nobetUstGrupDetaySonra,
            string sessionAdi,
            List<NobetUstGrupKisitDetayDegisimTakip> nobetUstGrupKisitlar);
    }
}
