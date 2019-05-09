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
    public interface IRaporKategoriService
    {
        RaporKategori GetById(int raporKategoriId);
        List<RaporKategori> GetList();
        //List<RaporKategori> GetByCategory(int categoryId);
        void Insert(RaporKategori raporKategori);
        void Update(RaporKategori raporKategori);
        void Delete(int raporKategoriId);
                        
    }
}