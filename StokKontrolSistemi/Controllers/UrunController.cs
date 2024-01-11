using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StokKontrolSistemi.Entities;
using StokKontrolSistemi.Models;
using System.Data;
using System.Data.SqlClient;


namespace StokKontrolSistemi.Controllers
{
    public class UrunController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UrunController(DataBaseContext dataBaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        //Ürünleri listeleme
        [Authorize]
        public IActionResult Index(string ara)
        {
            var list = _dataBaseContext.Urunler.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.Name.Contains(ara) || x.Description.Contains(ara)).ToList();
            }
            return View(list);
        }




        //ürün ekleme

        public IActionResult Ekle()
        {
            var kategoriler = _dataBaseContext.Kategori


                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.KategoriID.ToString(),
                }).ToList();
            var icindekiler = _dataBaseContext.Tarif
               .Select(x => new SelectListItem
               {
                   Text = x.TarifAdı,
                   Value = x.TarifID.ToString(),
               }).ToList();

            var model = new UrunViewModel
            {
                Kategoriler = kategoriler,
                Icındekiler = icindekiler
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Ekle(UrunViewModel model, IFormFile ResimDosyasi)
        {
            try
            {
                if (ResimDosyasi != null && ResimDosyasi.Length > 0)
                {
                    var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "Login", "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ResimDosyasi.FileName;
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    Directory.CreateDirectory(uploadPath);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ResimDosyasi.CopyTo(fileStream);
                    }

                    model.Data.Picture = uniqueFileName;
                }

                var malzemeIDler = _dataBaseContext.MalzemeTarif
                    .Where(tm => tm.TarifID == model.SecilenIcindekiID)
                    .Select(tm => tm.MalzemeID)
                    .ToList();

                // Malzemeler tablosundan MalzemeID'lerle malzeme isimlerini çekme
                var malzemeIsimleri = _dataBaseContext.Malzemeler
                    .Where(m => malzemeIDler.Contains(m.MalzemeID))
                    .Select(m => m.MalzemeAdi)
                    .ToList();

                model.Data.Icindekiler = string.Join(", ", malzemeIsimleri);

                var existingUrun = _dataBaseContext.Urunler.FirstOrDefault(u => u.TarifID == model.SecilenIcindekiID);

                if (existingUrun != null)
                {
                    TempData["ErrorMessage"] = "Ürün Daha Önceden Kaydedilmiştir";

                    return RedirectToAction("Ekle");
                }

                if (model.SecilenKategoriId != 0 || model.SecilenKategoriId != 0)
                {
                    _dataBaseContext.Database.ExecuteSqlRaw("UrunEkleProcedure @UrunAdi, @Aciklama, @Fiyat, @Resim, @Icindekiler, @KategoriID, @TarifID",
                        new SqlParameter("@UrunAdi", model.Name),
                        new SqlParameter("@Aciklama", model.Description),
                        new SqlParameter("@Fiyat", model.Price),
                        new SqlParameter("@Resim", model.Data.Picture),
                        new SqlParameter("@Icindekiler", model.Data.Icindekiler),
                        new SqlParameter("@KategoriID", model.SecilenKategoriId),
                        new SqlParameter("@TarifID", model.SecilenIcindekiID));

                    TempData["ErrorMassage"] = "Ürün Eklenmiştir";

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("SecilenKategoriId", "Lütfen bir kategori veya Recete seçin.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu. Lütfen tekrar deneyin.");
            }

            return View(model);
        }




        //ürün silme

        public IActionResult Sil(int id)
        {
            var urun = _dataBaseContext.Urunler.Where(x => x.UrunID == id).FirstOrDefault();

            if (urun != null)
            {
                _dataBaseContext.Urunler.Remove(urun);
                _dataBaseContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }





        //ürün güncelleme
        public IActionResult Guncelle(int id)
        {
            var guncelle = _dataBaseContext.Urunler.Where(x => x.UrunID == id).FirstOrDefault();

            if (guncelle == null)
            {
               
                return RedirectToAction("Index");
            }

            var kategoriler = _dataBaseContext.Kategori
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.KategoriID.ToString(),
                }).ToList();



            return View(guncelle);
        }

        [HttpPost]
        public IActionResult Guncelle(UrunViewModel model, IFormFile file, int id)
        {
            try
            {
                var urun = _dataBaseContext.Urunler.Find(id);

                if (urun == null)
                {
                    
                    TempData["AlertMessage"] = "Ürün bulunamadı.";
                    return RedirectToAction("Index");
                }

                var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "Login", "images");

                if (file == null)
                {
                    urun.Name = model.Name;
                    urun.Description = model.Description;
                    urun.Price = model.Price;
                }
                else
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    urun.Picture = uniqueFileName;
                    urun.Name = model.Name;
                    urun.Description = model.Description;
                    urun.Price = model.Price;
                }

                _dataBaseContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["AlertMessage"] = "Bir hata oluştu. Ürün güncellenemedi.";
                return RedirectToAction("Index");
            }
        }


        //tedarikci ekleme var olan tedarikciyle 

        public IActionResult TedarikciEkle()
        {
            List<SelectListItem> units = new List<SelectListItem>
            {
                new SelectListItem { Value = "Kg", Text = "Kg" },
                new SelectListItem { Value = "L", Text = "L" },
             
            };

            TedarikciViewModel model = new TedarikciViewModel
            {
                Units = units,
                SelectedUnit = units.FirstOrDefault()?.Value
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult TedarikciEkle(TedarikciViewModel model, int tedarikMiktari)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingMalzeme = _dataBaseContext.Malzemeler.FirstOrDefault(m => m.MalzemeAdi == model.TedarikEdilenUrun);

                    if (existingMalzeme == null)
                    {
                        Tedarikci tedarikci = new Tedarikci
                        {
                            TedarikciName = model.TedarikciName,
                            TedarikciAdres = model.TedarikciAdres,
                            TedarikEdilenUrun = model.TedarikEdilenUrun,
                        };

                        var malzeme = new Malzemeler
                        {
                            MalzemeAdi = model.TedarikEdilenUrun,
                            MalzemeTuru = model.SelectedUnit,
                            AlımTarihi = DateTime.Now,
                            Stok = model.TedarikMiktari
                        };
                        int affectedRows = _dataBaseContext.Database.ExecuteSqlRaw("EXEC MalzemeEkleProcedure @MalzemeAdi, @MalzemeTuru, @Stok, @AlimTarihi",
                                 new SqlParameter("@MalzemeAdi", malzeme.MalzemeAdi),
                                 new SqlParameter("@MalzemeTuru", malzeme.MalzemeTuru),
                                 new SqlParameter("@Stok", malzeme.Stok),
                                 new SqlParameter("@AlimTarihi", malzeme.AlımTarihi));

                        var retrievedProduct = _dataBaseContext.Malzemeler.SingleOrDefault(m => m.MalzemeAdi == malzeme.MalzemeAdi);

                        if (retrievedProduct == null)
                        {


                                TempData["ErrorMessage"] = "Bir hata oluştu. Ürün Miktarı 25 birimden küçük olamaz.";
                                return RedirectToAction("TedarikciEkle");


                          }

                   

                    
                            _dataBaseContext.Database.ExecuteSqlRaw("EXEC InsertTedarikci @TedarikciName, @TedarikciAdres, @TedarikEdilenUrun",
                                new SqlParameter("@TedarikciName", tedarikci.TedarikciName),
                                new SqlParameter("@TedarikciAdres", tedarikci.TedarikciAdres),
                                new SqlParameter("@TedarikEdilenUrun", tedarikci.TedarikEdilenUrun));

                   
                            }
                    else
                    {
                        TempData["ErrorMessage"] = "Bu malzeme daha önce kaydedilmiştir.";
                        return RedirectToAction("TedarikciEkle");
                    }

                    TempData["AlertMessage"] = "Tedarikçi ve Ürün Miktarı Kaydedilmiştir";
                    return RedirectToAction("TedarikciEkle"); // Redirect to a page displaying the list of suppliers
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's requirements
                TempData["ErrorMessage"] = "Bir hata oluştu.";
                return RedirectToAction("TedarikciEkle");
            }

            return View(model);
        }





        //yeni tedarikci ekle
        public IActionResult YeniTedarikciEkle()
        {
            List<SelectListItem> units = new List<SelectListItem>
            {
                new SelectListItem { Value = "Kg", Text = "Kg" },
                new SelectListItem { Value = "L", Text = "L" },
              
            };


            List<SelectListItem> tedarikciler = _dataBaseContext.Tedarikci
                .Select(t => new SelectListItem
                {
                    Value = t.TedarikciID.ToString(),
                    Text = t.TedarikciName
                })
                .ToList();

           
            StokEkleViewModel model = new StokEkleViewModel
            {
                Units = units,
                SelectedUnit = units.FirstOrDefault()?.Value,
                Tedarikciler = tedarikciler
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult YeniTedarikciEkle(StokEkleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var selectedTedarikci = _dataBaseContext.Tedarikci.FirstOrDefault(t => t.TedarikciID == model.SelectedTedarikciID);

                    // Var olan malzeme kontrolü
                    var existingMalzeme = _dataBaseContext.Malzemeler.FirstOrDefault(m => m.MalzemeAdi == selectedTedarikci.TedarikEdilenUrun);

                    if (existingMalzeme != null)
                    {
                        // Var olan malzemenin üzerine ekleme yapılacaksa güncelle
                        existingMalzeme.Stok += model.StokMiktari;
                        existingMalzeme.AlımTarihi = DateTime.Now;
                    }
                    else
                    {
                        // Yeni malzeme oluştur
                        Malzemeler malzemeler = new Malzemeler
                        {
                            MalzemeAdi = selectedTedarikci.TedarikEdilenUrun,
                            Stok = model.StokMiktari,
                            MalzemeTuru = model.SelectedUnit,
                            AlımTarihi = DateTime.Now,
                        };

                        _dataBaseContext.Malzemeler.Add(malzemeler);
                    }

                    // MalzemeTedarikci ilişkisi güncelleniyor
                    MalzemeTedarikci malzemeTedarikci = new MalzemeTedarikci
                    {
                        Tedarikci = selectedTedarikci,
                        Malzeme = existingMalzeme ?? _dataBaseContext.Malzemeler.LastOrDefault() // Güncellenen veya yeni oluşturulan malzeme kullanılır
                    };

                    _dataBaseContext.MalzemeTedarikciler.Add(malzemeTedarikci);
                    _dataBaseContext.SaveChanges();

                    TempData["AlertMessage"] = "Ürün Miktarı Kaydedilmiştir";

                    return RedirectToAction("YeniTedarikciEkle");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu. Ürün miktarı kaydedilemedi.";
                return RedirectToAction("YeniTedarikciEkle");
            }

            return View(model);
        }




        //stok görüntüleme
        public IActionResult StokGoruntule()
        {
            var stoktaOlanMalzemeler = _dataBaseContext.Malzemeler
                                           .Where(m => m.Stok > 0)
                                           .ToList();
            return View(stoktaOlanMalzemeler);
        }




        //kritik stokları görüntüleme
        public IActionResult KritikStok()
        {
            var kritikstok = _dataBaseContext.Malzemeler.Where(m => m.Stok <= 25).ToList();
            ViewBag.KritikStokSayisi = kritikstok.Count;
            return View(kritikstok);

        }




        // her pizzzaya özgü tarif oluşturma

        public ActionResult TarifEkle()
        {
            var malzemeler = _dataBaseContext.Malzemeler.ToList();
            return View(malzemeler);
        }





        [HttpPost]
        public ActionResult TarifEkle(IEnumerable<int> selectedMalzemeler, Dictionary<int, int> miktar, TarifModel model)
        {
            try
            {
                if (selectedMalzemeler != null && miktar != null)
                {
                    // Yapılacak işlemler: Seçilen malzemeleri ve miktarları kullanarak Tarif oluşturma
                    var tarif = new Tarifler
                    {
                        TarifAdı = model.TarifAdi
                    };

                    foreach (var malzemeID in selectedMalzemeler)
                    {
                        var malzeme = _dataBaseContext.Malzemeler.Find(malzemeID);

                        if (malzeme != null && miktar.TryGetValue(malzemeID, out int quantity))
                        {
                            // Tarif oluşturma işlemleri

                            foreach (var selectedMalzemeID in selectedMalzemeler)
                            {
                                var malzemeTarif = new MalzemeTarif
                                {
                                    MalzemeID = selectedMalzemeID,
                                    Miktar = miktar[selectedMalzemeID],
                                    Tarif = tarif
                                };

                                _dataBaseContext.MalzemeTarif.Add(malzemeTarif);
                            }

                            _dataBaseContext.SaveChanges();
                            TempData["AlertMessage"] = "Tarfi Eklenmiştir";
                          
                            return RedirectToAction("TarifEkle");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu. Tarif oluşturulamadı.";
                return RedirectToAction("TarifEkle");
            }

            return RedirectToAction("TarifEkle");
        }



        // satılan pizzayay göre sipariş güncelleme
        public IActionResult Siparis()
        {
            var urunler = _dataBaseContext.Urunler.ToList();
            return View(urunler);
        }


        [HttpPost]
        public IActionResult Siparis(List<int> SecileUrunlerId, SiparisViewModel model)
        {
            try
            {
              
                if (SecileUrunlerId != null && SecileUrunlerId.Any())
                {
                    var satis = new Satislar
                    {
                        KullaniciID = int.Parse(User.FindFirst("id")?.Value),
                        SiparisTarihi = DateTime.Now
                    };

                    _dataBaseContext.Satislar.Add(satis);
                    _dataBaseContext.SaveChanges();

                    for (int i = 0; i < SecileUrunlerId.Count; i++)
                    {
                        var tarifIdListesi = _dataBaseContext.Urunler
                            .Where(u => u.UrunID == SecileUrunlerId[i])
                            .Select(u => u.TarifID)
                            .ToList();

                        var malzemeIdListesi = _dataBaseContext.MalzemeTarif
                           .Where(mt => tarifIdListesi.Contains(mt.TarifID))
                           .ToList();

                        foreach (var item in malzemeIdListesi)
                        {
                            var malzeme = _dataBaseContext.Malzemeler
                                .FirstOrDefault(m => m.MalzemeID == item.MalzemeID);

                            if (malzeme != null)
                            {
                                malzeme.Stok -= (item.Miktar / 1000) * model.Adet[i];
                            }
                        }

                        _dataBaseContext.SaveChanges();

                        var urunSatis = new UrunSatis
                        {
                            SatislarID = satis.SatislarID,
                            UrunID = SecileUrunlerId[i],
                            Adet = model.Adet[i],
                        };

                        _dataBaseContext.UrunSatislar.Add(urunSatis);
                        _dataBaseContext.SaveChanges();
                    }

                    TempData["AlertMessage"] = "Stok Güncellenmiştir";
                    return RedirectToAction("Siparis");
                }

                TempData["AlertMessage"] = "Lütfen en az bir ürün seçiniz.";
                return RedirectToAction("Siparis");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hata oluştu: {ex.Message}";
                return RedirectToAction("Siparis");
            }
        }

        //harcanan un miktarı ay bazlı
        public IActionResult HarcananUnMiktar()
        {
            var result = (from s in _dataBaseContext.Satislar
                          join us in _dataBaseContext.UrunSatislar on s.SatislarID equals us.SatislarID
                          join u in _dataBaseContext.Urunler on us.UrunID equals u.UrunID
                          join mt in _dataBaseContext.MalzemeTarif on u.TarifID equals mt.TarifID
                          join m in _dataBaseContext.Malzemeler on mt.MalzemeID equals m.MalzemeID
                          where m.MalzemeAdi == "Un"
                          group new { s, mt } by new { Month = s.SiparisTarihi.Month, Year = s.SiparisTarihi.Year } into g
                          select new HarcananUnMiktarViewModel
                          {
                              Ay = g.Key.Month,
                              Yil = g.Key.Year,
                              HarcananUnMiktari = g.Sum(x => x.mt.Miktar/1000)
                          }).OrderBy(x => x.Yil).ThenBy(x => x.Ay).ToList();

            return View(result);
        }


        //pizza satıs oranları ay bazlı
        public IActionResult PizzaSatisOranlari()
        {
            var result = _dataBaseContext.Satislar
              .Join(_dataBaseContext.UrunSatislar,
                  s => s.SatislarID,
                  us => us.SatislarID,
                  (s, us) => new { s, us })
              .Join(_dataBaseContext.Urunler,
                  i => i.us.UrunID,
                  u => u.UrunID,
                  (i, u) => new { i, u })
              .GroupBy(joined => new
              {
                  Month = joined.i.s.SiparisTarihi.Month,
                  Year = joined.i.s.SiparisTarihi.Year,
                  Name = joined.u.Name
              })
              .Select(g => new PizzaSatisOraniViewModel
              {
                  Ay = g.Key.Month,
                  Yil = g.Key.Year,
                  PizzaCesidi = g.Key.Name,
                  SatilanAdet = g.Sum(x => x.i.us.Adet),
                  SatilmaOrani = ((double)g.Sum(x => x.i.us.Adet) / _dataBaseContext.UrunSatislar
                    .Where(us => us.Satislar.SiparisTarihi.Month == g.Key.Month && us.Satislar.SiparisTarihi.Year == g.Key.Year)
                    .Sum(us => us.Adet)) * 100
              })
              .OrderBy(x => x.Yil)
              .ThenBy(x => x.Ay)
              .ThenBy(x => x.PizzaCesidi)
              .ToList();

            return View(result);
        }
    }
}
