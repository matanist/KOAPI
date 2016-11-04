using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OvidosPresentation.Controllers
{
    public class HomeController : Controller
    {
        //Anasayfa. Ayrıca kayıtlı kullanıcıların mesaj gönderdiği sayfa.
        //Homepage. Also page where clients send messages
        public ActionResult Index()
        {
            return View();
        }
        
        //Kayıtlı kullanıcıların giriş yapabileceği bir sayfa
        //This is a Login page. Users can be sign-in the system via this page
        public ActionResult Login()
        {
            return View();
        }
    }
}