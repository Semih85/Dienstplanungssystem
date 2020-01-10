using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneIlceService
    {
        EczaneIlce GetById(int eczaneIlceId);
        List<EczaneIlce> GetList();
        //List<EczaneIlce> GetByRoleId(int roleId);
        void Insert(EczaneIlce eczaneIlce);
        void Update(EczaneIlce eczaneIlce);
        void Delete(int eczaneIlceId);
    }
}
