using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StokKontrolSistemi.Entities;
using StokKontrolSistemi.Models;

namespace StokKontrolSistemi.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(DataBaseContext dataBaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "A")]
        public IActionResult Index()
        {
            var kullanici = _dataBaseContext.Kullanici.ToList();
            return View(kullanici);
        }
        public IActionResult KullaniciEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KullaniciEkle(RegisterViewModels model)
        {
            try
            {
                Kullanici user = _dataBaseContext.Kullanici.SingleOrDefault(x => x.Email == model.Email.ToLower() && x.Username == model.Username.ToLower());
                if (user != null)
                {
                    ModelState.AddModelError("", "Kullanıcı Adı Veya Email Önceden Alınmış.");
                }

                if (ModelState.IsValid)
                {
                    Kullanici User = new()
                    {
                        Name = model.name,
                        SurName = model.Surname,
                        Email = model.Email,
                        Password = model.Password,
                        Username = model.Username,
                        State = "U",
                        Locked = false
                    };

                    _dataBaseContext.Kullanici.Add(User);
                    int affectedRowCount = _dataBaseContext.SaveChanges();

                    if (affectedRowCount == 0)
                    {
                        ModelState.AddModelError("", "Kullanıcı Eklenemedi.");
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Kullanıcı Eklenmiştir";
                        return RedirectToAction("KullaniciEkle");
                    }
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Bir hata oluştu. Kullanıcı kaydedilemedi.");
            }

            return View(model);
        }
        public IActionResult Duzenle(int id)
        {
            var kullanici = _dataBaseContext.Kullanici.Find(id);

            if (kullanici == null)
            {
                
            }

            return View(kullanici);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Duzenle(int id, [Bind("Id,Name,SurName,Email,Password,Username,State,Locked")] Kullanici editedKullanici)
        {
            if (id != editedKullanici.Id)
            {
                
                return RedirectToAction("Index");
            }

          
                try
                {
                   
                    _dataBaseContext.Update(editedKullanici);
                    _dataBaseContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    return RedirectToAction("Index");
                }
            TempData["AlertMessage"] = "Kullanıcı Düzenlenmiştir";
            return RedirectToAction("Duzenle");
            

            
        }
        public IActionResult Sil(int id)
        {
            var kullanici = _dataBaseContext.Kullanici.Where(x => x.Id == id).FirstOrDefault();

            if (kullanici != null)
            {
              kullanici.Locked= true;
                _dataBaseContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
