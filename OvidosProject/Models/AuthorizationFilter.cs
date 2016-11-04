using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using OvidosDAL;

namespace OvidosProject.Models
{
    //Veritabanında olmayan kullanıcıları sisteme kayıt olmaya zorlayan, istenilen action'larda kullanılmak üzere oluşturulmuş attribute
    //This attribute created for enforce users (whom didn't register to the system) to the registration.  You can use this attribute for use in the desired process
    public class AuthorizationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = actionContext.ControllerContext.RequestContext.Principal;
            KitapDBEntities db = new KitapDBEntities();
            //finding user on the context
            var hasUser = db.Kullanicilar.Any(m => m.Email+" "+m.Sifre == principal.Identity.Name);
            //Kullanıcı varsa, giriş yapmışsa ve yetkisi boştan farklı ise herhangi bir eylem gerekmez. 
            // There isn't necessary any action if user inserted in DB, user was signed-in and user's authority different from null. 
            if (principal != null && principal.Identity.IsAuthenticated && hasUser)
            {
                //don't do something
            }
            else
            {
                //değilse Yetkisiz Erişim uyarısı ver. 
                //İf there isn't system will show Unauthorized access error.
                var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized access.");
                actionContext.Response = response;
            }
            base.OnAuthorization(actionContext);
        }
    }
}