using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers
{
    class TransportInitializer : DropCreateDatabaseIfModelChanges<TransportContext>
    {
        protected override void Seed(TransportContext context)
        {
            var fabrikalar = new List<Fabrika>()
                            {
                                new Fabrika(){ Adi="A", Kapasite=10 },
                                new Fabrika(){ Adi="B", Kapasite=23 },
                                new Fabrika(){ Adi="C", Kapasite=27 }
                            };


            fabrikalar.ForEach(f => context.Fabrikalar.Add(f));
            context.SaveChanges();

            var depolar = new List<Depo>()
                            {
                                new Depo(){ Adi="E", Talep=21 },
                                new Depo(){ Adi="F", Talep=14 },
                                new Depo(){ Adi="G", Talep=25 }
                            };

            depolar.ForEach(d => context.Depolar.Add(d));
            context.SaveChanges();

            var maliyetMatrisi = new List<TransportMaliyet>()
                            {
                                new TransportMaliyet(){ FabrikaId=1, DepoId=1, Deger=9 },
                                new TransportMaliyet(){ FabrikaId=1, DepoId=2, Deger=6 },
                                new TransportMaliyet(){ FabrikaId=1, DepoId=3, Deger=9 },

                                new TransportMaliyet(){ FabrikaId=2, DepoId=1, Deger=15 },
                                new TransportMaliyet(){ FabrikaId=2, DepoId=2, Deger=8 },
                                new TransportMaliyet(){ FabrikaId=2, DepoId=3, Deger=3 },

                                new TransportMaliyet(){ FabrikaId=3, DepoId=1, Deger=12 },
                                new TransportMaliyet(){ FabrikaId=3, DepoId=2, Deger=13 },
                                new TransportMaliyet(){ FabrikaId=3, DepoId=3, Deger=11 }
                            };

            maliyetMatrisi.ForEach(m => context.Maliyetler.Add(m));
            context.SaveChanges();

            //base.Seed(context);
        }
    }
}
