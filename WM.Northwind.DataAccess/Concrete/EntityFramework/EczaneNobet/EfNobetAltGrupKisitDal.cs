﻿using System;
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
    public class EfNobetAltGrupKisitDal : EfEntityRepositoryBase<NobetAltGrupKisit, EczaneNobetContext>, INobetAltGrupKisitDal
    {
        public NobetAltGrupKisitDetay GetDetay(Expression<Func<NobetAltGrupKisitDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetAltGrupKisitlar
                    .Select(s => new NobetAltGrupKisitDetay
                    {
                        Aciklama = s.Aciklama,
                        NobetUstGrupKisitId = s.NobetUstGrupKisit.Kisit.Id,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        Id = s.Id,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMi = s.VarsayilanPasifMi

                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetAltGrupKisitDetay> GetDetayList(Expression<Func<NobetAltGrupKisitDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetAltGrupKisitlar
                    .Select(s => new NobetAltGrupKisitDetay
                    {
                        Aciklama = s.Aciklama,
                        NobetUstGrupKisitId = s.NobetUstGrupKisit.Kisit.Id,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        Id = s.Id,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        PasifMi = s.PasifMi,
                        SagTarafDegeri = s.SagTarafDegeri,
                        SagTarafDegeriVarsayilan = s.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMi = s.VarsayilanPasifMi

                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}