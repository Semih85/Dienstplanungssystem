using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Core.Utilities.Mvc.Session
{
    public interface ISessionServiceBase<T> where T : class, new()
    {
        T GetSession(string sessionAdi);
        //T GetSession();
        void SetSession(T nobetUstGrupDetay, string sessionAdi);
    }
}
