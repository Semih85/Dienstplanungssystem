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
    public interface IGunDegerService
    {
        GunDeger GetById(int gunDegerId);
        List<GunDeger> GetList();
        //List<GunDeger> GetByCategory(int categoryId);
        void Insert(GunDeger gunDeger);
        void Update(GunDeger gunDeger);
        void Delete(int gunDegerId);

        //GunDegerDetay GetDetayById(int gunDegerId);
        //List<GunDegerDetay> GetDetaylar();
    }
} 