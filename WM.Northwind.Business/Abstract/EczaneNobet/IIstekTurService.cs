using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IIstekTurService
    {
        IstekTur GetById(int istekTurId);
        List<IstekTur> GetList();
        //List<IstekTur> GetByRoleId(int roleId);
        void Insert(IstekTur istekTur);
        void Update(IstekTur istekTur);
        void Delete(int istekTurId);
    }
}
