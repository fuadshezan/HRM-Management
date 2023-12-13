using GTHRM.Data;
using GTHRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GTHRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Company> companies =await _context.Company.ToListAsync();
            return View(companies);
        }

        public async Task<IActionResult> SetCompany(Guid comId)
        {
            Response.Cookies.Append("comId",comId.ToString());
            var company = await _context.Company.Where(x => x.ComId == comId).FirstOrDefaultAsync();
            Response.Cookies.Append("comName", company.CompanyName);

            return RedirectToAction(nameof(Index));
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