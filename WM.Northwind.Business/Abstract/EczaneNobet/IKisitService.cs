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
    public interface IKisitService
    {
        Kisit GetById(int kisitId);
        List<Kisit> GetList();
        //List<Kisit> GetByCategory(int categoryId);
        void Insert(Kisit kisit);
        void Update(Kisit kisit);
        void Delete(int kisitId);

        KisitDetay GetDetayById(int KisitId);
        List<KisitDetay> GetDetaylar();
        List<KisitDetay> GetDetaylar(int kisitKategoriId);
    }
} 