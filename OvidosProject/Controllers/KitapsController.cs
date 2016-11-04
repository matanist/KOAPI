using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using OvidosDAL;
using OvidosProject.Models;

namespace OvidosProject.Controllers
{
    //Tüm domainlerden erişmek için orijin header'ları belirliyoruz.
    //we are determining the orijin header for the access from all domains
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [AuthorizationFilter]
    public class KitapsController : ApiController
    {
        private KitapDBEntities db = new KitapDBEntities();

        // GET: api/Kitaps
        public IQueryable<Kitap> GetKitaplar()
        {
            return db.Kitaplar;
        }

        // GET: api/Kitaps/5
        [ResponseType(typeof(Kitap))]
        public IHttpActionResult GetKitap(int id)
        {
            Kitap kitap = db.Kitaplar.Find(id);
            if (kitap == null)
            {
                return NotFound();
            }

            return Ok(kitap);
        }

        // PUT: api/Kitaps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKitap(int id, Kitap kitap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kitap.Id)
            {
                return BadRequest();
            }

            db.Entry(kitap).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KitapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
      
        // POST: api/Kitaps
        [ResponseType(typeof(Kitap))]
        public IHttpActionResult PostKitap(Kitap kitap)
       {
           //todo:Kontroller gerçekleştirilmedi.
            var mevcutKullaniciBilgileri = RequestContext.Principal.Identity.Name;
            var seciliKitap = db.Kitaplar.FirstOrDefault(k => k.Id == kitap.Id);
           var mevcutKullanici = db.Kullanicilar.FirstOrDefault(k => k.Email + " " + k.Sifre == mevcutKullaniciBilgileri);
           var yeniKullaniciKitap = new KullaniciKitap();
           yeniKullaniciKitap.Kitap = seciliKitap;
           yeniKullaniciKitap.Kullanici = mevcutKullanici;
            yeniKullaniciKitap.OduncAlmaZamani=DateTime.Now;
           db.KullaniciKitaplar.Add(yeniKullaniciKitap);
           db.SaveChanges();
            mevcutKullanici.Kitaplar.Add(db.KullaniciKitaplar.FirstOrDefault(kk=>kk.Id==yeniKullaniciKitap.Id));
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.Kitaplar.Add(kitap);
           seciliKitap.StokMiktari--;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kitap.Id }, kitap);
            //return Ok(test);
        }

        // DELETE: api/Kitaps/5
        [ResponseType(typeof(Kitap))]
        public IHttpActionResult DeleteKitap(int id)
        {
            Kitap kitap = db.Kitaplar.Find(id);
            if (kitap == null)
            {
                return NotFound();
            }

            db.Kitaplar.Remove(kitap);
            db.SaveChanges();

            return Ok(kitap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KitapExists(int id)
        {
            return db.Kitaplar.Count(e => e.Id == id) > 0;
        }
    }
}