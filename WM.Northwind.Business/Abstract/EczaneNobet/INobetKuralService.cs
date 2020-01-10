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
    public interface INobetKuralService
    {
        NobetKural GetById(int nobetKuralId);
        List<NobetKural> GetList();
        //List<NobetKural> GetByCategory(int categoryId);
        void Insert(NobetKural nobetKural);
        void Update(NobetKural nobetKural);
        void Delete(int nobetKuralId);
                        
    }
} 