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
    public interface IUserEczaneOdaService
    {
        UserEczaneOda GetById(int userEczaneOdaId);
        List<UserEczaneOda> GetList();
        List<UserEczaneOda> GetListByUserId(int userId);
        void Insert(UserEczaneOda userEczaneOda);
        void Update(UserEczaneOda userEczaneOda);
        void Delete(int userEczaneOdaId);

        UserEczaneOdaDetay GetDetayById(int userEczaneOdaId);
        List<UserEczaneOdaDetay> GetDetaylar();
    }
}
