using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfMenuAltDal : EfEntityRepositoryBase<MenuAlt, EczaneNobetContext>, IMenuAltDal
    {
        public MenuAltDetay GetDetay(Expression<Func<MenuAltDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.MenuAltlar
                        .Select(s => new MenuAltDetay
                        {
                            Id = s.Id,
                            LinkText = s.LinkText,
                            MenuId = s.MenuId,
                            PasifMi = s.PasifMi,
                            MenuLinkText = s.Menu.LinkText,
                            MenuPasifMi = s.Menu.PasifMi,
                            ActionName = s.ActionName,
                            SpanCssClass = s.SpanCssClass,
                            AreaName = s.AreaName,
                            ControllerName = s.ControllerName
                        }).SingleOrDefault(filter);
            }
        }

        public List<MenuAltDetay> GetDetayList(Expression<Func<MenuAltDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.MenuAltlar
                        .Select(s => new MenuAltDetay
                        {
                            Id = s.Id,
                            LinkText = s.LinkText,
                            MenuId = s.MenuId,
                            PasifMi = s.PasifMi,
                            MenuLinkText = s.Menu.LinkText,
                            MenuPasifMi = s.Menu.PasifMi,
                            ActionName = s.ActionName,
                            SpanCssClass = s.SpanCssClass,
                            AreaName = s.AreaName,
                            ControllerName = s.ControllerName
                        });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}

