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
    public interface IRaporService
    {
        Rapor GetById(int eczaneNobetRaporId);
        List<Rapor> GetList();
        //List<EczaneNobetRapor> GetByCategory(int categoryId);
        void Insert(Rapor eczaneNobetRapor);
        void Update(Rapor eczaneNobetRapor);
        void Delete(int eczaneNobetRaporId);
                        RaporDetay GetDetayById(int eczaneNobetRaporId);
        List <RaporDetay> GetDetaylar();
    }
}