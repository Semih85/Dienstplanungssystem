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
    public interface IEczaneGorevTipService
    {
        EczaneGorevTip GetById(int eczaneGorevTipId);
        List<EczaneGorevTip> GetList();
        //List<EczaneGorevTip> GetByCategory(int categoryId);
        void Insert(EczaneGorevTip eczaneGorevTip);
        void Update(EczaneGorevTip eczaneGorevTip);
        void Delete(int eczaneGorevTipId);

        EczaneGorevTipDetay GetDetayById(int eczaneGorevTipId);
        List<EczaneGorevTipDetay> GetDetaylar();
    }
} 