using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class UserEczane : IEntity
    {
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int UserId { get; set; }

        public virtual Eczane Eczane { get; set; }
        public virtual User User { get; set; }
    }
}
