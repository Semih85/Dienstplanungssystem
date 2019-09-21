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
    public interface IUserEczaneService
    {
        UserEczane GetById(int eczaneId);
        List<UserEczane> GetList();
        //List<UserEczane> GetByNobetGrup(int nobetId);
        void Insert(UserEczane eczane);
        void Update(UserEczane eczane);
        void Delete(int userEczaneId);

        List<UserEczane> GetListByUserId(int userId);
        UserEczaneDetay GetDetayById(int userEczaneId);
        List<UserEczaneDetay> GetDetaylar();
        List<UserEczaneDetay> GetDetaylar(List<int> nobetUstGrupIdList);
        List<UserEczaneDetay> GetDetaylarByUserId(int userId);
        List<UserEczaneDetay> GetDetaylar(int nobetUstGrupId);
    }
}
