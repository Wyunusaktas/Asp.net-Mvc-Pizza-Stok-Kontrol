using Microsoft.AspNetCore.Mvc;
using StokKontrolSistemi.Entities;

namespace StokKontrolSistemi.Components
{
    [ViewComponent]
    public class StokSayisiViewComponent:ViewComponent
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public StokSayisiViewComponent(DataBaseContext dataBaseContext, IWebHostEnvironment hostingEnvironment)
        {
            _dataBaseContext = dataBaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public IViewComponentResult Invoke()
        {  
            var sayi = _dataBaseContext.Malzemeler.Where(m => m.Stok <= 25).Count();

            return View(sayi);

        }
    }
}
