using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using System.Data.Entity.Migrations;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers
{
    //DropCreateDatabaseIfModelChanges
    public class EczaneNobetInitializer : DropCreateDatabaseIfModelChanges<EczaneNobetContext>
    {
        protected override void Seed(EczaneNobetContext context)
        {
            #region users
            var vUser = new List<User>()
                            {
            new User(){ Email="ozdamar85@gmail.com", FirstName="Semih", LastName="ÖZDAMAR", Password="123456", UserName="semih"},
            new User(){ Email="atesates2012@gmail.com", FirstName="Ateş", LastName="Ateş", Password="123456", UserName="ates"},
            new User(){ Email="huseyinecz@gmail.com", FirstName="Hüseyin", LastName="Eczane", Password="Alanya", UserName="huseyin"},

            new User(){ Email="odaAntalya@nobetyaz.com", FirstName="oda", LastName="Oda", Password="oda123", UserName="oda"},
            new User(){ Email="ustGrupAlanya@nobetyaz.com", FirstName="ustgrup", LastName="Oda", Password="ustgrup", UserName="ustGrupAlanya"},
            new User(){ Email="eczane@nobetyaz.com", FirstName="eczane", LastName="Oda", Password="eczane", UserName="eczane"},
            new User(){ Email="misafir@nobetyaz.com", FirstName="misafir", LastName="Oda", Password="misafir", UserName="misafir"},

            new User(){ Email="ustGrupAntalyaMerkez@nobetyaz.com", FirstName="ustgrup", LastName="Oda", Password="ustgrup", UserName="ustGrupAntalyaMerkez"}
                            };

            context.Users.AddOrUpdate(s => new { s.Email }, vUser.ToArray());
            context.SaveChanges();
            #endregion

            #region roles
            var vRole = new List<Role>()
                            {
                                new Role(){ Name="Admin" },
                                new Role(){ Name="Oda" },
                                new Role(){ Name="Üst Grup" },
                                new Role(){ Name="Eczane" },
                                new Role(){ Name="Misafir" }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            //vRole.ForEach(d => context.Roles.Add(d));
            context.SaveChanges();
            #endregion

            #region user roles
            var vuserRole = new List<UserRole>()
                            {
                                new UserRole(){ RoleId=1, UserId=1 },
                                new UserRole(){ RoleId=1, UserId=2 },
                                new UserRole(){ RoleId=3, UserId=3 },

                                new UserRole(){ RoleId=2, UserId=4 },
                                new UserRole(){ RoleId=3, UserId=5 },
                                new UserRole(){ RoleId=4, UserId=6 },
                                new UserRole(){ RoleId=5, UserId=7 },
                                new UserRole(){ RoleId=3, UserId=8 }
                            };

            context.UserRoles.AddOrUpdate(s => new { s.RoleId, s.UserId }, vuserRole.ToArray());
            context.SaveChanges();
            #endregion

            #region menüler
            var vMenu = new List<Menu>()
            {
            //Menü Single Items 1,2
            new Menu(){ LinkText="Eczaneler", ActionName="Index", ControllerName="Eczane", AreaName="EczaneNobet", SpanCssClass="fa fa-medkit fa-lg" },
            new Menu(){ LinkText="Eczane Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="fa fa-bug fa-lg" },

            //Menü Dropdown Titles 3,4,5,6,7
            new Menu(){ LinkText="Nöbet Gruplar", SpanCssClass="fa fa-bars"},
            new Menu(){ LinkText="Eczane Gruplar", SpanCssClass="fa fa-users"},
            new Menu(){ LinkText="Sonuçlar", SpanCssClass="fa fa-cubes" },
            new Menu(){ LinkText="Tanımlar", SpanCssClass="fa fa-database" },
            new Menu(){ LinkText="Yetki", SpanCssClass="fa fa-shield" },
            new Menu(){ LinkText="Nöbet Kural", SpanCssClass="fa fa-cogs" }
            };

            context.Menuler.AddOrUpdate(s => new { s.LinkText }, vMenu.ToArray());
            //vMenu.ForEach(d => context.Menuler.Add(d));
            context.SaveChanges();
            #endregion

            #region menü roller

            var vMenuRole = new List<MenuRole>()
                            {
                                //admin 
                                new MenuRole(){ MenuId=1, RoleId=1 },
                                new MenuRole(){ MenuId=2, RoleId=1 },
                                new MenuRole(){ MenuId=3, RoleId=1 },
                                new MenuRole(){ MenuId=4, RoleId=1 },
                                new MenuRole(){ MenuId=5, RoleId=1 },
                                new MenuRole(){ MenuId=6, RoleId=1 },
                                new MenuRole(){ MenuId=7, RoleId=1 },
                                new MenuRole(){ MenuId=8, RoleId=1 },
                                
                                //oda 
                                new MenuRole(){ MenuId=1, RoleId=2 },
                                new MenuRole(){ MenuId=2, RoleId=2 },
                                new MenuRole(){ MenuId=3, RoleId=2 },
                                new MenuRole(){ MenuId=4, RoleId=2 },
                                new MenuRole(){ MenuId=5, RoleId=2 },
                                new MenuRole(){ MenuId=7, RoleId=2 },
                                new MenuRole(){ MenuId=8, RoleId=2 },
                                
                                //nöbet üst grup 
                                new MenuRole(){ MenuId=1, RoleId=3 },
                                new MenuRole(){ MenuId=2, RoleId=3 },
                                new MenuRole(){ MenuId=3, RoleId=3 },
                                new MenuRole(){ MenuId=4, RoleId=3 },
                                new MenuRole(){ MenuId=5, RoleId=3 },
                                new MenuRole(){ MenuId=7, RoleId=3 },
                                new MenuRole(){ MenuId=8, RoleId=3 },

                                //eczacı
                                new MenuRole(){ MenuId=1, RoleId=4 },
                                new MenuRole(){ MenuId=2, RoleId=4 },
                                new MenuRole(){ MenuId=5, RoleId=4 },

                                //misafir
                                new MenuRole(){ MenuId=1, RoleId=5 },
                                new MenuRole(){ MenuId=2, RoleId=5 },
                                new MenuRole(){ MenuId=3, RoleId=5 },
                                new MenuRole(){ MenuId=4, RoleId=5 },
                                new MenuRole(){ MenuId=5, RoleId=5 }
                            };

            context.MenuRoles.AddOrUpdate(s => new { s.MenuId, s.RoleId }, vMenuRole.ToArray());
            //vMenuRole.ForEach(d => context.MenuRoles.Add(d));
            context.SaveChanges();

            #endregion

            #region menü altlar
            var vMenuAlt = new List<MenuAlt>()
            {
                //Nöbet Gruplar 1,2,3
                new MenuAlt(){ LinkText="Nöbet Üst Grup", ActionName="Index", ControllerName="NobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="Nöbet Grup", ActionName="Index", ControllerName="NobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },
                new MenuAlt(){ LinkText="Eczane Nöbet Grup", ActionName="Index", ControllerName="EczaneNobetGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3 },

                //Eczane Gruplar 4,5
                new MenuAlt(){ LinkText="Eczane Grup Tanım", ActionName="Index", ControllerName="EczaneGrupTanim", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4 },
                new MenuAlt(){ LinkText="Eczane Grup", ActionName="Index", ControllerName="EczaneGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=4},

                //Sonuçlar 6,7
                new MenuAlt(){ LinkText="Pivot Tablo", ActionName="Index", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5 },
                new MenuAlt(){ LinkText="Görsel Sonuçlar", ActionName="GorselSonuclar", ControllerName="EczaneNobetSonuc", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=5},

                //tanımlar 8,9,10
                new MenuAlt(){ LinkText="Eczanene Oda", ActionName="Index", ControllerName="EczaneOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret Tür", ActionName="Index", ControllerName="MazeretTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6 },
                new MenuAlt(){ LinkText="Mazeret", ActionName="Index", ControllerName="Mazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

                //yetkiler 11,12,13,14,15,16,17,18
                new MenuAlt(){ LinkText="Kullanıcı", ActionName="Register", ControllerName="Account", AreaName="", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü", ActionName="Index", ControllerName="Menu", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Alt", ActionName="Index", ControllerName="MenuAlt", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Rol", ActionName="Index", ControllerName="MenuRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Menü Alt Rol", ActionName="Index", ControllerName="MenuAltRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanıcı Oda", ActionName="Index", ControllerName="UserOda", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanıcı Nöbet Üst Grup", ActionName="Index", ControllerName="UserNobetUstGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},
                new MenuAlt(){ LinkText="Kullanıcı Eczane", ActionName="Index", ControllerName="UserEczane", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

                //tanımlar 19,20,21,22,23,24,25,26
                new MenuAlt(){ LinkText="İstek", ActionName="Index", ControllerName="Istek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="İstek Tür", ActionName="Index", ControllerName="IstekTur", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Nöbet Görev Tip", ActionName="Index", ControllerName="NobetGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Şehir", ActionName="Index", ControllerName="Sehir", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="İlçe", ActionName="Index", ControllerName="Ilce", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Rol", ActionName="Index", ControllerName="Role", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

                new MenuAlt(){ LinkText="Eczane Nöbet Mazeret", ActionName="Index", ControllerName="EczaneNobetMazeret", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},
                new MenuAlt(){ LinkText="Eczane Nöbet İstek", ActionName="Index", ControllerName="EczaneNobetIstek", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=2},

                //yetkiler 27
                new MenuAlt(){ LinkText="Kullanıcı Rol", ActionName="Index", ControllerName="UserRole", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=7},

                //nöbet kural 28,29
                new MenuAlt(){ LinkText="Nöbet Kural", ActionName="Index", ControllerName="NobetKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="Nöbet Grup Kural", ActionName="Index", ControllerName="NobetGrupKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

                //tanımlar 30,31,32
                new MenuAlt(){ LinkText="Görev Tip", ActionName="Index", ControllerName="GorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Eczane Görev Tip", ActionName="Index", ControllerName="EczaneGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},
                new MenuAlt(){ LinkText="Nöbet Grup Görev Tip", ActionName="Index", ControllerName="NobetGrupGorevTip", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=6},

                //Nöbet Gruplar 33
                new MenuAlt(){ LinkText="Nöbet Alt Grup", ActionName="Index", ControllerName="NobetAltGrup", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=3},

                //nöbet kural 34
                new MenuAlt(){ LinkText="Nöbet Grup Talepler", ActionName="Index", ControllerName="NobetGrupTalep", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},

                //nöbet kural 35,36
                new MenuAlt(){ LinkText="Nöbet Gün Kural", ActionName="Index", ControllerName="NobetGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
                new MenuAlt(){ LinkText="Nöbet Grup Gün Kural", ActionName="Index", ControllerName="NobetGrupGunKural", AreaName="EczaneNobet", SpanCssClass="dropdown-item", MenuId=8},
            };

            context.MenuAltlar.AddOrUpdate(s => new { s.LinkText }, vMenuAlt.ToArray());
            //vMenuAlt.ForEach(d => context.MenuAltlar.Add(d));
            context.SaveChanges();

            #endregion

            #region menü alt roller

            var vMenuAltRole = new List<MenuAltRole>()
                            {   
                                #region admin
		                        new MenuAltRole(){ MenuAltId=1, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=8, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=9, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=10, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=12, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=13, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=14, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=15, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=16, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=17, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=19, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=20, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=21, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=22, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=23, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=24, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=27, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=28, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=30, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=31, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=32, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=33, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=35, RoleId=1 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=1 }, 
	                            #endregion
                                
                                #region oda
		                        new MenuAltRole(){ MenuAltId=1, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=28, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=35, RoleId=2 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=2 }, 
	                            #endregion
                                
                                #region nöbet üst grup
		                                new MenuAltRole(){ MenuAltId=1, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=18, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=28, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=29, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=34, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=35, RoleId=3 },
                                new MenuAltRole(){ MenuAltId=36, RoleId=3 }, 
	                            #endregion
                                
                                #region eczacı
		                        new MenuAltRole(){ MenuAltId=1, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=4 }, 
	                            #endregion
                                
                                #region misafir
		                        new MenuAltRole(){ MenuAltId=1, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=4, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=5, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=11, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=5 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=5 } 
	                            #endregion
                            };

            context.MenuAltRoles.AddOrUpdate(s => new { s.MenuAltId, s.RoleId }, vMenuAltRole.ToArray());
            //vMenuAltRole.ForEach(d => context.MenuAltRoles.Add(d));
            context.SaveChanges();
            #endregion

            #region nobet gorev tipler
            var vNobetGorevTip = new List<NobetGorevTip>()
            {
                new NobetGorevTip(){ Adi="18:00 - 08.30" },
                new NobetGorevTip(){ Adi="18:00 - 00:00" },
                new NobetGorevTip(){ Adi="08:30 - 18:00" },
                new NobetGorevTip(){ Adi="08:30 - 00:00" }
            };

            context.NobetGorevTipler.AddOrUpdate(s => new { s.Adi }, vNobetGorevTip.ToArray());
            //vNobetGorevTip.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();

            #endregion

            #region nöbet gün kurallar
            var nobetGunKurallar = new List<NobetGunKural>()
                            {
                                new NobetGunKural(){ Adi="Pazar", Aciklama="Pazar günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Pazartesi", Aciklama="Pazartesi günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Salı", Aciklama="Salı günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Çarşamba", Aciklama="Çarşamba günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Perşembe", Aciklama="Perşembe günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Cuma", Aciklama="Cuma günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Cumartesi", Aciklama="Cumartesi günü eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Dini Bayram", Aciklama="Dini Bayram eşit dağılıma dahil olsun"},
                                new NobetGunKural(){ Adi="Milli Bayram", Aciklama="Milli Bayram eşit dağılıma dahil olsun"},
                            };

            context.NobetGunKurallar.AddOrUpdate(s => new { s.Adi }, nobetGunKurallar.ToArray());
            //nobetGunKurallar.ForEach(d => context.NobetGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet kurallar
            var nobetKurallar = new List<NobetKural>()
                            {
                                new NobetKural(){ Adi="Ardışık Nöbet Sayısı", Aciklama="Peşpeşe nöbet yazılmayacak ardışık gün sayısı"},
                                new NobetKural(){ Adi="Birlikte Nöbet Sayısı", Aciklama="Aynı Güne Denk Gelen Nöbet Sayısı"} // bu sayı 4 uygun
                            };

            context.NobetKurallar.AddOrUpdate(s => new { s.Adi }, nobetKurallar.ToArray());
            //nobetKurallar.ForEach(d => context.NobetKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region gün değerler 
            //(hafta ve bayramların gün değerleri)
            //var gunDegerler = new List<GunDeger>()
            //                {
            //                    new GunDeger(){ Adi="Pazar"},
            //                    new GunDeger(){ Adi="Pazartesi"},
            //                    new GunDeger(){ Adi="Salı"},
            //                    new GunDeger(){ Adi="Çarşamba"},
            //                    new GunDeger(){ Adi="Perşembe"},
            //                    new GunDeger(){ Adi="Cuma"},
            //                    new GunDeger(){ Adi="Cumartesi"},
            //                    new GunDeger(){ Adi="Dini Bayram"},
            //                    new GunDeger(){ Adi="Milli Bayram"}
            //                };

            //context.GunDegerler.AddOrUpdate(s => new { s.Adi }, gunDegerler.ToArray());
            ////gunDegerler.ForEach(d => context.GunDegerler.Add(d));
            //context.SaveChanges();
            #endregion

            #region takvimler
            List<Takvim> takvimler = new List<Takvim>();

            for (int y = 2018; y < 2023; y++)
            {
                for (int m = 1; m < 13; m++)
                {
                    var aydakiGunler = DateTime.DaysInMonth(y, m);

                    for (int d = 1; d <= aydakiGunler; d++)
                    {
                        takvimler.Add(new Takvim()
                        {
                            Tarih = new DateTime(y, m, d)
                        });
                    }
                }
            }
            context.Takvimler.AddOrUpdate(s => new { s.Tarih }, takvimler.ToArray());
            //takvimler.ForEach(d => context.Takvimler.Add(d));
            context.SaveChanges();

            #endregion

            #region nöbet görev tipler
            var nobbetGorevTipler = new List<NobetGorevTip>()
            {
                                new NobetGorevTip(){Adi = "Tam Gün Nöbetçi"},
                                new NobetGorevTip(){Adi = "Gündüz Nöbetçi"}
            };
            context.NobetGorevTipler.AddOrUpdate(s => new { s.Adi }, nobbetGorevTipler.ToArray());
            //nobbetGorevTipler.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            //bayram adı???
            #region bayramlar
            /*
            var bayramlar = new List<Bayram>()
                            {   
                                #region Tam gün
		                        new Bayram(){ TakvimId=1,   NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=113, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=121, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=139, NobetGorevTipId=1, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=165, NobetGorevTipId=1, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=166, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=167, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=168, NobetGorevTipId=1, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=232, NobetGorevTipId=1, NobetGunKuralId=0}, arefe
                                new Bayram(){ TakvimId=233, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=234, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=235, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=236, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=242, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=302, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=365, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=366, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=478, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=486, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=504, NobetGorevTipId=1, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=520, NobetGorevTipId=1, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=521, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=522, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=523, NobetGorevTipId=1, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=587, NobetGorevTipId=1, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=588, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=589, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=590, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=591, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=607, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=667, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=731, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=844, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=852, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=870, NobetGorevTipId=1, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=874, NobetGorevTipId=1, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=875, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=876, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=877, NobetGorevTipId=1, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=942, NobetGorevTipId=1, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=943, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=944, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=945, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=946, NobetGorevTipId=1, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=973, NobetGorevTipId=1, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=1033, NobetGorevTipId=1, NobetGunKuralId=9},

	                            #endregion
                                
                                #region gündüz
		                        new Bayram(){ TakvimId=1,   NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=113, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=121, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=139, NobetGorevTipId=2, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=165, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=166, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=167, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=168, NobetGorevTipId=2, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=232, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=233, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=234, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=235, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=236, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=242, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=302, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=365, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=366, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=478, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=486, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=504, NobetGorevTipId=2, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=520, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=521, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=522, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=523, NobetGorevTipId=2, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=587, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=588, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=589, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=590, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=591, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=607, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=667, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=731, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=844, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=852, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=870, NobetGorevTipId=2, NobetGunKuralId=9},
                                //new Bayram(){ TakvimId=874, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=875, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=876, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=877, NobetGorevTipId=2, NobetGunKuralId=8},
                                //new Bayram(){ TakvimId=942, NobetGorevTipId=2, NobetGunKuralId=0},
                                new Bayram(){ TakvimId=943, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=944, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=945, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=946, NobetGorevTipId=2, NobetGunKuralId=8},
                                new Bayram(){ TakvimId=973, NobetGorevTipId=2, NobetGunKuralId=9},
                                new Bayram(){ TakvimId=1033, NobetGorevTipId=2, NobetGunKuralId=9}

	                            #endregion
                            };

            context.Bayramlar.AddOrUpdate(s => new { s.TakvimId, s.NobetGorevTipId, s.NobetGunKuralId }, bayramlar.ToArray());
            //bayramlar.ForEach(d => context.Bayramlar.Add(d));
            context.SaveChanges();
            */
            #endregion
            
            #region eczane odalar
            var eczaneOdalar = new List<EczaneOda>()
            {
                new EczaneOda(){ Adi="Antalya", Adres="Antalya Merkez", TelefonNo="2423110329", MailAdresi="antalyaeczaciodasi@gmail.com", WebSitesi ="https://www.antalyaeo.org.tr/tr"}
            };

            context.EczaneOdalar.AddOrUpdate(s => new { s.Adi }, eczaneOdalar.ToArray());
            //eczaneOdalar.ForEach(d => context.EczaneOdalar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet üst gruplar
            var nobetUstGruplar = new List<NobetUstGrup>() {
                new NobetUstGrup(){Adi = "Alanya",Aciklama = "Antalya ilçesi",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
                new NobetUstGrup(){Adi = "Antalya Merkez",Aciklama = "Antalya merkez",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)},
            };

            context.NobetUstGruplar.AddOrUpdate(s => new { s.Adi }, nobetUstGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet gruplar

            var nobetGruplar = new List<NobetGrup>();

            int ustGrup = 1;
            for (int g = 1; g <= 3; g++)
            {
                nobetGruplar.Add(new NobetGrup()
                {
                    Adi = "Alanya-" + g,
                    BaslamaTarihi = new DateTime(2018, 1, 1),
                    NobetUstGrupId = ustGrup
                });
            };

            ustGrup = 2;
            for (int g = 4; g <= 14; g++)
            {
                nobetGruplar.Add(new NobetGrup()
                {
                    Adi = "Antalya-" + (g - 3),
                    BaslamaTarihi = new DateTime(2018, 1, 1),
                    NobetUstGrupId = ustGrup
                });
            };

            context.NobetGruplar.AddOrUpdate(s => new { s.Adi }, nobetGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region nöbet grup gün kurallar
            var nobetGrupGunKurallar = new List<NobetGrupGunKural>()
                            {
                        new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},

                        new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},

                        new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                        new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},

                    #region antalya merkez
		            new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=4, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=5, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=6, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=7, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=8, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=9, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=10, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=11, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=12, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=13, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=3, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=4, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=5, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=6, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=7, BaslangicTarihi=new DateTime(2018, 2, 1)},
                    new NobetGrupGunKural(){ NobetGrupId=14, NobetGunKuralId=8, BaslangicTarihi=new DateTime(2018, 2, 1)} 
	                #endregion
                            };

            context.NobetGrupGunKurallar.AddOrUpdate(s => new { s.NobetGrupId, s.NobetGunKuralId }, nobetGrupGunKurallar.ToArray());
            //nobetGrupGunKurallar.ForEach(d => context.NobetGrupGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup kurallar
            /*
            var nobetGrupKurallar = new List<NobetGrupKural>()
                            {
                    #region Alanya
		            //new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
              //      new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},
              //      new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=5},

              //      new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
              //      new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
              //      new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4}, 
	                #endregion

                    #region Antalya Merkez
		            new NobetGrupKural(){ NobetGrupId=4, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=5, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=6, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=7, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=8, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=9, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=10, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=11, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=12, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=13, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=14, NobetKuralId=1, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},

                    new NobetGrupKural(){ NobetGrupId=4, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=5, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=6, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=7, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=8, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=9, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=10, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=11, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=12, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=13, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4},
                    new NobetGrupKural(){ NobetGrupId=14, NobetKuralId=2, BaslangicTarihi=new DateTime(2018, 2, 1), Deger=4}, 
	                #endregion
                            };

            context.NobetGrupKurallar.AddOrUpdate(s => new { s.NobetGrupId, s.NobetKuralId }, nobetGrupKurallar.ToArray());
            //nobetGrupKurallar.ForEach(d => context.NobetGrupKurallar.Add(d));
            context.SaveChanges();
            */
            #endregion

            #region nöbet grup görev tipler
            var nobetGrupGorevTipler = new List<NobetGrupGorevTip>()
                            {
                    new NobetGrupGorevTip(){ NobetGrupId=1, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=2, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=3, NobetGorevTipId=1},

                    new NobetGrupGorevTip(){ NobetGrupId=4, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=5, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=6, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=7, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=8, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=9, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=10, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=11, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=12, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=13, NobetGorevTipId=1},
                    new NobetGrupGorevTip(){ NobetGrupId=14, NobetGorevTipId=1}
                            };

            context.NobetGrupGorevTipler.AddOrUpdate(s => new { s.NobetGrupId, s.NobetGorevTipId }, nobetGrupGorevTipler.ToArray());
            //nobetGrupGorevTipler.ForEach(d => context.NobetGrupGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup talepler
            //var nobetGrupTalepler = new List<NobetGrupTalep>()
            //                {
            //                    new NobetGrupTalep(){ TakvimId=1, NobetGrupGorevTipId=1, NobetciSayisi=2}
            //                };

            //context.NobetGrupTalepler.AddOrUpdate(s => new { s.TakvimId, s.NobetGrupGorevTipId, s.NobetciSayisi }, nobetGrupTalepler.ToArray());
            //context.SaveChanges();
            #endregion

            #region eczaneler

            var eczaneler = new List<Eczane>()
            {
                #region Alanya
                #region 1
                new Eczane { Adi = "ERENLEROBA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALAİYE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SİNAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLAY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AKÇALIOĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AY", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "EYÜP", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ŞİMŞEK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYDOĞAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ŞEKER", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜNEYLİOĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SARE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYNUR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "FARUK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "İKSİR", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "HİDAYET", AcilisTarihi = new DateTime(2018,1,1) }, 
                #endregion
                
                #region 2
                new Eczane { Adi = "MARTI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KAMBUROĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "YÜKSEK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NOYAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ARIKAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KASAPOĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ŞAHİN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "KOÇAK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÖKSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ASLI", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AKSU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALANYA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SELCEN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SİPAHİOĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TOROS", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALPER", AcilisTarihi = new DateTime(2018,1,1) }, 
                #endregion
                
                #region 3
                new Eczane { Adi = "SAĞLIK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ECE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ŞÜKRAN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TUNA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TURUNÇ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜLERYÜZ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BÜKE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BAŞAK", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BERNA AKÇALIOĞLU", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ALTUNBAŞ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "TUĞBA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "NİSA", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "ŞİRİN", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "AYYÜCE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "GÜNEŞ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "SEVİNDİ", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "BİLGE", AcilisTarihi = new DateTime(2018,1,1) },
                new Eczane { Adi = "FİLİZ", AcilisTarihi = new DateTime(2018,1,1) },
                #endregion
                #endregion

                #region Antalya
		 
                #region 1
                new Eczane { Adi = "SEVDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ YAPRAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜVENÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇİĞDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PASTÖR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HİLAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BUYRUKÇU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KOÇAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNAHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NOKTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DERİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZMEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "RODOPLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FUNDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIYMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ NİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ ADEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAVDIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ MELTEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELİF İNCE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAFRAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELTEM MERT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAMUK ŞEKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEFNE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NERGİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SİZİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZÇAĞLAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜRKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖNDER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKYÜZÜ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEÇKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜNEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ORUÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YİĞİT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FİDAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AİLE=ÖZGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYTİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAYSAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DENİZİN", AcilisTarihi = new DateTime(2018,2,1) }, 
                #endregion
                
                #region 2
		        new Eczane { Adi = "BAHAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ITIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DİLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜREL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ BURDUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLLÜK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERPİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURUKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASPENDOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTEKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YAKAMOZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEDİR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELMALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜNEŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖMÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VERİMLİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ŞARAMPOL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELVAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "METİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇOBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ ÖZLEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BİLGEHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜMİT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HONAMLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞUŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU DURAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEKİK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ŞULE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYKUT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ILGIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AZİM", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 3
		        new Eczane { Adi = "ETİLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEVİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜLİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÜZGÜN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUĞTEKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BATUHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÖK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KADIOĞLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONUKSEVER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ IŞIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKTAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEÇKİNER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALİ BERK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜVEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVDANLIOĞLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GAMZE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MAVİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DİKİLİTAŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAĞLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEVLANA", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 4
		        new Eczane { Adi = "CİHAN DİNÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MEYDAN ALPER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZEYNEP AKMAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞU GARAJI SAĞLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CUMHURİYET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ TUBA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UĞUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PINAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OLCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKUÇOĞLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YUNUS EMRE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKDENİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÖZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇEN EFE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURATPAŞA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MURAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAMANLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UĞURCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEMET", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAYBAŞI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERVE=ANTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEYZA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ULU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KİRAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERKAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAPLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MÜJGAN", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 5
		        new Eczane { Adi = "KEREM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MANDALİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANANAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İDİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZUHAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PORTAKAL ÇİÇEĞİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TARÇIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EVRİM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GENİŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BULVAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BENGİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FULYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÖNMEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TERMESSOS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZENCEFİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARZU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IŞIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAMLILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÖYÜM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ŞENDİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURUNÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FLORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAĞLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SENTEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIRIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KESKİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BALKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEŞİLBAHÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MÜĞREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHÇELİEVLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KUMBUL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELİFSU", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 6
		        new Eczane { Adi = "VOLKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İBRAHİM ÖZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YALÇINER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHHİYE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU-M", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERRİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUĞBA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FARMA LARA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GENCA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANDEMİR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZDENİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖKALİPTUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İKİZLER=SARIKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FERAH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURKUAZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ŞİRİNYALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUANA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "OKYANUS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VİTAMİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "IŞILAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHADIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜNAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZKENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZDEMİR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖKÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAHÇELİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "POSTACILAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATLAS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ YILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERTER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARINAKLAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LARA SAĞLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BURCU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İNCESU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZGÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DAMLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UĞUR UYSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SIHHAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDEM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖRNEKKÖY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKŞEHİR", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 7
		        new Eczane { Adi = "GÜLENYÜZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BÜYÜK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEVA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELİF", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAŞGÖR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERÇİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜLKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYDINLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SELİS", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MASAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İKİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÜLTÜR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOLUNAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAZLI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CENGİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEMİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ GÖLHİSAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARSLAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇAĞLAYAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BAŞAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MELDA", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 8
		        new Eczane { Adi = "ARAPSUYU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATILGAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANSEV", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SELMİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KIVANÇ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLNUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KONYAALTI BİLGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AVKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PERA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜZEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LİMAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EZGİM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELİT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURTOĞLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALYA SU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EGE BORA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLŞİFA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DENİZ YILDIZI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALTINKUM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "PAPATYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ELVİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DAĞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SİMGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ESMELER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKINCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERKAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BIÇAKÇI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TÜTÜNCÜ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UYAR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUĞÇE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SOYTÜRK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYSUN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞANTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ ARAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ANTALYA MODERN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALPSOY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MATRUŞKA=KB", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 9
		        new Eczane { Adi = "SÖĞÜT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇÖZEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ERDOĞAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CERENSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZ ANADOLU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÖZDE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYŞEGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İNANIR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKTAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TATLICAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZCAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KAYRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOKER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KÜÇÜKSU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÜNSAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLTALYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NADİREM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÇALIM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GÜLEN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEHİR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TAŞELİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BARIŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DİLŞAD", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KEPEZALTI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EREĞLİ ANIL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TOPKAYA", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 10
		        new Eczane { Adi = "SİBEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "UTKU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FREZYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAZAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEVGİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AYLİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TURGAY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEŞİLIRMAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "İKRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEZER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NEZİH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YILDIZ RÜYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EDA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CANAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "MERKEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EMEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKYILDIZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BİLGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LEVENT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ALKIŞ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAKARYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BABACAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DEMİRGÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YURTPINAR UĞUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YÜCEL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NİLAY", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion
                
                #region 11
		        new Eczane { Adi = "YENİ VARSAK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YİĞİTBAŞI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LALE PARK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YENİ EGE", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SEMİH", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "FİLİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ONUR", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞANTÜRK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CİHAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BEREN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DOĞRU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KORKUTELİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "NAR ÇİÇEĞİ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "VARSAK GÜNEY", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAYGILI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DUYGU TOPLUK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "EYLÜL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SAHİL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ZERRİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÜTÇÜLER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SERRA BALTA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ATA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "GEBİZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KALAYCI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "YEŞİLYAYLA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "DÜNYA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SARIÇOBAN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "SÜTÇÜLER SAĞLIK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ASYA KEPEZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BİLLUR ÇELİK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BANU YALÇIN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "LOKMAN HEKİM", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "HAYAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KARAL", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TALAT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ARPEK", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "CEVHER", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "TUNCALI", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KURU", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "BERRA", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖZYURT", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "KOLSUZ", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "AKTİN", AcilisTarihi = new DateTime(2018,2,1) },
                new Eczane { Adi = "ÖMER YILDIZ", AcilisTarihi = new DateTime(2018,2,1) }, 
	            #endregion 

	            #endregion
            };

            context.Eczaneler.AddOrUpdate(s => new { s.Adi, s.AcilisTarihi }, eczaneler.ToArray());
            context.SaveChanges();
            #endregion

            #region mazeret türler
            var mazeretTurler = new List<MazeretTur>()
                            {
                                new MazeretTur(){ Adi="Çok Önemli"},
                                new MazeretTur(){ Adi="Önemli"},
                                new MazeretTur(){ Adi="Az Önemli"}
                            };

            context.MazeretTurler.AddOrUpdate(s => new { s.Adi }, mazeretTurler.ToArray());
            //mazeretTurler.ForEach(d => context.MazeretTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region istek türler
            var istekTurler = new List<IstekTur>()
                            {
                                new IstekTur(){ Adi="Çok Önemli"},
                                new IstekTur(){ Adi="Önemli"},
                                new IstekTur(){ Adi="Az Önemli"}
                            };

            context.IstekTurler.AddOrUpdate(s => new { s.Adi }, istekTurler.ToArray());
            //istekTurler.ForEach(d => context.IstekTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region mazeretler
            var mazeretler = new List<Mazeret>()
                            {
                                new Mazeret(){ Adi="Sağlık", MazeretTurId=1}
                            };

            context.Mazeretler.AddOrUpdate(s => new { s.Adi }, mazeretler.ToArray());
            //mazeretler.ForEach(d => context.Mazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region istekler
            var istekler = new List<Istek>()
                            {
                                new Istek(){ Adi="Sıralı Nöbet", IstekTurId=1},
                                new Istek(){ Adi="Sağlık", IstekTurId=1}
                            };

            context.Istekler.AddOrUpdate(s => new { s.Adi }, istekler.ToArray());
            //istekler.ForEach(d => context.Istekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet mazeretler
            //var eczaneNobetMazeretler = new List<EczaneNobetMazeret>()
            //{
            //    new EczaneNobetMazeret(){ EczaneId=1, MazeretId=1, Aciklama="Deneme", TakvimId=1 },
            //};

            //context.EczaneNobetMazeretler.AddOrUpdate(s => new { s.EczaneId, s.MazeretId, s.TakvimId }, eczaneNobetMazeretler.ToArray());
            ////eczaneNobetMazeretler.ForEach(d => context.EczaneNobetMazeretler.Add(d));
            //context.SaveChanges();
            #endregion

            #region eczane nöbet istekler

            var eczaneNobetIstekler = new List<EczaneNobetIstek>()
                            {
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sırayla", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sırayla", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sırayla", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sırayla", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sırayla", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sırayla", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sırayla", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sırayla", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sırayla", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sırayla", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sırayla", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sırayla", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sırayla", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sırayla", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sırayla", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sırayla", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sırayla", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sırayla", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sırayla", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sırayla", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sırayla", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sırayla", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sırayla", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sırayla", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sırayla", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sırayla", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sırayla", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sırayla", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sırayla", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sırayla", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sırayla", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sırayla", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sırayla", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sırayla", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sırayla", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sırayla", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sırayla", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sırayla", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sırayla", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sırayla", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sırayla", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sırayla", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sırayla", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sırayla", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sırayla", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sırayla", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sırayla", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sırayla", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sırayla", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sırayla", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sırayla", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sırayla", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sırayla", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sırayla", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sırayla", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sırayla", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sırayla", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sırayla", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sırayla", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sırayla", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sırayla", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sırayla", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sırayla", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sırayla", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sırayla", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sırayla", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sırayla", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sırayla", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sırayla", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sırayla", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sırayla", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sırayla", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sırayla", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sırayla", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sırayla", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sırayla", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=1, IstekId=1, Aciklama="Sırayla", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=2, IstekId=1, Aciklama="Sırayla", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=3, IstekId=1, Aciklama="Sırayla", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=4, IstekId=1, Aciklama="Sırayla", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=5, IstekId=1, Aciklama="Sırayla", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=6, IstekId=1, Aciklama="Sırayla", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=7, IstekId=1, Aciklama="Sırayla", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=8, IstekId=1, Aciklama="Sırayla", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=9, IstekId=1, Aciklama="Sırayla", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=10, IstekId=1, Aciklama="Sırayla", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=11, IstekId=1, Aciklama="Sırayla", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=12, IstekId=1, Aciklama="Sırayla", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=13, IstekId=1, Aciklama="Sırayla", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=14, IstekId=1, Aciklama="Sırayla", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=15, IstekId=1, Aciklama="Sırayla", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=16, IstekId=1, Aciklama="Sırayla", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=17, IstekId=1, Aciklama="Sırayla", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=18, IstekId=1, Aciklama="Sırayla", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Sırayla", TakvimId=665},

                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=19, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=21, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=22, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=23, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=24, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=25, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=26, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=27, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=28, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=29, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=30, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=31, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=32, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=33, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=34, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=35, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=36, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=37, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=38, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=39, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=20, IstekId=1, Aciklama="Deneme", TakvimId=714},

                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=40, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=41, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=44, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=43, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=42, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=45, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=46, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=47, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=48, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=49, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=50, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=51, IstekId=1, Aciklama="Deneme", TakvimId=714},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=52, IstekId=1, Aciklama="Deneme", TakvimId=721},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=53, IstekId=1, Aciklama="Deneme", TakvimId=728},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=54, IstekId=1, Aciklama="Deneme", TakvimId=735},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=55, IstekId=1, Aciklama="Deneme", TakvimId=742},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=56, IstekId=1, Aciklama="Deneme", TakvimId=749},
                                new EczaneNobetIstek(){ EczaneNobetGrupId=57, IstekId=1, Aciklama="Deneme", TakvimId=756}
                            };

            context.EczaneNobetIstekler.AddOrUpdate(s => new { s.EczaneNobetGrupId, s.IstekId, s.TakvimId }, eczaneNobetIstekler.ToArray());
            //eczaneNobetIstekler.ForEach(d => context.EczaneNobetIstekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet muafiyet
            var eczaneNobetMuafiyetler = new List<EczaneNobetMuafiyet>()
                            {
                                new EczaneNobetMuafiyet(){ EczaneId=1, BaslamaTarihi=new DateTime(2018, 2, 1), BitisTarihi=new DateTime(2018, 2, 1).AddDays(30), Aciklama="deneme için muaftır" }
                            };

            context.EczaneNobetMuafiyetler.AddOrUpdate(s => new { s.EczaneId, s.BaslamaTarihi }, eczaneNobetMuafiyetler.ToArray());
            //eczaneNobetMuafiyetler.ForEach(d => context.EczaneNobetMuafiyetler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet gruplar
            /*
            var eczaneNobetGruplar = new List<EczaneNobetGrup>()
            {
                #region Alanya
		        #region 1
		        new EczaneNobetGrup() { EczaneId = 1, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 2, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 3, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 4, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 5, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 6, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 7, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 8, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 9, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 10, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 11, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 12, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 13, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 14, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 15, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 16, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 17, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 18, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 19, NobetGrupId = 1, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" }, 
                #endregion

                #region 2
	new EczaneNobetGrup() { EczaneId = 20, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 21, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 22, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 23, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 24, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 25, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 26, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 27, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 28, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 29, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 30, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 31, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 32, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 33, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 34, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 35, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 36, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 37, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 38, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 39, NobetGrupId = 2, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" }, 
#endregion

                #region 3
	            new EczaneNobetGrup() { EczaneId = 40, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 41, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 42, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 43, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 44, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 45, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 46, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 47, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 48, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 49, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 50, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 51, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 52, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 53, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 54, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 55, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 56, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
                new EczaneNobetGrup() { EczaneId = 57, NobetGrupId = 3, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
#endregion 
	            #endregion

                #region Antalya

                new EczaneNobetGrup() { EczaneId =58, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =59, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =60, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =61, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =62, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =63, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =64, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =65, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =66, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =67, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =68, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =69, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =70, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =71, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =72, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =73, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =74, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =75, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =76, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =77, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =78, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =79, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =80, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =81, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =82, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =83, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =84, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =85, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =86, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =87, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =88, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =89, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =90, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =91, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =92, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =93, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =94, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =95, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =96, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =97, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =98, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =99, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =100, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =101, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =102, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =103, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =104, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =105, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =106, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =107, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =108, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =109, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =110, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =111, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =112, NobetGrupId = 4, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =113, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =114, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =115, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =116, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =117, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =118, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =119, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =120, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =121, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =122, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =123, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =124, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =125, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =126, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =127, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =128, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =129, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =130, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =131, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =132, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =133, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =134, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =135, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =136, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =137, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =138, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =139, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =140, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =141, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =142, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =143, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =144, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =145, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =146, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =147, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =148, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =149, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =150, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =151, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =152, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =153, NobetGrupId = 5, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =154, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =155, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =156, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =157, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =158, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =159, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =160, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =161, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =162, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =163, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =164, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =165, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =166, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =167, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =168, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =169, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =170, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =171, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =172, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =173, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =174, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =175, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =176, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =177, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =178, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =179, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =180, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =181, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =182, NobetGrupId = 6, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =183, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =184, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =185, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =186, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =187, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =188, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =189, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =190, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =191, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =192, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =193, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =194, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =195, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =196, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =197, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =198, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =199, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =200, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =201, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =202, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =203, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =204, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =205, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =206, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =207, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =208, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =209, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =210, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =211, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =212, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =213, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =214, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =215, NobetGrupId = 7, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =216, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =217, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =218, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =219, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =220, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =221, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =222, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =223, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =224, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =225, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =226, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =227, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =228, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =229, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =230, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =231, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =232, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =233, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =234, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =235, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =236, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =237, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =238, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =239, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =240, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =241, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =242, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =243, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =244, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =245, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =246, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =247, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =248, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =249, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =250, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =251, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =252, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =253, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =254, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =255, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =256, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =257, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =258, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =259, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =260, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =261, NobetGrupId = 8, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =262, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =263, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =264, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =265, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =266, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =267, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =268, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =269, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =270, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =271, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =272, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =273, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =274, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =275, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =276, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =277, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =278, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =279, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =280, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =281, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =282, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =283, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =284, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =285, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =286, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =287, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =288, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =289, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =290, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =291, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =292, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =293, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =294, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =295, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =296, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =297, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =298, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =299, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =300, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =301, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =302, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =303, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =304, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =305, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =306, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =307, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =308, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =309, NobetGrupId = 9, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =310, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =311, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =312, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =313, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =314, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =315, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =316, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =317, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =318, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =319, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =320, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =321, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =322, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =323, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =324, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =325, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =326, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =327, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =328, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =329, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =330, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =331, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =332, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =333, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =334, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =335, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =336, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =337, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =338, NobetGrupId = 10, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =339, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =340, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =341, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =342, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =343, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =344, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =345, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =346, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =347, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =348, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =349, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =350, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =351, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =352, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =353, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =354, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =355, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =356, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =357, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =358, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =359, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =360, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =361, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =362, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =363, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =364, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =365, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =366, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =367, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =368, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =369, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =370, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =371, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =372, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =373, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =374, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =375, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =376, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =377, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =378, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =379, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =380, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =381, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =382, NobetGrupId = 11, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =383, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =384, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =385, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =386, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =387, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =388, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =389, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =390, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =391, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =392, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =393, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =394, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =395, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =396, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =397, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =398, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =399, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =400, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =401, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =402, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =403, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =404, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =405, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =406, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =407, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =408, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =409, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =410, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =411, NobetGrupId = 12, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =412, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =413, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =414, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =415, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =416, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =417, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =418, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =419, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =420, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =421, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =422, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =423, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =424, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =425, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =426, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =427, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =428, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =429, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =430, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =431, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =432, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =433, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =434, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =435, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =436, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =437, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =438, NobetGrupId = 13, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =439, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =440, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =441, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =442, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =443, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =444, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =445, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =446, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =447, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =448, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =449, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =450, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =451, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =452, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =453, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =454, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =455, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =456, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =457, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =458, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =459, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =460, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =461, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =462, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =463, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =464, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =465, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =466, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =467, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =468, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =469, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =470, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =471, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =472, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =473, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =474, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =475, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =476, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =477, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =478, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =479, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =480, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =481, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },
new EczaneNobetGrup() { EczaneId =482, NobetGrupId = 14, BaslangicTarihi = new DateTime(2018,1,1), Aciklama = "-" },

	#endregion
            };
            context.EczaneNobetGruplar.AddOrUpdate(s => new { s.EczaneId }, eczaneNobetGruplar.ToArray());
            context.SaveChanges();
            */
            #endregion

            #region eczane nöbet sonuç aktifler
            //var v13 = new List<EczaneNobetSonucAktif>()
            //                {
            //                    new EczaneNobetSonucAktif(){ EczaneNobetGrupId=1, TakvimId=1 }
            //                };

            //v13.ForEach(d => context.EczaneNobetSonucAktifler.Add(d));
            //context.SaveChanges();
            #endregion

            #region eczane nöbet sonuçlar
            //var v6 = new List<EczaneNobetSonuc>()
            //                {
            //                    new EczaneNobetSonuc(){ EczaneNobetGrupId=1, TakvimId=1 }
            //                };

            //v6.ForEach(d => context.EczaneNobetSonuclar.Add(d));
            //context.SaveChanges();
            #endregion

            #region eczane grup tanımlar

            var eczaneGrupTanimlar = new List<EczaneGrupTanim>()
                            {
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="HAYAT_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SİPAHİOĞLU_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SELCEN _SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TEZCAN_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_SU", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALTUNBAŞ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜLERYÜZ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALTUNBAŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜLERYÜZ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"},
                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=new DateTime(2018, 1, 1),  Aciklama="-"}


                            };

            context.EczaneGrupTanimlar.AddOrUpdate(s => new { s.NobetUstGrupId, s.Adi }, eczaneGrupTanimlar.ToArray());
            //eczaneGrupTanimlar.ForEach(d => context.EczaneGrupTanimlar.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane gruplama

            var eczaneGruplar = new List<EczaneGrup>()
                            {
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=1 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=2 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=3 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=4 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=5 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=6 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=7 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=8 },
                                new EczaneGrup(){ EczaneId=39, EczaneGrupTanimId=9 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=10 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=11 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=12 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=13 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=14 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=15 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=16 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=17 },
                                new EczaneGrup(){ EczaneId=32, EczaneGrupTanimId=18 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=19 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=20 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=21 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=22 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=23 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=24 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=25 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=26 },
                                new EczaneGrup(){ EczaneId=30, EczaneGrupTanimId=27 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=28 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=29 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=30 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=31 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=32 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=33 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=34 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=35 },
                                new EczaneGrup(){ EczaneId=29, EczaneGrupTanimId=36 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=37 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=38 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=39 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=40 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=41 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=42 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=43 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=44 },
                                new EczaneGrup(){ EczaneId=38, EczaneGrupTanimId=45 },
                                new EczaneGrup(){ EczaneId=37, EczaneGrupTanimId=46 },
                                new EczaneGrup(){ EczaneId=36, EczaneGrupTanimId=47 },
                                new EczaneGrup(){ EczaneId=35, EczaneGrupTanimId=48 },
                                new EczaneGrup(){ EczaneId=22, EczaneGrupTanimId=49 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=50 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=51 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=52 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=53 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=54 },
                                new EczaneGrup(){ EczaneId=13, EczaneGrupTanimId=55 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=56 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=57 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=58 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=59 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=60 },
                                new EczaneGrup(){ EczaneId=2, EczaneGrupTanimId=61 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=62 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=63 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=64 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=65 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=66 },
                                new EczaneGrup(){ EczaneId=14, EczaneGrupTanimId=67 },
                                new EczaneGrup(){ EczaneId=49, EczaneGrupTanimId=68 },
                                new EczaneGrup(){ EczaneId=45, EczaneGrupTanimId=69 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=70 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=71 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=72 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=73 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=74 },
                                new EczaneGrup(){ EczaneId=1, EczaneGrupTanimId=75 },

                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=1 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=2 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=3 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=4 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=5 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=6 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=7 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=8 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=9 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=10 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=11 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=12 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=13 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=14 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=15 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=16 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=17 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=18 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=19 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=20 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=21 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=22 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=23 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=24 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=25 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=26 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=27 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=28 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=29 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=30 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=31 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=32 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=33 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=34 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=35 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=36 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=37 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=38 },
                                new EczaneGrup(){ EczaneId=51, EczaneGrupTanimId=39 },
                                new EczaneGrup(){ EczaneId=50, EczaneGrupTanimId=40 },
                                new EczaneGrup(){ EczaneId=52, EczaneGrupTanimId=41 },
                                new EczaneGrup(){ EczaneId=56, EczaneGrupTanimId=42 },
                                new EczaneGrup(){ EczaneId=53, EczaneGrupTanimId=43 },
                                new EczaneGrup(){ EczaneId=57, EczaneGrupTanimId=44 },
                                new EczaneGrup(){ EczaneId=46, EczaneGrupTanimId=45 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=46 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=47 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=48 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=49 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=50 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=51 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=52 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=53 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=54 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=55 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=56 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=57 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=58 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=59 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=60 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=61 },
                                new EczaneGrup(){ EczaneId=33, EczaneGrupTanimId=62 },
                                new EczaneGrup(){ EczaneId=27, EczaneGrupTanimId=63 },
                                new EczaneGrup(){ EczaneId=28, EczaneGrupTanimId=64 },
                                new EczaneGrup(){ EczaneId=31, EczaneGrupTanimId=65 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=66 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=67 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=68 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=69 },
                                new EczaneGrup(){ EczaneId=55, EczaneGrupTanimId=70 },
                                new EczaneGrup(){ EczaneId=20, EczaneGrupTanimId=71 },
                                new EczaneGrup(){ EczaneId=49, EczaneGrupTanimId=72 },
                                new EczaneGrup(){ EczaneId=34, EczaneGrupTanimId=73 },
                                new EczaneGrup(){ EczaneId=45, EczaneGrupTanimId=74 },
                                new EczaneGrup(){ EczaneId=54, EczaneGrupTanimId=75 }

                            };

            context.EczaneGruplar.AddOrUpdate(s => new { s.EczaneId, s.EczaneGrupTanimId }, eczaneGruplar.ToArray());
            //eczaneGruplar.ForEach(d => context.EczaneGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region user eczane odalar
            var userEczaneOdalar = new List<UserEczaneOda>()
                            {
                                new UserEczaneOda(){ EczaneOdaId =1, UserId=4 }
                            };

            context.UserEczaneOdalar.AddOrUpdate(s => new { s.EczaneOdaId, s.UserId }, userEczaneOdalar.ToArray());
            context.SaveChanges();
            #endregion

            #region user nobet üst gruplar
            var userNobetUstGruplar = new List<UserNobetUstGrup>()
                            {
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=3 },
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=5 },
                                new UserNobetUstGrup(){  NobetUstGrupId=2, UserId=8 }
                            };

            context.UserNobetUstGruplar.AddOrUpdate(s => new { s.NobetUstGrupId, s.UserId }, userNobetUstGruplar.ToArray());
            context.SaveChanges();
            #endregion

            #region user eczaneler
            var userEczaneler = new List<UserEczane>()
                            {
                                new UserEczane(){ EczaneId=1, UserId=6 }
                            };

            context.UserEczaneler.AddOrUpdate(s => new { s.EczaneId, s.UserId }, userEczaneler.ToArray());
            context.SaveChanges();
            #endregion

            #region şehirler
            var vSehirler = new List<Sehir>()
                            {
                                new Sehir(){ Adi="Antalya", EczaneOdaId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            vSehirler.ForEach(d => context.Sehirler.Add(d));
            context.SaveChanges();

            #endregion

            #region ilçeler
            var vIlceler = new List<Ilce>()
                            {
                                new Ilce(){ Adi="Alanya", SehirId=1 },
                                new Ilce(){ Adi="Muratpaşa", SehirId=1 },
                                new Ilce(){ Adi="Konyaaltı", SehirId=1 },
                                new Ilce(){ Adi="Kepez", SehirId=1 }
                            };

            context.Roles.AddOrUpdate(s => new { s.Name }, vRole.ToArray());
            vIlceler.ForEach(d => context.Ilceler.Add(d));
            context.SaveChanges();

            #endregion

            #region eczane ilçeler
            //var vEczaneIlceler = new List<EczaneIlce>()
            //                {
            //                   // new EczaneIlce(){  },
            //                };

            //vEczaneIlceler.ForEach(d => context.EczaneIlceler.Add(d));
            //context.SaveChanges();

            #endregion
        }
    }
}
