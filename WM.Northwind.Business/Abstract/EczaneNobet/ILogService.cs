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
    public interface ILogService
    {
        Log GetById(int logId);
        List<Log> GetList();
        List<Log> GetList(params string[] icerik);
        //List<Log> GetByCategory(int categoryId);
        void Insert(Log log);
        void Update(Log log);
        void Delete(int logId);
                        
    }
}