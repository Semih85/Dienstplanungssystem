using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneService
    {
        Eczane GetById(int eczaneId);
        List<Eczane> GetList();
        List<Eczane> GetList(int nobetUstGrupId);
        List<Eczane> GetList(int[] nobetUstGrupIds);
        List<Eczane> GetListByUser(User user);
        List<Eczane> GetListByEczaneIds(int[] eczaneIds);

        //List<Eczane> GetByNobetGrup(int nobetId);
        void Insert(Eczane eczane);
        void Update(Eczane eczane);
        void Delete(int eczaneId);

        Eczane GetEczane(string eczaneAdi, DateTime acilisTarihi, int nobetUstGrupId);
        EczaneDetay GetDetayById(int eczaneId);
        EczaneDetay GetDetay(string eczaneAdi, DateTime acilisTarihi, int nobetUstGrupId);
        List<EczaneDetay> GetDetaylar();
        List<EczaneDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneDetay> GetDetaylar(List<int> nobetUstGrupIdList);

        NobetUstGrup GetByEczaneNobetGrupId(int eczaneNobetGrupId);

    }
}
