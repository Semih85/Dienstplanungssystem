namespace WM.Northwind.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "EczaneNobet.Bayramlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TakvimId = c.Int(nullable: false),
                        NobetGorevTipId = c.Int(nullable: false),
                        NobetGunKuralId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGorevTipler", t => t.NobetGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGunKurallar", t => t.NobetGunKuralId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.TakvimId, t.NobetGorevTipId, t.NobetGunKuralId }, unique: true, name: "UN_Bayramlar");
            
            CreateTable(
                "EczaneNobet.NobetGorevTipler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.EczaneNobetSonucAktifler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneNobetGrupId = c.Int(nullable: false),
                        TakvimId = c.Int(nullable: false),
                        NobetGorevTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneNobetGruplar", t => t.EczaneNobetGrupId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGorevTipler", t => t.NobetGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.EczaneNobetGrupId, t.TakvimId, t.NobetGorevTipId }, unique: true, name: "UN_EczaneNobetSonucAktifler");
            
            CreateTable(
                "EczaneNobet.EczaneNobetGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        NobetGrupId = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                        Aciklama = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGruplar", t => t.NobetGrupId, cascadeDelete: true)
                .Index(t => t.EczaneId)
                .Index(t => t.NobetGrupId);
            
            CreateTable(
                "EczaneNobet.Eczaneler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(maxLength: 20),
                        AcilisTarihi = c.DateTime(nullable: false),
                        KapanisTarihi = c.DateTime(),
                        OdaId = c.Int(nullable: false),
                        Enlem = c.Single(nullable: false),
                        Boylam = c.Single(nullable: false),
                        Adres = c.String(maxLength: 150),
                        TelefonNo = c.String(maxLength: 10),
                        MailAdresi = c.String(maxLength: 40),
                        WebSitesi = c.String(maxLength: 30),
                        EczaneOda_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneOdalar", t => t.EczaneOda_Id)
                .Index(t => t.EczaneOda_Id);
            
            CreateTable(
                "EczaneNobet.EczaneGorevTipler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        GorevTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.GorevTipler", t => t.GorevTipId, cascadeDelete: true)
                .Index(t => t.EczaneId)
                .Index(t => t.GorevTipId);
            
            CreateTable(
                "EczaneNobet.GorevTipler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Adi, t.Aciklama }, unique: true, name: "UN_GorevTipler");
            
            CreateTable(
                "EczaneNobet.EczaneGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneGrupTanimId = c.Int(nullable: false),
                        EczaneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.EczaneGrupTanimlar", t => t.EczaneGrupTanimId, cascadeDelete: true)
                .Index(t => new { t.EczaneGrupTanimId, t.EczaneId }, unique: true, name: "UN_EczaneGruplar");
            
            CreateTable(
                "EczaneNobet.EczaneGrupTanimlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 30),
                        ArdisikNobetSayisi = c.Int(nullable: false),
                        Aciklama = c.String(nullable: false, maxLength: 100),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                        NobetUstGrupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetUstGruplar", t => t.NobetUstGrupId, cascadeDelete: true)
                .Index(t => t.Adi, unique: true, name: "UN_EczaneGrupTanimAdi")
                .Index(t => t.NobetUstGrupId);
            
            CreateTable(
                "EczaneNobet.NobetUstGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        EczaneOdaId = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                        Aciklama = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneOdalar", t => t.EczaneOdaId, cascadeDelete: true)
                .Index(t => t.EczaneOdaId);
            
            CreateTable(
                "EczaneNobet.EczaneOdalar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BayramTurId = c.String(nullable: false, maxLength: 100),
                        Adres = c.String(maxLength: 150),
                        TelefonNo = c.String(maxLength: 10),
                        MailAdresi = c.String(maxLength: 100),
                        WebSitesi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.Sehirler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        EczaneOdaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneOdalar", t => t.EczaneOdaId, cascadeDelete: true)
                .Index(t => t.EczaneOdaId);
            
            CreateTable(
                "EczaneNobet.Ilceler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        SehirId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Sehirler", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId);
            
            CreateTable(
                "EczaneNobet.EczaneIlceler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        IlceId = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                        Aciklama = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Ilceler", t => t.IlceId, cascadeDelete: true)
                .Index(t => t.EczaneId)
                .Index(t => t.IlceId);
            
            CreateTable(
                "Yetki.UserEczaneOdalar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneOdaId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneOdalar", t => t.EczaneOdaId, cascadeDelete: true)
                .ForeignKey("Yetki.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.EczaneOdaId, t.UserId }, unique: true, name: "UN_UserEczaneOdalar");
            
            CreateTable(
                "Yetki.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UN_UserName")
                .Index(t => t.Email, unique: true, name: "UN_UserEmail");
            
            CreateTable(
                "Yetki.UserEczaneler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("Yetki.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.EczaneId, t.UserId }, unique: true, name: "UN_UserEczaneler");
            
            CreateTable(
                "Yetki.UserNobetUstGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NobetUstGrupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetUstGruplar", t => t.NobetUstGrupId, cascadeDelete: true)
                .ForeignKey("Yetki.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.NobetUstGrupId, t.UserId }, unique: true, name: "UN_UserNobetUstGruplar");
            
            CreateTable(
                "Yetki.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Yetki.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Yetki.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.RoleId, t.UserId }, unique: true, name: "UN_UserRoles");
            
            CreateTable(
                "Yetki.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Yetki.MenuAltRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuAltId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Yetki.MenuAltlar", t => t.MenuAltId, cascadeDelete: true)
                .ForeignKey("Yetki.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => new { t.MenuAltId, t.RoleId }, unique: true, name: "UN_MenuAltRoles");
            
            CreateTable(
                "Yetki.MenuAltlar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        LinkText = c.String(nullable: false, maxLength: 50),
                        ActionName = c.String(),
                        ControllerName = c.String(),
                        AreaName = c.String(),
                        SpanCssClass = c.String(),
                        PasifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Yetki.Menuler", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId)
                .Index(t => t.LinkText, unique: true, name: "UN_MenuAltlar");
            
            CreateTable(
                "Yetki.Menuler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkText = c.String(nullable: false, maxLength: 50),
                        ActionName = c.String(),
                        ControllerName = c.String(),
                        AreaName = c.String(),
                        SpanCssClass = c.String(),
                        PasifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LinkText, unique: true, name: "UN_Menuler");
            
            CreateTable(
                "Yetki.MenuRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Yetki.Menuler", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("Yetki.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => new { t.MenuId, t.RoleId }, unique: true, name: "UN_MenuRoles");
            
            CreateTable(
                "EczaneNobet.NobetGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 70),
                        NobetUstGrupId = c.Int(nullable: false),
                        BaslamaTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetUstGruplar", t => t.NobetUstGrupId, cascadeDelete: true)
                .Index(t => t.NobetUstGrupId);
            
            CreateTable(
                "EczaneNobet.NobetAltGruplar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 70),
                        NobetGrupId = c.Int(nullable: false),
                        BaslamaTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGruplar", t => t.NobetGrupId, cascadeDelete: true)
                .Index(t => new { t.Adi, t.NobetGrupId }, unique: true, name: "UN_NobetAltGruplar");
            
            CreateTable(
                "EczaneNobet.NobetGrupGorevTipler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NobetGrupId = c.Int(nullable: false),
                        NobetGorevTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGorevTipler", t => t.NobetGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGruplar", t => t.NobetGrupId, cascadeDelete: true)
                .Index(t => new { t.NobetGrupId, t.NobetGorevTipId }, unique: true, name: "UN_NobetGrupGorevTipler");
            
            CreateTable(
                "EczaneNobet.NobetGrupTalepler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TakvimId = c.Int(nullable: false),
                        NobetGrupGorevTipId = c.Int(nullable: false),
                        NobetciSayisi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGrupGorevTipler", t => t.NobetGrupGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.TakvimId, t.NobetciSayisi }, unique: true, name: "UN_NobetGrupTalepler")
                .Index(t => t.NobetGrupGorevTipId);
            
            CreateTable(
                "EczaneNobet.Takvimler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tarih = c.DateTime(nullable: false),
                        Aciklama = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Tarih, unique: true, name: "UN_Tarih");
            
            CreateTable(
                "EczaneNobet.EczaneNobetIstekler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        IstekId = c.Int(nullable: false),
                        TakvimId = c.Int(nullable: false),
                        Aciklama = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Istekler", t => t.IstekId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.EczaneId, t.TakvimId, t.IstekId }, unique: true, name: "UN_EczaneNobetIstekler");
            
            CreateTable(
                "EczaneNobet.Istekler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 100),
                        IstekTurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.IstekTurler", t => t.IstekTurId, cascadeDelete: true)
                .Index(t => t.IstekTurId);
            
            CreateTable(
                "EczaneNobet.IstekTurler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 30),
                        Aciklama = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.EczaneNobetMazeretler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        MazeretId = c.Int(nullable: false),
                        TakvimId = c.Int(nullable: false),
                        Aciklama = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Mazeretler", t => t.MazeretId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.EczaneId, t.TakvimId, t.MazeretId }, unique: true, name: "UN_EczaneNobetMazeretler");
            
            CreateTable(
                "EczaneNobet.Mazeretler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 100),
                        MazeretTurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.MazeretTurler", t => t.MazeretTurId, cascadeDelete: true)
                .Index(t => t.MazeretTurId);
            
            CreateTable(
                "EczaneNobet.MazeretTurler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 30),
                        Aciklama = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.EczaneNobetSonucDemolar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneNobetGrupId = c.Int(nullable: false),
                        TakvimId = c.Int(nullable: false),
                        NobetGorevTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneNobetGruplar", t => t.EczaneNobetGrupId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGorevTipler", t => t.NobetGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.EczaneNobetGrupId, t.TakvimId, t.NobetGorevTipId }, unique: true, name: "UN_EczaneNobetSonucDemolar");
            
            CreateTable(
                "EczaneNobet.EczaneNobetSonuclar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneNobetGrupId = c.Int(nullable: false),
                        TakvimId = c.Int(nullable: false),
                        NobetGorevTipId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.EczaneNobetGruplar", t => t.EczaneNobetGrupId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGorevTipler", t => t.NobetGorevTipId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.Takvimler", t => t.TakvimId, cascadeDelete: true)
                .Index(t => new { t.EczaneNobetGrupId, t.TakvimId, t.NobetGorevTipId }, unique: true, name: "UN_EczaneNobetSonuclar");
            
            CreateTable(
                "EczaneNobet.NobetGrupGunKurallar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NobetGrupId = c.Int(nullable: false),
                        NobetGunKuralId = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGruplar", t => t.NobetGrupId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetGunKurallar", t => t.NobetGunKuralId, cascadeDelete: true)
                .Index(t => new { t.NobetGrupId, t.NobetGunKuralId }, unique: true, name: "UN_NobetGrupKurallar");
            
            CreateTable(
                "EczaneNobet.NobetGunKurallar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Adi, t.Aciklama }, unique: true, name: "UN_NobetGunKurallar");
            
            CreateTable(
                "EczaneNobet.NobetGrupKurallar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NobetGrupId = c.Int(nullable: false),
                        NobetKuralId = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(),
                        Deger = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.NobetGruplar", t => t.NobetGrupId, cascadeDelete: true)
                .ForeignKey("EczaneNobet.NobetKurallar", t => t.NobetKuralId, cascadeDelete: true)
                .Index(t => t.NobetGrupId)
                .Index(t => t.NobetKuralId);
            
            CreateTable(
                "EczaneNobet.NobetKurallar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.EczaneNobetMuafiyetler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EczaneId = c.Int(nullable: false),
                        BaslamaTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(nullable: false),
                        Aciklama = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EczaneNobet.Eczaneler", t => t.EczaneId, cascadeDelete: true)
                .Index(t => t.EczaneId);
            
            CreateTable(
                "EczaneNobet.GunDegerler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EczaneNobet.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Detail = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Audit = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("EczaneNobet.Bayramlar", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.Bayramlar", "NobetGunKuralId", "EczaneNobet.NobetGunKurallar");
            DropForeignKey("EczaneNobet.Bayramlar", "NobetGorevTipId", "EczaneNobet.NobetGorevTipler");
            DropForeignKey("EczaneNobet.EczaneNobetSonucAktifler", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetSonucAktifler", "NobetGorevTipId", "EczaneNobet.NobetGorevTipler");
            DropForeignKey("EczaneNobet.EczaneNobetSonucAktifler", "EczaneNobetGrupId", "EczaneNobet.EczaneNobetGruplar");
            DropForeignKey("EczaneNobet.EczaneNobetGruplar", "NobetGrupId", "EczaneNobet.NobetGruplar");
            DropForeignKey("EczaneNobet.EczaneNobetGruplar", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.Eczaneler", "EczaneOda_Id", "EczaneNobet.EczaneOdalar");
            DropForeignKey("EczaneNobet.EczaneNobetMuafiyetler", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.EczaneGruplar", "EczaneGrupTanimId", "EczaneNobet.EczaneGrupTanimlar");
            DropForeignKey("EczaneNobet.EczaneGrupTanimlar", "NobetUstGrupId", "EczaneNobet.NobetUstGruplar");
            DropForeignKey("EczaneNobet.NobetGruplar", "NobetUstGrupId", "EczaneNobet.NobetUstGruplar");
            DropForeignKey("EczaneNobet.NobetGrupKurallar", "NobetKuralId", "EczaneNobet.NobetKurallar");
            DropForeignKey("EczaneNobet.NobetGrupKurallar", "NobetGrupId", "EczaneNobet.NobetGruplar");
            DropForeignKey("EczaneNobet.NobetGrupGunKurallar", "NobetGunKuralId", "EczaneNobet.NobetGunKurallar");
            DropForeignKey("EczaneNobet.NobetGrupGunKurallar", "NobetGrupId", "EczaneNobet.NobetGruplar");
            DropForeignKey("EczaneNobet.NobetGrupTalepler", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetSonuclar", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetSonuclar", "NobetGorevTipId", "EczaneNobet.NobetGorevTipler");
            DropForeignKey("EczaneNobet.EczaneNobetSonuclar", "EczaneNobetGrupId", "EczaneNobet.EczaneNobetGruplar");
            DropForeignKey("EczaneNobet.EczaneNobetSonucDemolar", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetSonucDemolar", "NobetGorevTipId", "EczaneNobet.NobetGorevTipler");
            DropForeignKey("EczaneNobet.EczaneNobetSonucDemolar", "EczaneNobetGrupId", "EczaneNobet.EczaneNobetGruplar");
            DropForeignKey("EczaneNobet.EczaneNobetMazeretler", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetMazeretler", "MazeretId", "EczaneNobet.Mazeretler");
            DropForeignKey("EczaneNobet.Mazeretler", "MazeretTurId", "EczaneNobet.MazeretTurler");
            DropForeignKey("EczaneNobet.EczaneNobetMazeretler", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.EczaneNobetIstekler", "TakvimId", "EczaneNobet.Takvimler");
            DropForeignKey("EczaneNobet.EczaneNobetIstekler", "IstekId", "EczaneNobet.Istekler");
            DropForeignKey("EczaneNobet.Istekler", "IstekTurId", "EczaneNobet.IstekTurler");
            DropForeignKey("EczaneNobet.EczaneNobetIstekler", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.NobetGrupTalepler", "NobetGrupGorevTipId", "EczaneNobet.NobetGrupGorevTipler");
            DropForeignKey("EczaneNobet.NobetGrupGorevTipler", "NobetGrupId", "EczaneNobet.NobetGruplar");
            DropForeignKey("EczaneNobet.NobetGrupGorevTipler", "NobetGorevTipId", "EczaneNobet.NobetGorevTipler");
            DropForeignKey("EczaneNobet.NobetAltGruplar", "NobetGrupId", "EczaneNobet.NobetGruplar");
            DropForeignKey("EczaneNobet.NobetUstGruplar", "EczaneOdaId", "EczaneNobet.EczaneOdalar");
            DropForeignKey("Yetki.UserEczaneOdalar", "UserId", "Yetki.Users");
            DropForeignKey("Yetki.UserRoles", "UserId", "Yetki.Users");
            DropForeignKey("Yetki.UserRoles", "RoleId", "Yetki.Roles");
            DropForeignKey("Yetki.MenuRoles", "RoleId", "Yetki.Roles");
            DropForeignKey("Yetki.MenuRoles", "MenuId", "Yetki.Menuler");
            DropForeignKey("Yetki.MenuAltRoles", "RoleId", "Yetki.Roles");
            DropForeignKey("Yetki.MenuAltRoles", "MenuAltId", "Yetki.MenuAltlar");
            DropForeignKey("Yetki.MenuAltlar", "MenuId", "Yetki.Menuler");
            DropForeignKey("Yetki.UserNobetUstGruplar", "UserId", "Yetki.Users");
            DropForeignKey("Yetki.UserNobetUstGruplar", "NobetUstGrupId", "EczaneNobet.NobetUstGruplar");
            DropForeignKey("Yetki.UserEczaneler", "UserId", "Yetki.Users");
            DropForeignKey("Yetki.UserEczaneler", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("Yetki.UserEczaneOdalar", "EczaneOdaId", "EczaneNobet.EczaneOdalar");
            DropForeignKey("EczaneNobet.Ilceler", "SehirId", "EczaneNobet.Sehirler");
            DropForeignKey("EczaneNobet.EczaneIlceler", "IlceId", "EczaneNobet.Ilceler");
            DropForeignKey("EczaneNobet.EczaneIlceler", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.Sehirler", "EczaneOdaId", "EczaneNobet.EczaneOdalar");
            DropForeignKey("EczaneNobet.EczaneGruplar", "EczaneId", "EczaneNobet.Eczaneler");
            DropForeignKey("EczaneNobet.EczaneGorevTipler", "GorevTipId", "EczaneNobet.GorevTipler");
            DropForeignKey("EczaneNobet.EczaneGorevTipler", "EczaneId", "EczaneNobet.Eczaneler");
            DropIndex("EczaneNobet.EczaneNobetMuafiyetler", new[] { "EczaneId" });
            DropIndex("EczaneNobet.NobetGrupKurallar", new[] { "NobetKuralId" });
            DropIndex("EczaneNobet.NobetGrupKurallar", new[] { "NobetGrupId" });
            DropIndex("EczaneNobet.NobetGunKurallar", "UN_NobetGunKurallar");
            DropIndex("EczaneNobet.NobetGrupGunKurallar", "UN_NobetGrupKurallar");
            DropIndex("EczaneNobet.EczaneNobetSonuclar", "UN_EczaneNobetSonuclar");
            DropIndex("EczaneNobet.EczaneNobetSonucDemolar", "UN_EczaneNobetSonucDemolar");
            DropIndex("EczaneNobet.Mazeretler", new[] { "MazeretTurId" });
            DropIndex("EczaneNobet.EczaneNobetMazeretler", "UN_EczaneNobetMazeretler");
            DropIndex("EczaneNobet.Istekler", new[] { "IstekTurId" });
            DropIndex("EczaneNobet.EczaneNobetIstekler", "UN_EczaneNobetIstekler");
            DropIndex("EczaneNobet.Takvimler", "UN_Tarih");
            DropIndex("EczaneNobet.NobetGrupTalepler", new[] { "NobetGrupGorevTipId" });
            DropIndex("EczaneNobet.NobetGrupTalepler", "UN_NobetGrupTalepler");
            DropIndex("EczaneNobet.NobetGrupGorevTipler", "UN_NobetGrupGorevTipler");
            DropIndex("EczaneNobet.NobetAltGruplar", "UN_NobetAltGruplar");
            DropIndex("EczaneNobet.NobetGruplar", new[] { "NobetUstGrupId" });
            DropIndex("Yetki.MenuRoles", "UN_MenuRoles");
            DropIndex("Yetki.Menuler", "UN_Menuler");
            DropIndex("Yetki.MenuAltlar", "UN_MenuAltlar");
            DropIndex("Yetki.MenuAltlar", new[] { "MenuId" });
            DropIndex("Yetki.MenuAltRoles", "UN_MenuAltRoles");
            DropIndex("Yetki.UserRoles", "UN_UserRoles");
            DropIndex("Yetki.UserNobetUstGruplar", "UN_UserNobetUstGruplar");
            DropIndex("Yetki.UserEczaneler", "UN_UserEczaneler");
            DropIndex("Yetki.Users", "UN_UserEmail");
            DropIndex("Yetki.Users", "UN_UserName");
            DropIndex("Yetki.UserEczaneOdalar", "UN_UserEczaneOdalar");
            DropIndex("EczaneNobet.EczaneIlceler", new[] { "IlceId" });
            DropIndex("EczaneNobet.EczaneIlceler", new[] { "EczaneId" });
            DropIndex("EczaneNobet.Ilceler", new[] { "SehirId" });
            DropIndex("EczaneNobet.Sehirler", new[] { "EczaneOdaId" });
            DropIndex("EczaneNobet.NobetUstGruplar", new[] { "EczaneOdaId" });
            DropIndex("EczaneNobet.EczaneGrupTanimlar", new[] { "NobetUstGrupId" });
            DropIndex("EczaneNobet.EczaneGrupTanimlar", "UN_EczaneGrupTanimAdi");
            DropIndex("EczaneNobet.EczaneGruplar", "UN_EczaneGruplar");
            DropIndex("EczaneNobet.GorevTipler", "UN_GorevTipler");
            DropIndex("EczaneNobet.EczaneGorevTipler", new[] { "GorevTipId" });
            DropIndex("EczaneNobet.EczaneGorevTipler", new[] { "EczaneId" });
            DropIndex("EczaneNobet.Eczaneler", new[] { "EczaneOda_Id" });
            DropIndex("EczaneNobet.EczaneNobetGruplar", new[] { "NobetGrupId" });
            DropIndex("EczaneNobet.EczaneNobetGruplar", new[] { "EczaneId" });
            DropIndex("EczaneNobet.EczaneNobetSonucAktifler", "UN_EczaneNobetSonucAktifler");
            DropIndex("EczaneNobet.Bayramlar", "UN_Bayramlar");
            DropTable("EczaneNobet.Logs");
            DropTable("EczaneNobet.GunDegerler");
            DropTable("EczaneNobet.EczaneNobetMuafiyetler");
            DropTable("EczaneNobet.NobetKurallar");
            DropTable("EczaneNobet.NobetGrupKurallar");
            DropTable("EczaneNobet.NobetGunKurallar");
            DropTable("EczaneNobet.NobetGrupGunKurallar");
            DropTable("EczaneNobet.EczaneNobetSonuclar");
            DropTable("EczaneNobet.EczaneNobetSonucDemolar");
            DropTable("EczaneNobet.MazeretTurler");
            DropTable("EczaneNobet.Mazeretler");
            DropTable("EczaneNobet.EczaneNobetMazeretler");
            DropTable("EczaneNobet.IstekTurler");
            DropTable("EczaneNobet.Istekler");
            DropTable("EczaneNobet.EczaneNobetIstekler");
            DropTable("EczaneNobet.Takvimler");
            DropTable("EczaneNobet.NobetGrupTalepler");
            DropTable("EczaneNobet.NobetGrupGorevTipler");
            DropTable("EczaneNobet.NobetAltGruplar");
            DropTable("EczaneNobet.NobetGruplar");
            DropTable("Yetki.MenuRoles");
            DropTable("Yetki.Menuler");
            DropTable("Yetki.MenuAltlar");
            DropTable("Yetki.MenuAltRoles");
            DropTable("Yetki.Roles");
            DropTable("Yetki.UserRoles");
            DropTable("Yetki.UserNobetUstGruplar");
            DropTable("Yetki.UserEczaneler");
            DropTable("Yetki.Users");
            DropTable("Yetki.UserEczaneOdalar");
            DropTable("EczaneNobet.EczaneIlceler");
            DropTable("EczaneNobet.Ilceler");
            DropTable("EczaneNobet.Sehirler");
            DropTable("EczaneNobet.EczaneOdalar");
            DropTable("EczaneNobet.NobetUstGruplar");
            DropTable("EczaneNobet.EczaneGrupTanimlar");
            DropTable("EczaneNobet.EczaneGruplar");
            DropTable("EczaneNobet.GorevTipler");
            DropTable("EczaneNobet.EczaneGorevTipler");
            DropTable("EczaneNobet.Eczaneler");
            DropTable("EczaneNobet.EczaneNobetGruplar");
            DropTable("EczaneNobet.EczaneNobetSonucAktifler");
            DropTable("EczaneNobet.NobetGorevTipler");
            DropTable("EczaneNobet.Bayramlar");
        }
    }
}
