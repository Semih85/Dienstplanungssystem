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
using System.Data.Entity;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfAyniGunTutulanNobetDal : EfEntityRepositoryBase<AyniGunTutulanNobet, EczaneNobetContext>, IAyniGunTutulanNobetDal
    {
        public AyniGunTutulanNobetDetay GetDetay(Expression<Func<AyniGunTutulanNobetDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.AyniGunTutulanNobetler
                    .Select(s => new AyniGunTutulanNobetDetay
                    {
                        Id = s.Id,
                        EnSonAyniGunNobetTakvimId = s.EnSonAyniGunNobetTakvimId,

                        EczaneNobetGrupId1 = s.EczaneNobetGrupl.Id,
                        EczaneId1 = s.EczaneNobetGrupl.EczaneId,
                        EczaneAdi1 = s.EczaneNobetGrupl.Eczane.Adi,
                        NobetGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EczaneNobetGrupId2 = s.EczaneNobetGrup2.Id,
                        EczaneId2 = s.EczaneNobetGrup2.EczaneId,
                        EczaneAdi2 = s.EczaneNobetGrup2.Eczane.Adi,
                        NobetGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EnSonAyniGunNobetTarihi = s.Takvim.Tarih,
                        AyniGunNobetSayisi = s.AyniGunNobetSayisi,
                        AyniGunNobetTutamayacaklariGunSayisi = s.AyniGunNobetTutamayacaklariGunSayisi
                    }).SingleOrDefault(filter);
            }
        }
        public List<AyniGunTutulanNobetDetay> GetDetayList(Expression<Func<AyniGunTutulanNobetDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.AyniGunTutulanNobetler
                    .Select(s => new AyniGunTutulanNobetDetay
                    {
                        Id = s.Id,
                        EnSonAyniGunNobetTakvimId = s.EnSonAyniGunNobetTakvimId,

                        EczaneNobetGrupId1 = s.EczaneNobetGrupl.Id,
                        EczaneId1 = s.EczaneNobetGrupl.EczaneId,
                        EczaneAdi1 = s.EczaneNobetGrupl.Eczane.Adi,
                        NobetGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi1 = s.EczaneNobetGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EczaneNobetGrupId2 = s.EczaneNobetGrup2.Id,
                        EczaneId2 = s.EczaneNobetGrup2.EczaneId,
                        EczaneAdi2 = s.EczaneNobetGrup2.Eczane.Adi,
                        NobetGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi2 = s.EczaneNobetGrup2.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,

                        EnSonAyniGunNobetTarihi = s.Takvim.Tarih,

                        AyniGunNobetSayisi = s.AyniGunNobetSayisi,
                        AyniGunNobetTutamayacaklariGunSayisi = s.AyniGunNobetTutamayacaklariGunSayisi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}