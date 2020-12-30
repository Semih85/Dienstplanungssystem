using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetGrupKisitDal : EfEntityRepositoryBase<EczaneNobetGrupKisit, EczaneNobetContext>, IEczaneNobetGrupKisitDal
    {
        public EczaneNobetGrupKisitDetay GetDetay(Expression<Func<EczaneNobetGrupKisitDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetGrupKisitlar
                    .Select(s => new EczaneNobetGrupKisitDetay
                    {
                        Aciklama = s.Aciklama,
                        EczaneNobetGrupAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        Id = s.Id,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        NobetUstGrupId = s.NobetUstGrupKisit.NobetUstGrupId,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId,
                         //KisitAdiUzun = s.NobetUstGrupKisit.Kisit.u
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetGrupKisitDetay> GetDetayList(Expression<Func<EczaneNobetGrupKisitDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetGrupKisitlar
                    .Select(s => new EczaneNobetGrupKisitDetay
                    {
                        Aciklama = s.Aciklama,
                        EczaneNobetGrupAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        Id = s.Id,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        NobetUstGrupId = s.NobetUstGrupKisit.NobetUstGrupId,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}