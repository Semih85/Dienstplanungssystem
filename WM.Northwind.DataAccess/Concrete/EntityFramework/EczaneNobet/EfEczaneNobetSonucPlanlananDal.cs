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
    public class EfEczaneNobetSonucPlanlananDal : EfEntityRepositoryBase<EczaneNobetSonucPlanlanan, EczaneNobetContext>, IEczaneNobetSonucPlanlananDal
    {
        public EczaneNobetSonucDetay2 GetDetay(Expression<Func<EczaneNobetSonucDetay2, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSonuclar
                    .Select(s => new EczaneNobetSonucDetay2
                    {
                        Id = s.Id,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                        EczaneNobetGrupBitisTarihi = s.EczaneNobetGrup.BitisTarihi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        TakvimId = s.TakvimId,
                        Tarih = s.Takvim.Tarih,
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.BaslamaTarihi,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        //NobetAltGrupId = s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).SingleOrDefault() != null
                        //? s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).SingleOrDefault().NobetAltGrupId
                        //: 0,
                        //NobetAltGrupAdi = s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).SingleOrDefault() != null
                        //? s.EczaneNobetGrup.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).SingleOrDefault().NobetAltGrup.Adi
                        //: "Aalt grup yok"
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetSonucDetay2> GetDetayList(Expression<Func<EczaneNobetSonucDetay2, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = from s in ctx.EczaneNobetSonucPlanlananlar
                            //let eczaneNobetGrupAltGrup = s.EczaneNobetGrup.EczaneNobetGrupAltGruplar
                            //.Where(w => w.BaslangicTarihi <= s.Takvim.Tarih && (s.Takvim.Tarih <= w.BitisTarihi || w.BitisTarihi == null)).FirstOrDefault()
                            select new EczaneNobetSonucDetay2
                            {
                                Id = s.Id,
                                EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                                EczaneId = s.EczaneNobetGrup.EczaneId,
                                EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                                EczaneNobetGrupBitisTarihi = s.EczaneNobetGrup.BitisTarihi,
                                EczaneNobetGrupId = s.EczaneNobetGrupId,
                                NobetGorevTipAdi = s.NobetGorevTip.Adi,
                                NobetGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                                NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                                NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                                NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                                TakvimId = s.TakvimId,
                                Tarih = s.Takvim.Tarih,
                                NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                                NobetGrupGorevTipBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.BaslamaTarihi,
                                NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                                //NobetAltGrupId = eczaneNobetGrupAltGrup != null ? eczaneNobetGrupAltGrup.NobetAltGrupId : 0,
                                //NobetAltGrupAdi = eczaneNobetGrupAltGrup != null ? eczaneNobetGrupAltGrup.NobetAltGrup.Adi : "Aalt grup yok",
                            };

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.EczaneNobetSonucPlanlananlar.RemoveRange(context.EczaneNobetSonucPlanlananlar.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }

        public virtual void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetCozum in eczaneNobetCozumler)
                {
                    var nobetSonuc = new EczaneNobetSonucPlanlanan
                    {
                        TakvimId = nobetCozum.TakvimId,
                        EczaneNobetGrupId = nobetCozum.EczaneNobetGrupId,
                        NobetGorevTipId = nobetCozum.NobetGorevTipId
                    };

                    context.EczaneNobetSonucPlanlananlar.Add(nobetSonuc);
                }
                context.SaveChanges();
            }
        }
    }
}