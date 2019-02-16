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
    public interface INobetFeragatTipService
    {
        NobetFeragatTip GetById(int nobetFeragatTipId);
        List<NobetFeragatTip> GetList();
        //List<NobetFeragatTip> GetByCategory(int categoryId);
        void Insert(NobetFeragatTip nobetFeragatTip);
        void Update(NobetFeragatTip nobetFeragatTip);
        void Delete(int nobetFeragatTipId);
                        
    }
}