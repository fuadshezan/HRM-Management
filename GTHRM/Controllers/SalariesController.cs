using GTHRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GTHRM.Controllers
{
    public class SalariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalariesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                return RedirectToAction(nameof(Index),"Home");
            }
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.salaries.Where(x => x.ComId == comId).Include(a => a.Employee).ToListAsync();

            return View(model);
        }
    }
}
