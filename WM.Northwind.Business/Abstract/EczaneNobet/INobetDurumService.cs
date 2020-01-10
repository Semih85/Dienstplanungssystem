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
    public interface INobetDurumService
    {
        NobetDurum GetById(int nobetDurumId);
        List<NobetDurum> GetList();
        //List<NobetDurum> GetByCategory(int categoryId);
        void Insert(NobetDurum nobetDurum);
        void Update(NobetDurum nobetDurum);
        void Delete(int nobetDurumId);
        NobetDurumDetay GetDetayById(int nobetDurumId);
        List<NobetDurumDetay> GetDetaylar();
        List<NobetDurumDetay> GetDetaylar(List<int> nobetUstGruplar);
        List<NobetDurumDetay> GetDetaylar(int nobetUstGrupId);
    }
}