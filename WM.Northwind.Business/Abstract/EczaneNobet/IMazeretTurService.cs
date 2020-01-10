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
    public interface IMazeretTurService
    {
        MazeretTur GetById(int mazeretTurId);
        List<MazeretTur> GetList();
        //List<MazeretTur> GetByCategory(int categoryId);
        void Insert(MazeretTur mazeretTur);
        void Update(MazeretTur mazeretTur);
        void Delete(int mazeretTurId);
    }
}
