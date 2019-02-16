﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetSonucDal : EfEntityRepositoryBase<EczaneNobetSonuc, EczaneNobetContext>, IEczaneNobetSonucDal
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
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetAltGrupId = s.EczaneNobetGrup.EczaneNobetGrupAltGrup != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGrup.NobetAltGrupId
                        : 0,
                        NobetAltGrupAdi = s.EczaneNobetGrup.EczaneNobetGrupAltGrup != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGrup.NobetAltGrup.Adi
                        : "Aalt grup yok"
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetSonucDetay2> GetDetayList(Expression<Func<EczaneNobetSonucDetay2, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetSonuclar
                    .Where(w => w.EczaneNobetFeragat.NobetFeragatTipId == 3)
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
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetAltGrupId = s.EczaneNobetGrup.EczaneNobetGrupAltGrup != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGrup.NobetAltGrupId
                        : 0,
                        NobetAltGrupAdi = s.EczaneNobetGrup.EczaneNobetGrupAltGrup != null
                        ? s.EczaneNobetGrup.EczaneNobetGrupAltGrup.NobetAltGrup.Adi
                        : "Aalt grup yok"
                    });

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.EczaneNobetSonuclar.RemoveRange(context.EczaneNobetSonuclar.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }

        public virtual void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            var liste = new List<EczaneNobetSonuc>();

            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetCozum in eczaneNobetCozumler)
                {
                    var nobetSonuc = new EczaneNobetSonuc
                    {
                        TakvimId = nobetCozum.TakvimId,
                        EczaneNobetGrupId = nobetCozum.EczaneNobetGrupId,
                        NobetGorevTipId = nobetCozum.NobetGorevTipId
                    };

                    liste.Add(nobetSonuc);
                }

                //foreach (var nobetCozum in eczaneNobetCozumler)
                //{
                //    var nobetSonuc = new EczaneNobetSonuc
                //    {
                //        TakvimId = nobetCozum.TakvimId,
                //        EczaneNobetGrupId = nobetCozum.EczaneNobetGrupId,
                //        NobetGorevTipId = nobetCozum.NobetGorevTipId
                //    };

                //    context.EczaneNobetSonuclar.Add(nobetSonuc);
                //}                

                context.EczaneNobetSonuclar.AddRange(liste);
                context.SaveChanges();
            }
        }
    }
}
