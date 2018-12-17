using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class UserEczaneOda : IEntity
    {
        public int Id { get; set; }
        public int EczaneOdaId { get; set; }
        public int UserId { get; set; }

        public virtual EczaneOda EczaneOda { get; set; }
        public virtual User User { get; set; }
    }
}
