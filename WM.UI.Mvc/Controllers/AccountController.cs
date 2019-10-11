//using Microsoft.Owin.Security;
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

namespace WM.UI.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public AccountController(IUserService userService,
            IUserNobetUstGrupService userNobetUstGrupService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _userService = userService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            //var rolId = rolIdler.FirstOrDefault();

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var userNobetUstGruplar = _userNobetUstGrupService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList());

            //var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var kullanicilar = _userService.GetList()
                .OrderBy(o => o.UserName)
                .ToList();
            //var liste = new List<User>();

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
                var user = _userService.GetByEMailAndPassword(login.LoginItem);

                //Session["nobetUstGrupId"] = 1;

                if (user != null)
                {
                    var simdi = DateTime.UtcNow;

                    var remember = login.LoginItem.RememberMe;

                    var utcExpires = remember
                        ? simdi.AddDays(45)
                        : simdi.AddHours(8);

                    var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
                    var roller = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleName).ToArray();

                    if (rolIdler.Count() > 0)
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
                            url = RedirectToAction("Index", "EczaneKullanici", new { area = "EczaneNobet", userId = user.Id });
                            break;
                        //case 5://misafir
                        //    url = RedirectToAction("Index", "EczaneKullanici", new { area = "EczaneNobet", userId = user.Id });
                        //    break;
                        case 6:
                            url = RedirectToAction("Index", "NobetKomisyonu", new { area = "EczaneNobet" });
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
        public ActionResult Register(RegisterViewModel Model)
        {
            //if (ReCaptcha.Validate("6LfnxisUAAAAAHvALIZD4c_ai1fu41L8HjCQ0-00"))
            //{
            //var token = "";
            try
            {//son true kullanıcıya doğrulama maili göndermek için
             // token = WebSecurity.CreateUserAndAccount(Model.User.EMail, Model.User.Password, new { First_name = Model.User.FirstName, Last_name = Model.User.LastName }, true);

                _userService.Insert(Model.User);

                // Roles.AddUserToRole(Model.User.Email, "Members");
                var subject = "Nöbet Yaz kayıt";

                var body =
                    $"<p>" +
                        $"Merhaba, Sayın {Model.User.FirstName} {Model.User.LastName.ToUpper()}." +
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
                                $"{Model.User.UserName}" +
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
                                $"{Model.User.Password}" +
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
                                $"{Model.User.BaslamaTarihi}" +
                            $"</td>" +
                        $"</tr>" +
                    $"</table>"
                    ;

                var toEmail = "ozdamar85@gmail.com";//;Model.User.Email;

                //SendMail(subject, body, toEmail);

                TempData["KayitSonuc"] = "Kayıt başarılı.";
            }
            catch
            {
                TempData["Message"] = "Bu email ile kayıt zaten yapılmış!";
                return View("Error");
            }

            return RedirectToAction("Index");
            //}
            //else
            //{
            //    TempData["Message"] = "ReCaptcha yanlış!";

            //    return View("Fail");
            //}
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

            var fromEmail = "yoneylemci@hotmail.com";
            var fromPW = "semihates2017";

            var htmlMessage = new StringBuilder();

            htmlMessage.Append("<!DOCTYPE html><body>");
            htmlMessage.Append("<head>");
            htmlMessage.Append("<title>");
            htmlMessage.Append("Nöbet Yaz Kayıt Bilgilendirme!");
            htmlMessage.Append("</title>");
            htmlMessage.Append("</head>");
            htmlMessage.Append(body);
            htmlMessage.Append(Environment.NewLine);
            htmlMessage.Append("</body></html>");

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                IsBodyHtml = true,
                Body = htmlMessage.ToString()
            };

            message.To.Add(toEmail);

            var smtpClient = new SmtpClient("smtp.live.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential(fromEmail, fromPW)
            };

            smtpClient.Send(message);
        }
    }
}