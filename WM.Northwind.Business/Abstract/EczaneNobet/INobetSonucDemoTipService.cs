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
    public interface INobetSonucDemoTipService
    {
        NobetSonucDemoTip GetById(int nobetSonucDemoTipId);
        List<NobetSonucDemoTip> GetList();
        //List<NobetSonucDemoTip> GetByCategory(int categoryId);
        void Insert(NobetSonucDemoTip nobetSonucDemoTip);
        void Update(NobetSonucDemoTip nobetSonucDemoTip);
        void Delete(int nobetSonucDemoTipId);
                        
    }
} 