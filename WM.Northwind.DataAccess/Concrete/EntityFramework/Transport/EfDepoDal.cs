using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Transport
{
    public class EfDepoDal : EfEntityRepositoryBase<Depo, TransportContext>, IDepoDal
    {
    }
}
