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
    public interface INobetUstGrupKisitIstisnaGunGrupService
    {
        NobetUstGrupKisitIstisnaGunGrup GetById(int nobetUstGrupKisitIstisnaGunGrupId);
        List<NobetUstGrupKisitIstisnaGunGrup> GetList();
        //List<NobetUstGrupKisitIstisnaGunGrup> GetByCategory(int categoryId);
        void Insert(NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup);
        void Update(NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup);
        void Delete(int nobetUstGrupKisitIstisnaGunGrupId);
                        NobetUstGrupKisitIstisnaGunGrupDetay GetDetayById(int nobetUstGrupKisitIstisnaGunGrupId);
                                   List <NobetUstGrupKisitIstisnaGunGrupDetay> GetDetaylar();
    }
} 