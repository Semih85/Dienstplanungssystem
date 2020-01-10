using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface ISehirService
    {
        Sehir GetById(int sehirId);
        List<Sehir> GetList();
        //List<Sehir> GetByRoleId(int roleId);
        void Insert(Sehir sehir);
        void Update(Sehir sehir);
        void Delete(int sehirId);

        SehirDetay GetDetayById(int sehirId);
        List<SehirDetay> GetDetaylar();
    }
}
