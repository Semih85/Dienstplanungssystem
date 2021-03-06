﻿//using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WM.Core.CrossCuttingConcerns.Security.Web;
using WM.UI.Mvc.Models;
using WebMatrix.WebData;
using WM.Northwind.Business.Abstract.Authorization;
using System.Net.Mail;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Entities.Concrete.Authorization;
using System.Text.RegularExpressions;
using System.Text;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.UI.Mvc.Services;
using System.Security.Cryptography;
using System.Data.Entity.Infrastructure;
using System.Net;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IEczaneService _eczaneService;
        private IUserEczaneService _userEczaneService;
        private IUserRoleService _userRoleService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IUserEczaneOdaService _userEczaneOdaService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public AccountController(IUserService userService,
            IUserRoleService userRoleService,
            IEczaneService eczaneService,
            IUserEczaneService userEczaneService,
            IUserNobetUstGrupService userNobetUstGrupService,
            IUserEczaneOdaService userEczaneOdaService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _userService = userService;
            _eczaneService = eczaneService;
            _userRoleService = userRoleService;
            _userEczaneService = userEczaneService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _userEczaneOdaService = userEczaneOdaService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var userNobetUstGruplar = _userNobetUstGrupService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList());

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var kullanicilar = new List<User>();
            var nobetUstGrupIdler = new List<int>();
            var kullaniciIDler = new List<int>();

            if (rolId == 1)
            {//Admin için tüm kullanıclar
                kullanicilar = _userService.GetList()
                   .OrderBy(o => o.UserName)
                   .ToList();
            }
            else if (rolId == 2 || rolId == 3)
            {//Oda ve üst grup yetkilisi için seçili nöbet üst grup kullanıcıları gelecek
                kullaniciIDler = _userNobetUstGrupService.GetListByNobetUstGrupId(ustGrupSession.Id)
                    .OrderBy(o => o.KullaniciAdi)
                    .Select(s => s.UserId)
                    .ToList();
                foreach (var item in kullaniciIDler)
                {
                    kullanicilar.Add(_userService.GetById(item));
                }
            }



            return View(kullanicilar);
        }

        // GET: EczaneNobet/Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                Regex regex = new Regex("^/(?<Controller>[^/]*)(/(?<Action>[^/]*)(/(?<id>[^?]*)(/?(?<QueryString>.*))?)?)?$");
                Match match = regex.Match(returnUrl);

                // match.Groups["Controller"].Value is the controller, 
                // match.Groups["Action"].Value is the action,
                // match.Groups["id"].Value is the id
                // match.Groups["QueryString"].Value are the other parameters
            }

            return User.Identity.IsAuthenticated
                    ? View("Unauthorized")
                    : View("Login", new LoginViewModel());
        }

        // GET: Account
        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                login.LoginItem.Password = SHA256(login.LoginItem.Password);

                var user = _userService.GetByEMailAndPassword(login.LoginItem);

                //Session["nobetUstGrupId"] = 1;

                if (user != null)
                {
                    var simdi = DateTime.UtcNow;

                    var remember = login.LoginItem.RememberMe;

                    var utcExpires = remember
                        ? simdi.AddDays(45)
                        : simdi.AddHours(8);

                    //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
                    var rolIdler = _userRoleService.GetDetayListByUserId(user.Id).Select(s => s.RoleId).ToList();
                    //var roller = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleName).ToArray();
                    var roller = _userRoleService.GetDetayListByUserId(user.Id).OrderBy(s => s.RoleId).Select(u => u.RoleName).ToArray();

                    if (rolIdler.Any())
                    {
                        AuthenticationHelper
                            .CreateAuthCookie(
                                new Guid(),
                                user.UserName,
                                user.Email,
                                utcExpires,
                                roller,
                                login.LoginItem.RememberMe,
                                user.FirstName,
                                user.LastName);
                    }

                    var url = RedirectToAction("Index", "NobetYaz", new { area = "EczaneNobet" });

                    var rolId = rolIdler.FirstOrDefault();

                    switch (rolId)
                    {
                        case 1:
                            url = RedirectToAction("Index", "Admin", new { area = "" });
                            break;
                        case 2:
                            //url = RedirectToAction("Index", "OdaYonetim", new { area = "EczaneNobet", userId = user.Id });
                            break;
                        case 3:
                            //url = RedirectToAction("Index", "NobetUstGrupYonetim", new { area = "EczaneNobet", userId = user.Id });
                            break;
                        case 4:
                            url = RedirectToAction("Index", "Eczane", new { area = "EczaneNobet" });
                            break;
                        //case 5://misafir
                        //    url = RedirectToAction("Index", "EczaneKullanici", new { area = "EczaneNobet", userId = user.Id });
                        //    break;
                        case 6:
                            url = RedirectToAction("PivotSonuclar", "EczaneNobetSonuc", new { area = "EczaneNobet" });
                            break;
                        default:
                            url = RedirectToAction("Unauthorized", "Account", new { area = "" });
                            break;
                    }

                    return returnUrl == null ? url : (ActionResult)Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "E-Posta ya da şifre hatalı !!!");
                    //ViewBag.Warn = "E-Posta ya da şifre hatalı !!! ";
                    return View(user);
                }
            }
            else
            {
                ViewBag.FormClass = "form-control is-invalid";
                return View(login);
            }

        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Register(RegisterViewModel model)
        {
            //if (ReCaptcha.Validate("6LfnxisUAAAAAHvALIZD4c_ai1fu41L8HjCQ0-00"))
            //{
            //var token = "";
            try
            {//son true kullanıcıya doğrulama maili göndermek için
             // token = WebSecurity.CreateUserAndAccount(Model.User.EMail, Model.User.Password, new { First_name = Model.User.FirstName, Last_name = Model.User.LastName }, true);

                //foreach (var user in _userService.GetList())
                //{
                //    user.Password = SHA256(user.Password);
                //    user.Email = user.Email.Trim();

                //    _userService.Update(user);
                //}

                model.User.Password = SHA256(model.Password);
                model.User.UserName = model.User.Email;

                _userService.Insert(model.User);

                // Roles.AddUserToRole(Model.User.Email, "Members");
                var subject = "Nöbet Yaz kayıt";

                var body =
                    $"<p>" +
                        $"Merhaba, Sayın {model.User.FirstName} {model.User.LastName.ToUpper()}." +
                    $"</p>" +
                    $"<p>" +
                        $"Bu mesaj, <b>nobetyaz.com</b> sitesine yapmış olduğunuz üyelik hakkında bilgilendirme amacıyla gönderilmiştir. " +
                        $"<br/>" +
                        $"<strong>Siteye giriş yapmak için lütfen aşağıdaki linki tıklayınız.</strong>" +
                    $"</p>" +
                    $"<a class='mb-2' href='https://nobetyaz.com' title='Nöbet Yaz'>Nöbet Yaz</a> <span>kullanıcı bilgileriniz:</span>" +
                    $"<table>" +
                        $"<tr>" +
                            $"<td>" +
                                $"<b>Kullanıcı Adı</b>" +
                            $"</td>" +
                             $"<td>" +
                                $":" +
                            $"</td>" +
                            $"<td>" +
                                $"{model.User.UserName}" +
                            $"</td>" +
                        $"</tr>" +
                        $"<tr>" +
                            $"<td>" +
                                $"<b>Parola</b>" +
                            $"</td>" +
                            $"<td>" +
                                $":" +
                            $"</td>" +
                            $"<td>" +
                                $"{model.User.Password}" +
                            $"</td>" +
                        $"</tr>" +
                        $"<tr>" +
                            $"<td>" +
                                $"<b>Başlama Tarihi</b>" +
                            $"</td>" +
                            $"<td>" +
                                $":" +
                            $"</td>" +
                            $"<td>" +
                                $"{model.User.BaslamaTarihi}" +
                            $"</td>" +
                        $"</tr>" +
                    $"</table>"
                    ;

                var toEmail = "ozdamar85@gmail.com";//;Model.User.Email;

                //SendMail(subject, body, toEmail);

                TempData["KayitSonuc"] = "Kayıt başarılı.";
            }
            catch (DbUpdateException ex)
            {
                var hata = ex.InnerException.ToString();

                string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                if (dublicateRowHatasiMi)
                {
                    throw new Exception("Mükerrer kayıt eklenemez... <strong>(Mükerrer kayıt !)</strong>", ex);
                }

                throw ex;
            }
            catch (Exception e)
            {
                TempData["Message"] = "Bu email ile kayıt zaten yapılmış!";

                return View("Error", e);
            }

            return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["Message"] = "ReCaptcha yanlış!";

            //    return View("Fail");
            //}
        }
        [AllowAnonymous]
        public ActionResult RegisterEczaciMobilKullanici()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //if (ustGrupSession.Id != 0)
            //{
            //    nobetUstGrup = ustGrupSession;
            //}
            var mevcutEczaneler = _userEczaneService.GetList()
                .Select(s => s.EczaneId);
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id)
                .Where(w => !mevcutEczaneler.Contains(w.Id));
            eczaneler = eczaneler.OrderBy(o => o.EczaneAdi);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi");
            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult RegisterEczaciMobilKullanici(RegisterViewModelMobil model)
        {
            //if (ReCaptcha.Validate("6LfnxisUAAAAAHvALIZD4c_ai1fu41L8HjCQ0-00"))
            //{
            //var token = "";
            //try
            //{
                //son true kullanıcıya doğrulama maili göndermek için
                // token = WebSecurity.CreateUserAndAccount(Model.User.EMail, Model.User.Password, new { First_name = Model.User.FirstName, Last_name = Model.User.LastName }, true);

                //foreach (var user in _userService.GetList())
                //{
                //    user.Password = SHA256(user.Password);
                //    user.Email = user.Email.Trim();

                //    _userService.Update(user);
                //}
                #region kayıt
                string parola = "";
                Random rnd = new Random();
                //User user = new User();
                User user = _userService.GetByEmail(model.User.Email);
                bool emailGitsinMi = false;
                bool yeniKullaniciMi = false;

                if (user != null)
                {// eğer kullanıcı daha önceden akyıt olmuşsa sadece userEczane ye seçilen EczaneId ile 
                 //ve userRole de eczane rolü ile kaydı yapılacak, else den sonra yazıldı ortak olduğu için.

                    TempData["KayitSonuc"] = "Eski kullanıcı";
                }
                else
                {
                    emailGitsinMi = true;
                    yeniKullaniciMi = true;
                    user = model.User;
                    parola = rnd.Next(111111, 999999).ToString();
                    model.User.Password = SHA256(parola);
                    model.User.UserName = model.User.Email;
                    model.User.BaslamaTarihi = model.BaslamaTarihi;
                    model.User.BitisTarihi = model.BitisTarihi;
                    _userService.Insert(model.User);

                    #region userNobetUstGrup
                    UserNobetUstGrup userNobetUstGrup = new UserNobetUstGrup();
                    userNobetUstGrup.BaslamaTarihi = model.BaslamaTarihi;
                    userNobetUstGrup.BitisTarihi = model.BitisTarihi;
                    var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
                    userNobetUstGrup.NobetUstGrupId = ustGrupSession.Id;
                    userNobetUstGrup.UserId = model.User.Id;

                    _userNobetUstGrupService.Insert(userNobetUstGrup);
                    #endregion

                    #region userEczaneOda
                    UserEczaneOda userEczaneOda = new UserEczaneOda();
                    NobetUstGrup nobetUstGrup = new NobetUstGrup();
                    userEczaneOda.BaslamaTarihi = model.BaslamaTarihi;
                    userEczaneOda.BitisTarihi = model.BitisTarihi;
                    userEczaneOda.EczaneOdaId = ustGrupSession.EczaneOdaId;
                    userEczaneOda.UserId = model.User.Id;

                    _userEczaneOdaService.Insert(userEczaneOda);

                    TempData["KayitSonuc"] = "Yeni kullanıcı";

                    #endregion
                }

                #region userRole
                int eczaciRol = _userRoleService.GetListByUserId(user.Id).Where(w => w.RoleId == 4).Count();
                if (eczaciRol == 0)
                {
                    emailGitsinMi = true;
                    UserRole userRole = new UserRole();
                    userRole.UserId = user.Id;
                    userRole.BaslamaTarihi = model.BaslamaTarihi;
                    userRole.BitisTarihi = model.BitisTarihi;
                    userRole.RoleId = 4;

                    _userRoleService.Insert(userRole);

                    TempData["KayitSonuc"] = TempData["KayitSonuc"] + ", Eczacı rolü kaydı başarılı";

                }
                else
                {//eczacı rolü var önceden
                    TempData["KayitSonuc"] = TempData["KayitSonuc"] + ", Eczacı rolü kaydı daha önce tanımlanmış";

                }
                #endregion

                string eczaneAdi = "";

                #region userEczane
                int userEczaci = _userEczaneService.GetListByUserId(user.Id).Select(s => s.Id).Count();
                if (userEczaci == 0)
                {
                    emailGitsinMi = true;
                    UserEczane userEczane = new UserEczane();
                    userEczane.BaslamaTarihi = model.BaslamaTarihi;
                    userEczane.BitisTarihi = model.BitisTarihi;
                    userEczane.EczaneId = model.EczaneId;
                    userEczane.UserId = user.Id;

                    _userEczaneService.Insert(userEczane);

                    eczaneAdi = _eczaneService.GetById(model.EczaneId).Adi;

                    TempData["KayitSonuc"] = TempData["KayitSonuc"] + ", Eczane kullanıcıya tanımlandı. ";
                }
                else
                {//daha önce eczane tanımlanmış
                    int eczaneId = _userEczaneService.GetListByUserId(user.Id).Select(s => s.EczaneId).FirstOrDefault();
                    Eczane eczane = _eczaneService.GetById(eczaneId);
                    eczaneAdi = eczane.Adi;

                    TempData["KayitSonuc"] = TempData["KayitSonuc"] + ", Eczane kullanıcıya daha önce tanımlanmış. ";

                }

                if (emailGitsinMi)
                {//email gönder
                    #region email
                    // Roles.AddUserToRole(Model.User.Email, "Members");
                    var subject = "NöbetYaz Mobil Uygulama Kayıt";
                    if (!yeniKullaniciMi)
                    {
                        parola = "Mevcut parolanız.";
                    }
                    var body = "Merhaba, Sayın " + user.FirstName + " " + user.LastName
                    + System.Environment.NewLine +
                        "Bu mesaj, eczacı odanızın " + eczaneAdi + " ECZANESİ adına Nöbetyaz mobil uygulamasına yapmış olduğu kayıt hakkında sizi bilgilendirme amacıyla gönderilmiştir. "
                    + System.Environment.NewLine +
                           "Google (https://play.google.com/store/apps/details?id=com.nobetyax.android)"
                    + System.Environment.NewLine +
                           "veya"
                    + System.Environment.NewLine +
                           "Apple mağazalarında Nöbetyaz uygulamasını ücretsiz indirip giriş yapabilirsiniz."
                    + System.Environment.NewLine +
                         "Nöbetyaz uygulaması sayesinde eczacı odanızdan güncel duyurular ve nöbetler ile ilgili bildirimler alabilirsiniz."
                    + System.Environment.NewLine +
                         "Kullanıcı bilgileriniz:"
                    + System.Environment.NewLine +
                        "Kullanıcı Adınız: " + user.Email
                    + System.Environment.NewLine +
                            "Parolanız: " + parola;

                    var toEmail = user.Email;
                    SendMail(subject, body, toEmail);


                    TempData["KayitSonuc"] = TempData["KayitSonuc"] + ",  Mail gönderildi. ";

                    #endregion
                }
                #endregion

                #endregion


            //}
            //catch (DbUpdateException ex)
            //{
            //    var hata = ex.InnerException.ToString();

            //    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

            //    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

            //    if (dublicateRowHatasiMi)
            //    {
            //        throw new Exception("Mükerrer kayıt eklenemez... <strong>(Mükerrer kayıt !)</strong>", ex);
            //    }

            //    throw ex;
            //}
            //catch (Exception e)
            //{
            //    TempData["Message"] = "Bu email ile kayıt zaten yapılmış!";

            //    return View("Error", e);
            //}

            // return RedirectToAction("Index");
            return RedirectToAction("Index", "UserEczane", new { area = "EczaneNobet" });
            //return View();

            //}
            //else
            //{
            //    TempData["Message"] = "ReCaptcha yanlış!";

            //    return View("Fail");
            //}
        }
        public ActionResult UserEczaneListeyeGit()
        {
            return RedirectToAction("Index", "UserEczane", new { area = "EczaneNobet" });
        }
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetByUserName(userName);

            if (user == null)
            {
                return HttpNotFound();
            }

            var editUser = new EditUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                //Password = SHA256(user.Password),
                BaslamaTarihi = user.BaslamaTarihi,
                BitisTarihi = user.BitisTarihi,
                UserName = user.UserName,
                Email = user.Email,
                Id = user.Id
            };

            //var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            //ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(
            [Bind(Include = "Id,Email,FirstName,LastName,UserName,Password,PasswordLast,PasswordVerify,BaslamaTarihi,BitisTarihi,ParolaDegistir,kullaniciAdiMevcut")]
            EditUser editUser, string kullaniciAdiMevcut)
        {
            var kullanici = _userService.GetByUserName(kullaniciAdiMevcut);

            var userAktif = _userService.GetByUserName(User.Identity.Name);

            var mesaj = $"<strong>{editUser.UserName}</strong> kullanıcısı başarı ile güncellenmiştir.";

            if (userAktif.Id == kullanici.Id)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }

            //return RedirectToAction("Index", "Home", new { area = "" });

            if (editUser.ParolaDegistir && ModelState.IsValidField("Id,Email,FirstName,UserName,LastName,Password,PasswordLast,PasswordVerify,BaslamaTarihi,BitisTarihi,ParolaDegistir"))
            {
                var sonParolaKontrol = SHA256(editUser.PasswordLast);

                if (kullanici.Password == sonParolaKontrol)
                {
                    var user = new User()
                    {
                        FirstName = editUser.FirstName,
                        LastName = editUser.LastName,
                        Password = SHA256(editUser.Password),
                        BaslamaTarihi = editUser.BaslamaTarihi,
                        BitisTarihi = editUser.BitisTarihi,
                        UserName = editUser.UserName,
                        Email = editUser.Email,
                        Id = kullanici.Id
                    };

                    _userService.Update(user);

                    TempData["KullaniciDurumu"] = mesaj;
                }
                else
                {
                    return View(editUser);
                }

                return RedirectToAction("Index");
            }
            else if (!editUser.ParolaDegistir && ModelState.IsValidField("Id,Email,FirstName,UserName,LastName,BaslamaTarihi,BitisTarihi,ParolaDegistir"))
            {
                var user = new User()
                {
                    FirstName = editUser.FirstName,
                    LastName = editUser.LastName,
                    Password = kullanici.Password,
                    BaslamaTarihi = editUser.BaslamaTarihi,
                    BitisTarihi = editUser.BitisTarihi,
                    UserName = editUser.UserName,
                    Email = editUser.Email,
                    Id = kullanici.Id
                };

                _userService.Update(user);

                TempData["KullaniciDurumu"] = mesaj;

                return RedirectToAction("Index");
            }

            //var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            //ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            return View(editUser);
        }

        // GET: EczaneNobet/Eczane/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userService.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: EczaneNobet/Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _userService.GetById(id);
            _userService.Delete(id);
            return RedirectToAction("Index");
        }

       
        public void SendMail(string subject, string body, string toEmail) // RegisterViewModel Model, string password)
        {
            // string mesaj = "Hesabınız aktif hale gelmiştir.";
            // var body = System.IO.File.ReadAllText(Server.MapPath("~/Content/EMailMessage.html"));
            // var link = Url.Action("Validate", "Account", Request.Url.Scheme);
            // var link = Url.Action("Login", "Account", new { area="", Request.Url.Scheme } );

            //body = string.Format(body, Model.User.FirstName + ' ' + Model.User.LastName, "nobetyaz.com/Account/Login");

            // SendMail msg = new SendMail();
            // msg.AddandSend(this.Form, "naklentenis@gmail.com", txtemail.Text, "naklentenis.com seyirci girişi için email ve parolanız:", mesaj, "", "");

            var fromEmail = "nobetyaz@gmail.com";
            var fromPW = "Marvelyus";

            var htmlMessage = new StringBuilder();

            htmlMessage.Append("<!DOCTYPE html><body>");
            htmlMessage.Append("<head>");
            htmlMessage.Append("<title>");
            htmlMessage.Append("Nöbet Yaz Kayıt Bilgilendirme");
            htmlMessage.Append("</title>");
            htmlMessage.Append("</head>");
            htmlMessage.Append(body);
            htmlMessage.Append(Environment.NewLine);
            htmlMessage.Append("</body></html>");


            // SendMail msg = new SendMail();
            // msg.AddandSend(this.Form, "naklentenis@gmail.com", txtemail.Text, "naklentenis.com seyirci girişi için email ve parolanız:", mesaj, "", "");

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();



            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPW);

            smtpClient.Send(message.From.ToString(), message.To.ToString(),
                message.Subject, message.Body);
        }

        public string SHA256(string strGiris)
        {
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Veri Yok");
            }
            else
            {
                SHA256Managed sifre = new SHA256Managed();
                byte[] arySifre = StringToByte(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                var hash = BitConverter.ToString(aryHash);
                return hash.Replace("-", "");
            }
        }

        public static byte[] StringToByte(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }

        static string Sha256(string randomString)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString().ToUpper();
        }

    }
}