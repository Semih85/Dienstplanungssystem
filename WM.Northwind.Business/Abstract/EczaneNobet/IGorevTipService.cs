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
    public interface IGorevTipService
    {
        GorevTip GetById(int gorevTipId);
        List<GorevTip> GetList();
        //List<GorevTip> GetByCategory(int categoryId);
        void Insert(GorevTip gorevTip);
        void Update(GorevTip gorevTip);
        void Delete(int gorevTipId);
                        
    }
} 