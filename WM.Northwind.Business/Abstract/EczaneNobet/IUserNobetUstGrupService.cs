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
    public interface IUserNobetUstGrupService
    {
        UserNobetUstGrup GetById(int userUstGrupId);
        List<UserNobetUstGrup> GetList();
        List<UserNobetUstGrup> GetListByUserId(int userId);
        void Insert(UserNobetUstGrup userUstGrup);
        void Update(UserNobetUstGrup userUstGrup);
        void Delete(int userUstGrupId);

        UserNobetUstGrup GetByUserId(int userUstGrupId);
        UserNobetUstGrupDetay GetDetayById(int userNobetUstGrupId);
        List<UserNobetUstGrupDetay> GetDetaylar();
        List<UserNobetUstGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<UserNobetUstGrupDetay> GetDetaylar(List<int> nobetUstGrupIdList);
    }
}
