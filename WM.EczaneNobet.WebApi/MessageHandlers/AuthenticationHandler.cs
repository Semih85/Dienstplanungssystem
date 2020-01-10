using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.DependencyResolvers.Ninject;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.EczaneNobet.WebApi.MessageHandlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("Authorization").FirstOrDefault();

                if (token != null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedString.Split(':');

                    var userService = InstanceFactory.GetInstance<IUserService>();

                    var emailAndPassword = new LoginItem
                    {
                         Email= tokenValues[0],
                         Password = tokenValues[1]
                    };

                    var user = userService.GetByEMailAndPassword(emailAndPassword);
                    //tokenValues[0] == "semih" && tokenValues[1] == "12345"
                    //new[] { "Admin" }
                    if (user != null)
                    {
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokenValues[0]),
                            userService.GetUserRoles(user).Select(u => u.RoleName).ToArray());
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                }
            }
            catch
            {

            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}