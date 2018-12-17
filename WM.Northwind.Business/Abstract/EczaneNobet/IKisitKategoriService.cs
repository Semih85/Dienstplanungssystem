using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IKisitKategoriService
    {
        KisitKategori GetById(int kisitKategoriId);
        List<KisitKategori> GetList();
        //List<KisitKategori> GetByCategory(int categoryId);
        void Insert(KisitKategori kisitKategori);
        void Update(KisitKategori kisitKategori);
        void Delete(int kisitKategoriId);                        
    }
}