using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WM.Core.Aspects.PostSharp.AutorizationAspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string[] roles = Roles.Split(',');
            bool isAutorized = false;

            for (int i = 0; i < roles.Length; i++)
            {
                var rol = roles[i];

                if (System.Threading.Thread.CurrentPrincipal.IsInRole(rol))
                {
                    isAutorized = true;
                }
            }

            if (isAutorized == false)
            {
                throw new SecurityException("You are not authorized!");
            }
        }
    }
}
