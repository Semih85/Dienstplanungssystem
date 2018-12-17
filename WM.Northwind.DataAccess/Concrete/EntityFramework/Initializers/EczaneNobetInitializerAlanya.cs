using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.DataAccess.Migrations;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Initializers
{
    //DropCreateDatabaseIfModelChanges
    //DropCreateDatabaseIfModelChanges<EczaneNobetContext>
    public class EczaneNobetInitializerAlanya : DropCreateDatabaseIfModelChanges<EczaneNobetContext> //MigrateDatabaseToLatestVersion<EczaneNobetContext, Configuration>
    {   
        /*
        protected override void Seed(EczaneNobetContext context)
        {
            #region users
            var vUser = new List<User>()
                            {
                                new User(){ Email="ozdamar85@gmail.com", FirstName="Semih", LastName="ÖZDAMAR", Password="123456", UserName="semih"},
                                new User(){ Email="atesates2012@gmail.com", FirstName="Ateş", LastName="Ateş", Password="123456", UserName="ates"},
                                new User(){ Email="huseyinecz@gmail.com", FirstName="Hüseyin", LastName="Eczane", Password="Alanya", UserName="huseyin"},

                                new User(){ Email="oda@nobetyaz.com", FirstName="oda", LastName="Oda", Password="oda123", UserName="oda"},
                                new User(){ Email="ustgrup@nobetyaz.com", FirstName="ustgrup", LastName="Oda", Password="ustgrup", UserName="ustgrup"},
                                new User(){ Email="eczane@nobetyaz.com", FirstName="eczane", LastName="Oda", Password="eczane", UserName="eczane"},
                                new User(){ Email="misafir@nobetyaz.com", FirstName="misafir", LastName="Oda", Password="misafir", UserName="misafir"}
                            };

            vUser.ForEach(d => context.Users.Add(d));
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

            vRole.ForEach(d => context.Roles.Add(d));
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

                            };

            vuserRole.ForEach(d => context.UserRoles.Add(d));
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

            vMenu.ForEach(d => context.Menuler.Add(d));
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

            vMenuRole.ForEach(d => context.MenuRoles.Add(d));
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

            vMenuAlt.ForEach(d => context.MenuAltlar.Add(d));
            context.SaveChanges();

            #endregion

            #region menü alt roller

            var vMenuAltRole = new List<MenuAltRole>()
                            {
                                //admin
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

                                //oda
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

                                //nöbet üst grup
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
                                
                                //eczacı
                                new MenuAltRole(){ MenuAltId=1, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=2, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=3, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=6, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=7, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=25, RoleId=4 },
                                new MenuAltRole(){ MenuAltId=26, RoleId=4 },

                                
                                //misafir
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
                            };

            vMenuAltRole.ForEach(d => context.MenuAltRoles.Add(d));
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

            vNobetGorevTip.ForEach(d => context.NobetGorevTipler.Add(d));
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

            nobetGunKurallar.ForEach(d => context.NobetGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet kurallar
            var nobetKurallar = new List<NobetKural>()
                            {
                                new NobetKural(){ Adi="Ardışık Nöbet Sayısı", Aciklama="Peşpeşe nöbet yazılmayacak ardışık gün sayısı"}
                            };

            nobetKurallar.ForEach(d => context.NobetKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region gün değerler 
            //(hafta ve bayramların gün değerleri)
            var v12 = new List<GunDeger>()
                            {
                                new GunDeger(){ Adi="Pazar"},
                                new GunDeger(){ Adi="Pazartesi"},
                                new GunDeger(){ Adi="Salı"},
                                new GunDeger(){ Adi="Çarşamba"},
                                new GunDeger(){ Adi="Perşembe"},
                                new GunDeger(){ Adi="Cuma"},
                                new GunDeger(){ Adi="Cumartesi"},
                                new GunDeger(){ Adi="Dini Bayram"},
                                new GunDeger(){ Adi="Milli Bayram"}
                            };

            v12.ForEach(d => context.GunDegerler.Add(d));
            context.SaveChanges();
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
            takvimler.ForEach(d => context.Takvimler.Add(d));
            context.SaveChanges();

            #endregion

            #region nöbet görev tipler
            var nobbetGorevTipler = new List<NobetGorevTip>()
            {
                                new NobetGorevTip(){Adi = "Tam Gün Nöbetçi"},
                                new NobetGorevTip(){Adi = "Gündüz Nöbetçi"}
            };
            nobbetGorevTipler.ForEach(d => context.NobetGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            //bayram adı???
            #region bayramlar
            var v1 = new List<Bayram>()
                            {
                                //Tam gün
                                new Bayram(){ TakvimId=1,   NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=113, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=121, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=139, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=165, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=166, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=167, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=168, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=232, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=233, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=234, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=235, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=236, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=242, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=302, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=365, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=366, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=478, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=486, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=504, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=520, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=521, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=522, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=523, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=587, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=588, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=589, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=590, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=591, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=607, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=667, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=731, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=844, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=852, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=870, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=874, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=875, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=876, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=877, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=942, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=943, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=944, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=945, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=946, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=973, NobetGorevTipId=1, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=1033, NobetGorevTipId=1, NobetGunKuralId=9 },

                                //gündüz
                                new Bayram(){ TakvimId=1,   NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=113, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=121, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=139, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=165, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=166, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=167, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=168, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=232, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=233, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=234, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=235, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=236, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=242, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=302, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=365, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=366, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=478, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=486, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=504, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=520, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=521, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=522, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=523, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=587, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=588, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=589, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=590, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=591, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=607, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=667, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=731, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=844, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=852, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=870, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=874, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=875, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=876, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=877, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=942, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=943, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=944, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=945, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=946, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=973, NobetGorevTipId=2, NobetGunKuralId=9 },
                                new Bayram(){ TakvimId=1033, NobetGorevTipId=2, NobetGunKuralId=9 }

                            };

            v1.ForEach(d => context.Bayramlar.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane odalar
            var v7 = new List<EczaneOda>()
                            {
                                new EczaneOda(){ Adi="Antalya", Adres="Antalya Merkez", TelefonNo=""}
                            };

            v7.ForEach(d => context.EczaneOdalar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet üst gruplar

            var nobetUstGruplariAlanya = new List<NobetUstGrup>() {
                new NobetUstGrup(){Adi = "Alanya-1",Aciklama = "Antalya ilçesi",EczaneOdaId = 1, BaslangicTarihi=new DateTime(2018,1,1)}
            };

            nobetUstGruplariAlanya.ForEach(d => context.NobetUstGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet gruplar

            var nobetGruplariAlanya = new List<NobetGrup>();

            int ustGrup = 1;
            for (int g = 1; g <= 3; g++)
            {
                nobetGruplariAlanya.Add(new NobetGrup()
                {
                    Adi = "Alanya-" + g,

                    BaslamaTarihi = DateTime.Now,
                    NobetUstGrupId = ustGrup
                });
            };

            nobetGruplariAlanya.ForEach(d => context.NobetGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup gün kurallar
            var nobetGrupGunKurallar = new List<NobetGrupGunKural>()
                            {
                                new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=1, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=2, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=6, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=1, NobetGunKuralId=7, BaslangicTarihi=DateTime.Now},

                                new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=1, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=2, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=3, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=6, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=2, NobetGunKuralId=7, BaslangicTarihi=DateTime.Now},

                                new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=1, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=6, BaslangicTarihi=DateTime.Now},
                                new NobetGrupGunKural(){ NobetGrupId=3, NobetGunKuralId=7, BaslangicTarihi=DateTime.Now}
                            };

            nobetGrupGunKurallar.ForEach(d => context.NobetGrupGunKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup kurallar
            var nobetGrupKurallar = new List<NobetGrupKural>()
                            {
                                new NobetGrupKural(){ NobetGrupId=1, NobetKuralId=1, BaslangicTarihi=DateTime.Now, Deger=5},
                                new NobetGrupKural(){ NobetGrupId=2, NobetKuralId=1, BaslangicTarihi=DateTime.Now, Deger=5},
                                new NobetGrupKural(){ NobetGrupId=3, NobetKuralId=1, BaslangicTarihi=DateTime.Now, Deger=5}
                            };

            nobetGrupKurallar.ForEach(d => context.NobetGrupKurallar.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup görev tipler
            var nobetGrupGorevTipler = new List<NobetGrupGorevTip>()
                            {
                                new NobetGrupGorevTip(){ NobetGrupId=1, NobetGorevTipId=1},
                                new NobetGrupGorevTip(){ NobetGrupId=2, NobetGorevTipId=1},
                                new NobetGrupGorevTip(){ NobetGrupId=3, NobetGorevTipId=1}
                            };

            nobetGrupGorevTipler.ForEach(d => context.NobetGrupGorevTipler.Add(d));
            context.SaveChanges();
            #endregion

            #region nöbet grup talepler
            var nobetGrupTalepler = new List<NobetGrupTalep>()
                            {
                                new NobetGrupTalep(){ TakvimId=1, NobetGrupGorevTipId=1, NobetciSayisi=2}
                            };

            nobetGrupTalepler.ForEach(d => context.NobetGrupTalepler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczaneler

            var eczanelerAlanya = new List<Eczane>()
            {
                new Eczane { Adi = "ERENLEROBA", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ALAİYE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SİNAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÜLAY", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AKÇALIOĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÜLER", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "NAZ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AY", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "NUR", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "EYÜP", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ŞİMŞEK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AYDOĞAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ŞEKER", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÜNEYLİOĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SARE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AYNUR", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "FARUK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "İKSİR", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "HİDAYET", AcilisTarihi = DateTime.Now },


                new Eczane { Adi = "MARTI", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "DEFNE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "TEZCAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "KAMBUROĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "YÜKSEK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "NOYAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ARIKAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "KASAPOĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ŞAHİN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "KOÇAK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÖKSU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ASLI", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AKSU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ALANYA", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SELCEN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SİPAHİOĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "HAYAT", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "TOROS", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ALPER", AcilisTarihi = DateTime.Now },


                new Eczane { Adi = "SAĞLIK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ECE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ŞÜKRAN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "TUNA", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "TURUNÇ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÜLERYÜZ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "BÜKE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "BAŞAK", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "BERNA AKÇALIOĞLU", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ALTUNBAŞ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "TUĞBA", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "NİSA", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "ŞİRİN", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "AYYÜCE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "GÜNEŞ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "SEVİNDİ", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "BİLGE", AcilisTarihi = DateTime.Now },
                new Eczane { Adi = "FİLİZ", AcilisTarihi = DateTime.Now }
            };


            eczanelerAlanya.ForEach(f => context.Eczaneler.Add(f));
            context.SaveChanges();
            #endregion

            #region mazeret türler
            var v15 = new List<MazeretTur>()
                            {
                                new MazeretTur(){ Adi="Çok Önemli"},
                                new MazeretTur(){ Adi="Önemli"},
                                new MazeretTur(){ Adi="Az Önemli"}
                            };

            v15.ForEach(d => context.MazeretTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region istek türler
            var v_istek_turleri = new List<IstekTur>()
                            {
                                new IstekTur(){ Adi="Çok Önemli"},
                                new IstekTur(){ Adi="Önemli"},
                                new IstekTur(){ Adi="Az Önemli"}
                            };

            v_istek_turleri.ForEach(d => context.IstekTurler.Add(d));
            context.SaveChanges();
            #endregion

            #region mazeretler
            var v9 = new List<Mazeret>()
                            {
                                new Mazeret(){ Adi="Sağlık", MazeretTurId=1}
                            };

            v9.ForEach(d => context.Mazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region istekler
            var v_istekler = new List<Istek>()
                            {
                                new Istek(){ Adi="Sıralı Nöbet", IstekTurId=1},
                                new Istek(){ Adi="Sağlık", IstekTurId=1}

                            };

            v_istekler.ForEach(d => context.Istekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet mazeretler
            var v4 = new List<EczaneNobetMazeret>()
            {
                new EczaneNobetMazeret(){ EczaneId=1, MazeretId=1, Aciklama="Deneme", TakvimId=1 },
            };

            v4.ForEach(d => context.EczaneNobetMazeretler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet istekler

            var v1v_eczane_istek = new List<EczaneNobetIstek>()
                            {
                                new EczaneNobetIstek(){ EczaneId=1, IstekId=1, Aciklama="Sırayla", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneId=2, IstekId=1, Aciklama="Sırayla", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneId=3, IstekId=1, Aciklama="Sırayla", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneId=4, IstekId=1, Aciklama="Sırayla", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneId=5, IstekId=1, Aciklama="Sırayla", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneId=6, IstekId=1, Aciklama="Sırayla", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneId=7, IstekId=1, Aciklama="Sırayla", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneId=8, IstekId=1, Aciklama="Sırayla", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneId=9, IstekId=1, Aciklama="Sırayla", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneId=10, IstekId=1, Aciklama="Sırayla", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneId=11, IstekId=1, Aciklama="Sırayla", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneId=12, IstekId=1, Aciklama="Sırayla", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneId=13, IstekId=1, Aciklama="Sırayla", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneId=14, IstekId=1, Aciklama="Sırayla", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneId=15, IstekId=1, Aciklama="Sırayla", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneId=16, IstekId=1, Aciklama="Sırayla", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneId=17, IstekId=1, Aciklama="Sırayla", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneId=18, IstekId=1, Aciklama="Sırayla", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Sırayla", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneId=1, IstekId=1, Aciklama="Sırayla", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneId=2, IstekId=1, Aciklama="Sırayla", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneId=3, IstekId=1, Aciklama="Sırayla", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneId=4, IstekId=1, Aciklama="Sırayla", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneId=5, IstekId=1, Aciklama="Sırayla", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneId=6, IstekId=1, Aciklama="Sırayla", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneId=7, IstekId=1, Aciklama="Sırayla", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneId=8, IstekId=1, Aciklama="Sırayla", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneId=9, IstekId=1, Aciklama="Sırayla", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneId=10, IstekId=1, Aciklama="Sırayla", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneId=11, IstekId=1, Aciklama="Sırayla", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneId=12, IstekId=1, Aciklama="Sırayla", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneId=13, IstekId=1, Aciklama="Sırayla", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneId=14, IstekId=1, Aciklama="Sırayla", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneId=15, IstekId=1, Aciklama="Sırayla", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneId=16, IstekId=1, Aciklama="Sırayla", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneId=17, IstekId=1, Aciklama="Sırayla", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneId=18, IstekId=1, Aciklama="Sırayla", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Sırayla", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneId=1, IstekId=1, Aciklama="Sırayla", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneId=2, IstekId=1, Aciklama="Sırayla", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneId=3, IstekId=1, Aciklama="Sırayla", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneId=4, IstekId=1, Aciklama="Sırayla", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneId=5, IstekId=1, Aciklama="Sırayla", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneId=6, IstekId=1, Aciklama="Sırayla", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneId=7, IstekId=1, Aciklama="Sırayla", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneId=8, IstekId=1, Aciklama="Sırayla", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneId=9, IstekId=1, Aciklama="Sırayla", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneId=10, IstekId=1, Aciklama="Sırayla", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneId=11, IstekId=1, Aciklama="Sırayla", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneId=12, IstekId=1, Aciklama="Sırayla", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneId=13, IstekId=1, Aciklama="Sırayla", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneId=14, IstekId=1, Aciklama="Sırayla", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneId=15, IstekId=1, Aciklama="Sırayla", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneId=16, IstekId=1, Aciklama="Sırayla", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneId=17, IstekId=1, Aciklama="Sırayla", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneId=18, IstekId=1, Aciklama="Sırayla", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Sırayla", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneId=1, IstekId=1, Aciklama="Sırayla", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneId=2, IstekId=1, Aciklama="Sırayla", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneId=3, IstekId=1, Aciklama="Sırayla", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneId=4, IstekId=1, Aciklama="Sırayla", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneId=5, IstekId=1, Aciklama="Sırayla", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneId=6, IstekId=1, Aciklama="Sırayla", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneId=7, IstekId=1, Aciklama="Sırayla", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneId=8, IstekId=1, Aciklama="Sırayla", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneId=9, IstekId=1, Aciklama="Sırayla", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneId=10, IstekId=1, Aciklama="Sırayla", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneId=11, IstekId=1, Aciklama="Sırayla", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneId=12, IstekId=1, Aciklama="Sırayla", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneId=13, IstekId=1, Aciklama="Sırayla", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneId=14, IstekId=1, Aciklama="Sırayla", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneId=15, IstekId=1, Aciklama="Sırayla", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneId=16, IstekId=1, Aciklama="Sırayla", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneId=17, IstekId=1, Aciklama="Sırayla", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneId=18, IstekId=1, Aciklama="Sırayla", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Sırayla", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneId=1, IstekId=1, Aciklama="Sırayla", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneId=2, IstekId=1, Aciklama="Sırayla", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneId=3, IstekId=1, Aciklama="Sırayla", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneId=4, IstekId=1, Aciklama="Sırayla", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneId=5, IstekId=1, Aciklama="Sırayla", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneId=6, IstekId=1, Aciklama="Sırayla", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneId=7, IstekId=1, Aciklama="Sırayla", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneId=8, IstekId=1, Aciklama="Sırayla", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneId=9, IstekId=1, Aciklama="Sırayla", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneId=10, IstekId=1, Aciklama="Sırayla", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneId=11, IstekId=1, Aciklama="Sırayla", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneId=12, IstekId=1, Aciklama="Sırayla", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneId=13, IstekId=1, Aciklama="Sırayla", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneId=14, IstekId=1, Aciklama="Sırayla", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneId=15, IstekId=1, Aciklama="Sırayla", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneId=16, IstekId=1, Aciklama="Sırayla", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneId=17, IstekId=1, Aciklama="Sırayla", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneId=18, IstekId=1, Aciklama="Sırayla", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Sırayla", TakvimId=665},

                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneId=19, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneId=21, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneId=22, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneId=23, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneId=24, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneId=25, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneId=26, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneId=27, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneId=28, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneId=29, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneId=30, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneId=31, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneId=32, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneId=33, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneId=34, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneId=35, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneId=36, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneId=37, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneId=38, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneId=39, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneId=21, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneId=22, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneId=23, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneId=24, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneId=25, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneId=26, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneId=27, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneId=28, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneId=29, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneId=30, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneId=31, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneId=32, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneId=33, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneId=34, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneId=35, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneId=36, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneId=37, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneId=38, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneId=39, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneId=21, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneId=22, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneId=23, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneId=24, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneId=25, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneId=26, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneId=27, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneId=28, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneId=29, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneId=30, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneId=31, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneId=32, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneId=33, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneId=34, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneId=35, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneId=36, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneId=37, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneId=38, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneId=39, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneId=21, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneId=22, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneId=23, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneId=24, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneId=25, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneId=26, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneId=27, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneId=28, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneId=29, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneId=30, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneId=31, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneId=32, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneId=33, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneId=34, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneId=35, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneId=36, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneId=37, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneId=38, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneId=39, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneId=21, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneId=22, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneId=23, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneId=24, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneId=25, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneId=26, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneId=27, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneId=28, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneId=29, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneId=30, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneId=31, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneId=32, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneId=33, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneId=34, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneId=35, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneId=36, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneId=37, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneId=38, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneId=39, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneId=20, IstekId=1, Aciklama="Deneme", TakvimId=714},

                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=7},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=14},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=21},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=28},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=35},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=42},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=49},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=56},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=63},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=70},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=77},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=84},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=91},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=98},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=105},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=112},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=119},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=126},
                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=133},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=140},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=147},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=154},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=161},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=168},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=175},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=182},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=189},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=196},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=203},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=210},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=217},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=224},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=231},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=238},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=245},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=252},
                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=259},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=266},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=273},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=280},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=287},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=294},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=301},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=308},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=315},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=322},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=329},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=336},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=343},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=350},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=357},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=364},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=371},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=378},
                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=385},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=392},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=399},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=406},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=413},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=420},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=427},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=434},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=441},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=448},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=455},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=462},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=469},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=476},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=483},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=490},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=497},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=504},
                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=511},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=518},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=525},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=532},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=539},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=546},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=553},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=560},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=567},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=574},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=581},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=588},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=595},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=602},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=609},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=616},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=623},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=630},
                                new EczaneNobetIstek(){ EczaneId=40, IstekId=1, Aciklama="Deneme", TakvimId=637},
                                new EczaneNobetIstek(){ EczaneId=41, IstekId=1, Aciklama="Deneme", TakvimId=644},
                                new EczaneNobetIstek(){ EczaneId=44, IstekId=1, Aciklama="Deneme", TakvimId=651},
                                new EczaneNobetIstek(){ EczaneId=43, IstekId=1, Aciklama="Deneme", TakvimId=658},
                                new EczaneNobetIstek(){ EczaneId=42, IstekId=1, Aciklama="Deneme", TakvimId=665},
                                new EczaneNobetIstek(){ EczaneId=45, IstekId=1, Aciklama="Deneme", TakvimId=672},
                                new EczaneNobetIstek(){ EczaneId=46, IstekId=1, Aciklama="Deneme", TakvimId=679},
                                new EczaneNobetIstek(){ EczaneId=47, IstekId=1, Aciklama="Deneme", TakvimId=686},
                                new EczaneNobetIstek(){ EczaneId=48, IstekId=1, Aciklama="Deneme", TakvimId=693},
                                new EczaneNobetIstek(){ EczaneId=49, IstekId=1, Aciklama="Deneme", TakvimId=700},
                                new EczaneNobetIstek(){ EczaneId=50, IstekId=1, Aciklama="Deneme", TakvimId=707},
                                new EczaneNobetIstek(){ EczaneId=51, IstekId=1, Aciklama="Deneme", TakvimId=714},
                                new EczaneNobetIstek(){ EczaneId=52, IstekId=1, Aciklama="Deneme", TakvimId=721},
                                new EczaneNobetIstek(){ EczaneId=53, IstekId=1, Aciklama="Deneme", TakvimId=728},
                                new EczaneNobetIstek(){ EczaneId=54, IstekId=1, Aciklama="Deneme", TakvimId=735},
                                new EczaneNobetIstek(){ EczaneId=55, IstekId=1, Aciklama="Deneme", TakvimId=742},
                                new EczaneNobetIstek(){ EczaneId=56, IstekId=1, Aciklama="Deneme", TakvimId=749},
                                new EczaneNobetIstek(){ EczaneId=57, IstekId=1, Aciklama="Deneme", TakvimId=756}



                            };

            v1v_eczane_istek.ForEach(d => context.EczaneNobetIstekler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet muafiyet
            var v5 = new List<EczaneNobetMuafiyet>()
                            {
                                new EczaneNobetMuafiyet(){ EczaneId=1, BaslamaTarihi=DateTime.Today, BitisTarihi=DateTime.Today.AddDays(30), Aciklama="deneme için muaftır" }
                            };

            v5.ForEach(d => context.EczaneNobetMuafiyetler.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane nöbet gruplar

            var eczaneNobetGruplarAlanya = new List<EczaneNobetGrup>()
            {
                    new EczaneNobetGrup() { EczaneId = 1, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 2, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 3, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 4, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 5, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 6, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 7, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 8, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 9, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 10, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 11, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 12, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 13, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 14, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 15, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 16, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 17, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 18, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 19, NobetGrupId = 1, BaslangicTarihi = DateTime.Now, Aciklama = "-" },

                    new EczaneNobetGrup() { EczaneId = 20, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 21, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 22, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 23, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 24, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 25, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 26, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 27, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 28, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 29, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 30, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 31, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 32, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 33, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 34, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 35, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 36, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 37, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 38, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 39, NobetGrupId = 2, BaslangicTarihi = DateTime.Now, Aciklama = "-" },

                    new EczaneNobetGrup() { EczaneId = 40, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 41, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 42, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 43, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 44, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 45, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 46, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 47, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 48, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 49, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 50, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 51, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 52, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 53, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 54, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 55, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 56, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" },
                    new EczaneNobetGrup() { EczaneId = 57, NobetGrupId = 3, BaslangicTarihi = DateTime.Now, Aciklama = "-" }
            };


            eczaneNobetGruplarAlanya.ForEach(d => context.EczaneNobetGruplar.Add(d));
            context.SaveChanges();
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

            var v13 = new List<EczaneGrupTanim>()
                            {
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALPER_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ASLI_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÖKSU_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="KOÇAK_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_NİSA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_TUĞBA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_ŞİRİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BİLGE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_AYYÜCE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_FİLİZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TOROS_BÜKE", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="HAYAT_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SİPAHİOĞLU_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="SELCEN _SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="TEZCAN_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_SU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ŞEKER_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_SU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALAİYE_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_AKSU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_KASAPOĞLU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_ŞAHİN", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_SU", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜNEYLİOĞLU_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ALTUNBAŞ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="GÜLERYÜZ_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_SEVİNDİ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_MARTI", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALTUNBAŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_ALANYA", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜLERYÜZ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"},
                                new EczaneGrupTanim(){NobetUstGrupId = 1, Adi="ERENLER OBA_GÜNEŞ", ArdisikNobetSayisi=1, BaslangicTarihi=DateTime.Now,  Aciklama="-"}


                            };

            v13.ForEach(d => context.EczaneGrupTanimlar.Add(d));
            context.SaveChanges();
            #endregion

            #region eczane gruplama

            var v14 = new List<EczaneGrup>()
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

            v14.ForEach(d => context.EczaneGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region user eczane odalar
            var userEczaneOdalar = new List<UserEczaneOda>()
                            {
                                new UserEczaneOda(){ EczaneOdaId =1, UserId=4 }
                            };

            userEczaneOdalar.ForEach(d => context.UserEczaneOdalar.Add(d));
            context.SaveChanges();
            #endregion

            #region user nobet üst gruplar
            var userNobetUstGruplar = new List<UserNobetUstGrup>()
                            {
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=3 },
                                new UserNobetUstGrup(){  NobetUstGrupId=1, UserId=5 }
                            };

            userNobetUstGruplar.ForEach(d => context.UserNobetUstGruplar.Add(d));
            context.SaveChanges();
            #endregion

            #region user eczaneler
            var userEczaneler = new List<UserEczane>()
                            {
                                new UserEczane(){ EczaneId=1, UserId=6 }
                            };

            userEczaneler.ForEach(d => context.UserEczaneler.Add(d));
            context.SaveChanges();
            #endregion

            #region şehirler
            var vSehirler = new List<Sehir>()
                            {
                                new Sehir(){ Adi="Antalya", EczaneOdaId=1 }
                            };

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
        */
    }
}


#region bayram türleri
//var v2 = new List<BayramTuru>()
//                {
//                    new BayramTuru(){ Adi="Dini"},
//                    new BayramTuru(){ Adi="Milli"},
//                    new BayramTuru(){ Adi="Arefe"}
//                };

//v2.ForEach(d => context.BayramTurleri.Add(d));
//context.SaveChanges();
#endregion