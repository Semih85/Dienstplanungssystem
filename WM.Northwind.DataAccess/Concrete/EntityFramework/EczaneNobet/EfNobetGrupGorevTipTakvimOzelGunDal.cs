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
    public class EfNobetGrupGorevTipTakvimOzelGunDal : EfEntityRepositoryBase<NobetGrupGorevTipTakvimOzelGun, EczaneNobetContext>, INobetGrupGorevTipTakvimOzelGunDal
    {
        public void CokluEkle(List<NobetGrupGorevTipTakvimOzelGun> nobetGrupGorevTipTakvimOzelGunler)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetGrupGorevTipTakvimOzelGun in nobetGrupGorevTipTakvimOzelGunler)
                {
                    context.NobetGrupGorevTipTakvimOzelGunler.Add(nobetGrupGorevTipTakvimOzelGun);
                }
                context.SaveChanges();
            }
        }

        public void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.NobetGrupGorevTipTakvimOzelGunler.RemoveRange(context.NobetGrupGorevTipTakvimOzelGunler.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }

        public NobetGrupGorevTipTakvimOzelGunDetay GetDetay(Expression<Func<NobetGrupGorevTipTakvimOzelGunDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return (from s in ctx.NobetGrupGorevTipTakvimOzelGunler
                        let nobetGrupGorevTipGunKural = ctx.NobetGrupGorevTipGunKurallar
                                 .FirstOrDefault(x => x.NobetGrupGorevTipId == s.NobetGrupGorevTipGunKural.NobetGrupGorevTipId && x.NobetGunKuralId == s.NobetGunKuralId).NobetUstGrupGunGrup
                        select (new NobetGrupGorevTipTakvimOzelGunDetay
                        {
                            TakvimId = s.TakvimId,
                            NobetGrupGorevTipGunKuralAdi = s.NobetGrupGorevTipGunKural.NobetGunKural.Adi,
                            NobetGunKuralIdGrup = s.NobetGrupGorevTipGunKural.NobetGunKuralId,
                            NobetGunKuralAdiGrup = s.NobetGrupGorevTipGunKural.NobetGunKural.Adi,
                            NobetGunKuralIdFarkli = s.NobetGunKuralId,
                            NobetGunKuralAdiFarkli = s.NobetGunKural.Adi,
                            NobetGrupGorevTipGunKuralId = s.NobetGrupGorevTipGunKuralId,
                            NobetOzelGunAdi = s.NobetOzelGun.Adi,
                            NobetOzelGunId = s.NobetOzelGunId,
                            Tarih = s.Takvim.Tarih,
                            NobetUstGrupGunGrupId = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrupId,
                            //GunGrupAdi = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Adi,
                            //GunGrupId = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Id,
                            GunGrupAdi = s.FarkliGunGosterilsinMi == true
                                     ? nobetGrupGorevTipGunKural.GunGrup.Adi
                                     : s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Adi,
                            GunGrupId = s.FarkliGunGosterilsinMi == true
                                     ? nobetGrupGorevTipGunKural.GunGrupId
                                     : s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrupId,
                            NobetUstGrupId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                            NobetGrupGorevTipId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTipId,
                            NobetGorevTipId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGorevTipId,
                            NobetGorevTipAdi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGorevTip.Adi,
                            NobetGrupId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrupId,
                            NobetGrupAdi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrup.Adi,
                            Id = s.Id,
                            FarkliGunGosterilsinMi = s.FarkliGunGosterilsinMi,
                            AgirlikDegeri = s.AgirlikDegeri,
                            NobetGrupGorevTipBaslamaTarihi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.BaslamaTarihi,
                            NobetOzelGunKategoriId = s.NobetOzelGunKategoriId,
                            NobetOzelGunKategoriAdi = s.NobetOzelGunKategori.Adi
                        })).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupGorevTipTakvimOzelGunDetay> GetDetayList(Expression<Func<NobetGrupGorevTipTakvimOzelGunDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = from s in ctx.NobetGrupGorevTipTakvimOzelGunler
                            let nobetGrupGorevTipGunKural = ctx.NobetGrupGorevTipGunKurallar
                                .FirstOrDefault(x => x.NobetGrupGorevTipId == s.NobetGrupGorevTipGunKural.NobetGrupGorevTipId && x.NobetGunKuralId == s.NobetGunKuralId).NobetUstGrupGunGrup
                            select (new NobetGrupGorevTipTakvimOzelGunDetay
                            {
                                TakvimId = s.TakvimId,
                                NobetGrupGorevTipGunKuralAdi = s.NobetGrupGorevTipGunKural.NobetGunKural.Adi,
                                NobetGunKuralIdGrup = s.NobetGrupGorevTipGunKural.NobetGunKuralId,
                                NobetGunKuralAdiGrup = s.NobetGrupGorevTipGunKural.NobetGunKural.Adi,
                                NobetGunKuralIdFarkli = s.NobetGunKuralId,
                                NobetGunKuralAdiFarkli = s.NobetGunKural.Adi,
                                NobetGrupGorevTipGunKuralId = s.NobetGrupGorevTipGunKuralId,
                                NobetOzelGunAdi = s.NobetOzelGun.Adi,
                                NobetOzelGunId = s.NobetOzelGunId,
                                Tarih = s.Takvim.Tarih,
                                NobetUstGrupGunGrupId = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrupId,
                                //GunGrupAdi = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Adi,
                                //GunGrupId = s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Id,
                                GunGrupAdi = s.FarkliGunGosterilsinMi == true
                                    ? nobetGrupGorevTipGunKural.GunGrup.Adi
                                    : s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrup.Adi,
                                GunGrupId = s.FarkliGunGosterilsinMi == true
                                    ? nobetGrupGorevTipGunKural.GunGrupId
                                    : s.NobetGrupGorevTipGunKural.NobetUstGrupGunGrup.GunGrupId,
                                NobetUstGrupId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                                NobetGrupGorevTipId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTipId,
                                NobetGorevTipId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGorevTipId,
                                NobetGorevTipAdi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGorevTip.Adi,
                                NobetGrupId = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrupId,
                                NobetGrupAdi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.NobetGrup.Adi,
                                Id = s.Id,
                                FarkliGunGosterilsinMi = s.FarkliGunGosterilsinMi,
                                AgirlikDegeri = s.AgirlikDegeri,
                                NobetGrupGorevTipBaslamaTarihi = s.NobetGrupGorevTipGunKural.NobetGrupGorevTip.BaslamaTarihi,
                                NobetOzelGunKategoriId = s.NobetOzelGunKategoriId,
                                NobetOzelGunKategoriAdi = s.NobetOzelGunKategori.Adi
                            });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}
