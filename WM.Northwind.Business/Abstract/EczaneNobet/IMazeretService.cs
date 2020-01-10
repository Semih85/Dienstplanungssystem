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
    public interface IMazeretService
    {
        Mazeret GetById(int mazeretId);
        List<Mazeret> GetList();
        //List<Mazeret> GetByCategory(int categoryId);
        void Insert(Mazeret mazeret);
        void Update(Mazeret mazeret);
        void Delete(int mazeretId);
    }
}
