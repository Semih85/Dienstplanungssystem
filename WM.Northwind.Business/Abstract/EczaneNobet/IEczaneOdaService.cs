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
    public interface IEczaneOdaService
    {
        EczaneOda GetById(int eczaneOdaId);
        List<EczaneOda> GetList();
        List<EczaneOda> GetListByUser(User user);
        void Insert(EczaneOda eczaneOda);
        void Update(EczaneOda eczaneOda);
        void Delete(int eczaneOdaId);
    }
}
