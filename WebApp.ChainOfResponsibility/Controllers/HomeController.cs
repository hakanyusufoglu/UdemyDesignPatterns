using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.ChainOfResponsibility.ChainOfResponsiblity;
using WebApp.ChainOfResponsibility.Models;

namespace BaseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendEmail()
        {
            var products = await _context.Products.ToListAsync();

            var excelProcessHandler = new ExcelProcessHandler<Product>();

            var zipFileProcessHandler = new ZipFileProcessHandler<Product>();

            var sendEMailProcessHandler = new SendEmailProcessHandler("product.zip", "hakanyusufoglu@outlook.com");

            //Chain of responsibility - yukarıdaki 3 işlemi zincilere bağlıyorum.

            excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEMailProcessHandler);

            //ilk halkamın handle metodunu çağırarak diğer halkalar çalışmasını sağlıyorum.

            excelProcessHandler.Handle(products);

            return View(nameof(Index));

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}