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
    public interface INobetGrupService
    {
        NobetGrup GetById(int nobetGrupId);
        List<NobetGrup> GetList();
        List<NobetGrup> GetListByNobetUstGrupId(int nobetUstGrupId);
        List<NobetGrup> GetList(List<int> nobetGrupIdList);
        void Insert(NobetGrup nobetGrup);
        void Update(NobetGrup nobetGrup);
        void Delete(int nobetGrupId);

        NobetGrupDetay GetDetayById(int nobetGrupId);
        List<NobetGrupDetay> GetDetaylar();
        List<NobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<NobetGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList);
        List<NobetGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGrup> GetListByUser(User user);
    }
} 