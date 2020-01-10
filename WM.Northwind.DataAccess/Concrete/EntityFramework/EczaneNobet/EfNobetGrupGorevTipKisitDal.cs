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
    public class EfNobetGrupGorevTipKisitDal : EfEntityRepositoryBase<NobetGrupGorevTipKisit, EczaneNobetContext>, INobetGrupGorevTipKisitDal
    {
        public NobetGrupGorevTipKisitDetay GetDetay(Expression<Func<NobetGrupGorevTipKisitDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupGorevTipKisitlar
                    .Select(s => new NobetGrupGorevTipKisitDetay
                    {
                        Id = s.Id,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId,
                        KisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        NobetUstGrupId = s.NobetUstGrupKisit.NobetUstGrupId,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        NobetUstGrupAdi = s.NobetUstGrupKisit.NobetUstGrup.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        KisitAciklama = s.NobetUstGrupKisit.Kisit.Aciklama,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        KisitAdiGosterilen = s.NobetUstGrupKisit.Kisit.AdiGosterilen,
                        KisitKategoriAdi = s.NobetUstGrupKisit.Kisit.KisitKategori.Adi,
                        KisitKategoriId = s.NobetUstGrupKisit.Kisit.KisitKategoriId,
                        Aciklama = s.Aciklama,
                        PasifMiUstGrup = s.NobetUstGrupKisit.PasifMi,
                        SagTarafDegeriUstGrup = s.NobetUstGrupKisit.SagTarafDegeri,
                        SagTarafDegeriVarsayilanUstGrup = s.NobetUstGrupKisit.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMiUstGrup = s.NobetUstGrupKisit.VarsayilanPasifMi,
                        AciklamaUstGrup = s.NobetUstGrupKisit.Aciklama,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        KisitKategorisi = s.NobetUstGrupKisit.Kisit.KisitKategori.Adi,
                        DegerPasifMi = s.NobetUstGrupKisit.Kisit.DegerPasifMi
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupGorevTipKisitDetay> GetDetayList(Expression<Func<NobetGrupGorevTipKisitDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupGorevTipKisitlar
                    .Select(s => new NobetGrupGorevTipKisitDetay
                    {
                        Id = s.Id,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId,
                        KisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        NobetUstGrupId = s.NobetUstGrupKisit.NobetUstGrupId,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        NobetUstGrupAdi = s.NobetUstGrupKisit.NobetUstGrup.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        KisitAciklama = s.NobetUstGrupKisit.Kisit.Aciklama,
                        VarsayilanPasifMi = s.VarsayilanPasifMi,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        KisitAdiGosterilen = s.NobetUstGrupKisit.Kisit.AdiGosterilen,
                        KisitKategoriAdi = s.NobetUstGrupKisit.Kisit.KisitKategori.Adi,
                        KisitKategoriId = s.NobetUstGrupKisit.Kisit.KisitKategoriId,
                        Aciklama = s.Aciklama,
                        PasifMiUstGrup = s.NobetUstGrupKisit.PasifMi,
                        SagTarafDegeriUstGrup = s.NobetUstGrupKisit.SagTarafDegeri,
                        SagTarafDegeriVarsayilanUstGrup = s.NobetUstGrupKisit.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMiUstGrup = s.NobetUstGrupKisit.VarsayilanPasifMi,
                        AciklamaUstGrup = s.NobetUstGrupKisit.Aciklama,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        KisitKategorisi = s.NobetUstGrupKisit.Kisit.KisitKategori.Adi,
                        DegerPasifMi = s.NobetUstGrupKisit.Kisit.DegerPasifMi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}