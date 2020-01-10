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
    public interface INobetGunKuralService
    {
        NobetGunKural GetById(int nobetGunKuralId);
        List<NobetGunKural> GetList();
        //List<NobetGunKural> GetAktifList(List<int> nobetGrupIdList);
        void Insert(NobetGunKural nobetGunKural);
        void Update(NobetGunKural nobetGunKural);
        void Delete(int nobetGunKuralId);
        
    }
} 