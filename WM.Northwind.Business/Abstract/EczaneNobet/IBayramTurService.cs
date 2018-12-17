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
    public interface IBayramTurService
    {
        BayramTur GetById(int bayramTurId);
        List<BayramTur> GetList();
        //List<BayramTur> GetByCategory(int categoryId);
        void Insert(BayramTur bayramTur);
        void Update(BayramTur bayramTur);
        void Delete(int bayramTurId);
                        
    }
} 