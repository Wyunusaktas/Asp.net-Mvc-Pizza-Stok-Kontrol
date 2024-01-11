using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StokKontrolSistemi.Entities;
using StokKontrolSistemi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using System.Data;
using ClosedXML.Excel;
using ExcelDataReader;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StokKontrolSistemi.Controllers
{

	public class AccountController : Controller
	{
        private readonly DataBaseContext _dataBaseContext;

        public AccountController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }
        // Griş işlemleri yapılcak
        public IActionResult Login()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Login(LoginViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Kullanici user = _dataBaseContext.Kullanici.SingleOrDefault(x => x.Email == model.Email.ToLower() && x.Password == model.Password.ToLower());

                    if (user != null)
                    {
                        if (user.Locked)
                        {
                            ModelState.AddModelError("", "Kullanıcı Silinmiş");
                            return View(model);
                        }

                        List<Claim> claims = new List<Claim>
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.Name.ToString()),
                    new Claim("SurName", user.SurName.ToString()),
                    new Claim("Username", user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.State)
                };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Urun");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Yanlış.");
                    }
                }
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "Bir hata oluştu. Giriş yapılamadı.");
            }

            return View(model);
        }





        // kayıt olma işlemler
        public IActionResult Register()
		{
			return View();
		}
        [HttpPost]
        public IActionResult Register(RegisterViewModels model)
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

                    _dataBaseContext.Database.ExecuteSqlRaw("EXEC KullaniciEkleProcedure @Name, @SurName, @Email, @Password, @Username, @State, @Locked",
                             new SqlParameter("@Name", model.name),
                             new SqlParameter("@SurName", model.Surname),
                             new SqlParameter("@Email", model.Email),
                             new SqlParameter("@Password", model.Password),
                             new SqlParameter("@Username", model.Username),
                             new SqlParameter("@State", "U"),  
                             new SqlParameter("@Locked", false)
                     );

                    return RedirectToAction("Login");


                  
                }
            }
            catch (Exception ex)
            {
               
                TempData["ErrorMessage"] = "Şifrenizde minimum 8 haneli ve 1 tane harf, 1 tane özel karakter, 1 tane sayı olmalıdır.";
                        return RedirectToAction("Register");
    }

            return View(model);
        }


        //çıkış yapma işlemi

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }



        //kullanıcı profil ekranı
        public IActionResult profile()
        {
          
            return View();
        }
		[HttpPost]
        public IActionResult Profile(LoginViewModels model)
        {
            var kullanici = model.Email;
			if (ModelState.IsValid)
			{


			}
			
            return View(model);
        }


        
        public IActionResult GoogleLogin()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            }, GoogleDefaults.AuthenticationScheme);
        }
       
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return RedirectToAction("Index", "Urun");
        }



        //şifre değiştirme   gmail ile
        public IActionResult SifreReset()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SifreReset(string eposta)
        {
            var mail = _dataBaseContext.Kullanici.Where(x => x.Email == eposta).FirstOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                string yenisifre = rnd.Next()+"a"+".";
                Kullanici sifre = new Kullanici();
                mail.Password = yenisifre;
                _dataBaseContext.SaveChanges();

                // E-postayı göndermek için SmtpClient'ı kullanın
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("gönderici maili", "urhrcqfgazgodemg");
                    client.EnableSsl = true;
                    client.Port = 587;
                    string emailBody = "Şifreniz: " + yenisifre;
                    // E-postayı oluşturun ve gönderin
                    MailMessage message = new MailMessage("stokkontrolapp@gmail.com", eposta, "Giriş Şifreniz", emailBody);
                    client.Send(message);
                }

                ViewBag.uyari = "Şifreniz Başarıyla Gönderilmiştir";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.uyari = "Şifreniz Gönderilemedi";
            }

            return View();
        }

        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        { 
            var _empdata=GetEmpdata();
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_empdata, "Stok Bilgileri");
                using(MemoryStream ms=new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StokBilgileri.xlsx");

                }
            }

        }
        [NonAction]
        private DataTable GetEmpdata()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("MalzemeID", typeof(int));
            dt.Columns.Add("MalzemeAdi", typeof(string));
            dt.Columns.Add("Stok", typeof(float));
            dt.Columns.Add("AlimTarihi", typeof(string));
          ;
            var _list=this._dataBaseContext.Malzemeler.ToList();
            if (_list.Count > 0 )
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.MalzemeID,item.MalzemeAdi,item.Stok,item.AlımTarihi);
                });
            }
            return dt;

        }
        //excel import işlemi


        public IActionResult UploadExcel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file != null && file.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);

                // Ensure the FileStream is properly closed after copying
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Open the file for reading using a new FileStream
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Disable triggers temporarily
                    await _dataBaseContext.Database.ExecuteSqlRawAsync("DISABLE TRIGGER ALL ON Malzemeler");

                    try
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                bool isHeaderSkipped = false;

                                while (reader.Read())
                                {
                                    if (!isHeaderSkipped)
                                    {
                                        isHeaderSkipped = true;
                                        continue;
                                    }

                                    Malzemeler S = new Malzemeler();
                                    S.MalzemeAdi = reader.GetValue(1).ToString();
                                    S.Stok = Convert.ToInt32(reader.GetValue(2).ToString());
                                    // AlımTarihi'ni DateTime türüne çevirme
                                    string databaseDateValue = reader.GetValue(3).ToString();

                                    Tedarikci D = new Tedarikci();
                                    D.TedarikciName = reader.GetValue(5).ToString();
                                    D.TedarikciAdres = reader.GetValue(6).ToString();
                                    D.TedarikEdilenUrun = reader.GetValue(7).ToString();

                                    if (DateTime.TryParse(databaseDateValue, out DateTime alimTarihi))
                                    {
                                        S.AlımTarihi = alimTarihi;
                                    }
                                    S.MalzemeTuru = reader.GetValue(4).ToString();
                                    _dataBaseContext.Add(S);
                                    _dataBaseContext.Add(D);
                                    await _dataBaseContext.SaveChangesAsync();

                                    // Trigger logic
                                    if (S.Stok < 25)
                                    {
                                        // Execute the trigger logic using raw SQL
                                        await _dataBaseContext.Database.ExecuteSqlRawAsync(
                                            $"EXEC StokKontrolu @MalzemeID = {S.MalzemeID}, @StokMiktar = {S.Stok}");
                                    }
                                }
                            } while (reader.NextResult());
                        }
                        await _dataBaseContext.SaveChangesAsync();
                    }

                    catch (Exception ex)
                    {
                        // Handle exceptions as needed
                        // You may want to log the exception for further investigation
                    }
                    finally
                    {
                        // Enable triggers after the operation
                        await _dataBaseContext.Database.ExecuteSqlRawAsync("ENABLE TRIGGER ALL ON Malzemeler");
                    }
                }
            }

            return View();
        }

    }
}
