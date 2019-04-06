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
    public interface INobetOzelGunKategoriService
    {
        NobetOzelGunKategori GetById(int nobetOzelGunKategoriId);
        List<NobetOzelGunKategori> GetList();
        //List<NobetOzelGunKategori> GetByCategory(int categoryId);
        void Insert(NobetOzelGunKategori nobetOzelGunKategori);
        void Update(NobetOzelGunKategori nobetOzelGunKategori);
        void Delete(int nobetOzelGunKategoriId);
                        
    }
}