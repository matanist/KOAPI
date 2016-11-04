using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace OvidosProject.Models
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;
            //authorization boştan farklı parametresi de string boşluktan ve null'dan farklı ise kimlik denetimi yapılandırılması yapılıyor.
            //authorization isn't null and it's parameter doesn't contains string null ("") or different from null (null); system will do Configuring authentication
            if (authorization != null && !string.IsNullOrEmpty(authorization.Parameter))
            {
                var identity = new GenericIdentity(authorization.Parameter, authorization.Scheme);
                var principal = new GenericPrincipal(identity, null);
                context.Principal = principal;
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}