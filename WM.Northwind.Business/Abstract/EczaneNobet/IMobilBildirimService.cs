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
    public interface IMobilBildirimService
    {
        MobilBildirim GetById(int mobilBildirimId);
        List<MobilBildirim> GetList();
        //List<MobilBildirim> GetByCategory(int categoryId);
        void Insert(MobilBildirim mobilBildirim);
        void Update(MobilBildirim mobilBildirim);
        void Delete(int mobilBildirimId);
         MobilBildirimDetay GetDetayById(int mobilBildirimId);
         List <MobilBildirimDetay> GetDetaylar();
         List <MobilBildirimDetay> GetDetaylar(int nobetUstGrupId);
         List <MobilBildirimDetay> GetDetaylarByNobetUstGrupGonderimTarihi(int nobetUstGrupId, DateTime gonderimTarihi);
    }
} 