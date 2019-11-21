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
    public class EfNobetUstGrupKisitDal : EfEntityRepositoryBase<NobetUstGrupKisit, EczaneNobetContext>, INobetUstGrupKisitDal
    {
        public void CokluEkle(List<NobetUstGrupKisit> nobetUstGrupKisitlar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetUstGrupKisit in nobetUstGrupKisitlar)
                {
                    context.NobetUstGrupKisitlar.Add(nobetUstGrupKisit);
                }
                context.SaveChanges();
            }
        }

        public NobetUstGrupKisitDetay GetDetay(Expression<Func<NobetUstGrupKisitDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetUstGrupKisitlar
                    .Select(s => new NobetUstGrupKisitDetay
                    {
                        Id = s.Id,
                        NobetUstGrupId = s.NobetUstGrupId,
                        KisitAdi = s.Kisit.Adi,
                        KisitId = s.KisitId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        KisitAciklama = s.Kisit.Aciklama,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        KisitAdiGosterilen = s.Kisit.AdiGosterilen,
                        KisitKategoriAdi = s.Kisit.KisitKategori.Adi,
                        KisitKategoriId = s.Kisit.KisitKategoriId,
                        NobetGrupGorevtipKisitSayisi = s.NobetGrupGorevTipKisitlar.Count,
                        DegerPasifMi = s.Kisit.DegerPasifMi
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetUstGrupKisitDetay> GetDetayList(Expression<Func<NobetUstGrupKisitDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetUstGrupKisitlar
                    .Select(s => new NobetUstGrupKisitDetay
                    {
                        Id = s.Id,
                        NobetUstGrupId = s.NobetUstGrupId,
                        KisitAdi = s.Kisit.Adi,
                        KisitId = s.KisitId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        KisitAciklama = s.Kisit.Aciklama,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        KisitAdiGosterilen = s.Kisit.AdiGosterilen,
                        KisitKategoriAdi = s.Kisit.KisitKategori.Adi,
                        KisitKategoriId = s.Kisit.KisitKategoriId,
                        NobetGrupGorevtipKisitSayisi = s.NobetGrupGorevTipKisitlar.Count,
                        DegerPasifMi = s.Kisit.DegerPasifMi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}