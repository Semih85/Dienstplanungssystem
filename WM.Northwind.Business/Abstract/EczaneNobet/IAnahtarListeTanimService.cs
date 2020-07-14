using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IAnahtarListeTanimService
    {
        AnahtarListeTanim GetById(int anahtarListeTanimId);
        List<AnahtarListeTanim> GetList();
        //List<AnahtarListeTanim> GetByCategory(int categoryId);
        void Insert(AnahtarListeTanim anahtarListeTanim);
        void Update(AnahtarListeTanim anahtarListeTanim);
        void Delete(int anahtarListeTanimId);
                        
    }
} 