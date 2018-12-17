using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.DataAccess.Abstract.Transport
{
    public interface IDepoDal : IEntityRepository<Depo>
    {
        //Custom Operations
    }
}
