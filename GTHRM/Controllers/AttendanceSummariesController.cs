using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTHRM.Data;
using GTHRM.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GTHRM.Controllers
{
    public class AttendanceSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceSummariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceSummaries
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.AttendanceSummaries.Where(x => x.ComId == comId).Include(a =>a.Employee).ToListAsync();
          
            return View(model);
        }

        
        public async Task<IActionResult> ProcessMonthly(int year,int month)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            try
            {
                var SpParameter = new List<SqlParameter>();
                SpParameter.Add(new SqlParameter("@ComId", comId));
                SpParameter.Add(new SqlParameter("@Month", month));
                SpParameter.Add(new SqlParameter("@Year", year));

                var result = await Task.Run(() => _context.Database
            .ExecuteSqlRawAsync(@"exec monthlyAttendance @ComId, @Month, @Year", SpParameter.ToArray()));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString()); 
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceSummaryExists(Guid id)
        {
          return ( _context.AttendanceSummaries?.Any(e => e.ComId == id)).GetValueOrDefault();
        }
    }
}
