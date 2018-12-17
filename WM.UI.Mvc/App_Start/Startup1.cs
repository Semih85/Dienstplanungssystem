using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(WM.UI.Mvc.App_Start.Startup1))]

namespace WM.UI.Mvc.App_Start
{
    public class Startup1
    {
        public CookieAuthenticationOptions Yol { get; set; }

        public void Configuration(IAppBuilder app)
        {
            // Uygulamanızı nasıl yapılandıracağınız hakkında daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=316888 adresini ziyaret edin
            //var url = HttpContext.Current.Request.Url.AbsolutePath;
            //string loginpath=

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login")
            });


        }
    }
}
