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
    public interface IMobilUygulamaYetkiService
    {
        MobilUygulamaYetki GetById(int mobilUygulamaYetkiId);
        List<MobilUygulamaYetki> GetList();
        //List<MobilUygulamaYetki> GetByCategory(int categoryId);
        void Insert(MobilUygulamaYetki mobilUygulamaYetki);
        void Update(MobilUygulamaYetki mobilUygulamaYetki);
        void Delete(int mobilUygulamaYetkiId);
                        
    }
} 