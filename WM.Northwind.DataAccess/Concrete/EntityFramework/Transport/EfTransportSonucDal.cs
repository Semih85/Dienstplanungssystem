using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Transport
{
    public class EfTransportSonucDal : EfEntityRepositoryBase<TransportSonuc, TransportContext>, ITransportSonucDal
    {
        public static int Sayi { get; set; }

        public List<TransportSonucDetail> GetSonucDetails(int? id)
        {
            using (var context = new TransportContext())
            {

                var kategoriler = new List<TransportSonucDetail>();

                if (id == null)
                {
                    kategoriler = context.TransportSonuclar
                    .Select(x => new TransportSonucDetail
                    {
                        Id = x.Id,
                        DepoAdi = x.Depo.Adi,
                        FabrikaAdi = x.Fabrika.Adi,
                        Sonuc = x.Sonuc
                    }).ToList();
                }
                else
                {
                    kategoriler = context.TransportSonuclar
                        .Where(x => x.Id == id)
                        .Select(x => new TransportSonucDetail
                        {
                            Id = x.Id,
                            DepoAdi = x.Depo.Adi,
                            FabrikaAdi = x.Fabrika.Adi,
                            Sonuc = x.Sonuc
                        }).ToList();
                }


                return kategoriler;
            }

        }

        public TransportSonucDetail GetSonucDetailsById(int id)
        {

            using (var context = new TransportContext())
            {
                var kategoriler = new TransportSonucDetail();

                kategoriler = context.TransportSonuclar
                    .Where(x => x.Id == id)
                    .Select(x => new TransportSonucDetail
                    {
                        Id = x.Id,
                        DepoAdi = x.Depo.Adi,
                        FabrikaAdi = x.Fabrika.Adi,
                        Sonuc = x.Sonuc
                    }).SingleOrDefault();

                return kategoriler;
            }
        }

        public List<TransportSonucNodes> GetTransportSonucNodes()
        {
            using (var context = new TransportContext())
            {
                int levelA = 0;
                int levelB = 1;

                int groupA = 0;
                int groupB = 1;

                var fabrikalar = context.Fabrikalar
                                .Where(f => context.TransportSonuclar
                                                .Select(x => x.FabrikaId).Distinct().Contains(f.Id))
                                .Select(h => new TransportSonucNodes
                                {
                                    Id = "g1" + h.Id,
                                    Label = h.Adi,
                                    Value = h.Kapasite,
                                    Level = levelA,
                                    Group = groupA
                                })
                                .ToList();

                var depolar = context.Depolar
                                .Where(f => context.TransportSonuclar
                                                .Select(x => x.DepoId).Distinct().Contains(f.Id))
                                .Select(d => new TransportSonucNodes
                                {
                                    Id = "g2" + d.Id,
                                    Label = d.Adi,
                                    Value = d.Talep,
                                    Level = levelB,
                                    Group = groupB
                                })
                                .ToList();

                var nodes = fabrikalar.Union(depolar).ToList();

                return nodes;
            }

        }

        public List<TransportSonucEdges> GetTransportSonucEdges()
        {
            //{ from: 1, to: 5, value: 1,  label: 1,  title: 'FA->DE: 1 adet' },
            using (var context = new TransportContext())
            {
                var edges = context.TransportSonuclar
                                .Select(h => new TransportSonucEdges
                                {
                                    From = "g1" + h.FabrikaId,
                                    To = "g2" + h.DepoId,
                                    Value = h.Sonuc,
                                    Label = h.Sonuc,
                                    Title = "Fabrika "+ h.Fabrika.Adi + "->" + "Depo " + h.Depo.Adi + ": " + h.Sonuc + " adet"
                                })
                                .ToList();
                return edges;
            }
        }
    }
}
