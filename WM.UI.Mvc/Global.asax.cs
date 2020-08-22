using FluentValidation.Mvc;
using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WM.BLL.DependencyResolvers.Ninject;
using WM.Core.CrossCuttingConcerns.Security.Web;
using WM.Core.CrossCuttingConcerns.Validation.FluentValidation;
using WM.Core.Utilities.Mvc.Infrastructure;
using WM.Northwind.Business.DependencyResolvers.Ninject;
using WM.UI.Mvc.App_Start;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule(), new MvcModule()));

            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new NinjectValidationFactory(new ValidationModule());
            });
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;

                if (string.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket);

                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket);
                var principal = new GenericPrincipal(identity, identity.Roles);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {

            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr");
            }
        }

        protected void Application_AcquireRequestState()
        {
            var routes = RouteTable.Routes;

            var httpContext = Request.RequestContext.HttpContext;
            if (httpContext == null) return;

            var routeData = routes.GetRouteData(httpContext);

            var language = routeData.Values["language"] as string;
            if (!(language == "tr"
                || language == "de"
                || language == "en"))
            {
                return;
            }

            var cultureInfo = new CultureInfo(language);

            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
