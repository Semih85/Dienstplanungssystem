using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IIlceService
    {
        Ilce GetById(int ilceId);
        List<Ilce> GetList();
        //List<Ilce> GetByRoleId(int roleId);
        void Insert(Ilce ilce);
        void Update(Ilce ilce);
        void Delete(int ilceId);

        IlceDetay GetDetayById(int ilceId);
        List<IlceDetay> GetListDetay();
    }
}
