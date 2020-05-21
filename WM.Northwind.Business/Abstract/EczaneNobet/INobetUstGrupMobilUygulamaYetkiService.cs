using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface INobetUstGrupMobilUygulamaYetkiService
    {
        NobetUstGrupMobilUygulamaYetki GetById(int nobetUstGrupMobilUygulamaYetkiId);
        List<NobetUstGrupMobilUygulamaYetki> GetList();
        //List<NobetUstGrupMobilUygulamaYetki> GetByCategory(int categoryId);
        void Insert(NobetUstGrupMobilUygulamaYetki nobetUstGrupMobilUygulamaYetki);
        void Update(NobetUstGrupMobilUygulamaYetki nobetUstGrupMobilUygulamaYetki);
        void Delete(int nobetUstGrupMobilUygulamaYetkiId);
        NobetUstGrupMobilUygulamaYetkiDetay GetDetayById(int nobetUstGrupMobilUygulamaYetkiId);
        List<NobetUstGrupMobilUygulamaYetkiDetay> GetDetaylar();
    }
}