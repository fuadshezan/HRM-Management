using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTHRM.Data;
using GTHRM.Models;
using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;

namespace GTHRM.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendances.Include(a => a.Company).Include(a => a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Company)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            ViewData["Employee"] = new SelectList(_context.Employees.Where(x => x.ComId == comId), "EmpId", "EmpName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance model)
        {
            if (ModelState.IsValid)
            {
                model.ComId = Guid.Parse(Request.Cookies["comId"].ToString());
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComId"] = new SelectList(_context.Company, "ComId", "CompanyName", model.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpCode", model.EmpId);
            return View(model);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(Guid id,DateTime date)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());

            var model =  _context.Attendances
                         .Where(x=>x.EmpId == id && x.ComId==comId && x.dtDate==date)
                         .FirstOrDefault();
            if (model == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Attendance model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(model.ComId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid? id,DateTime date)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Attendances
                         .Where(x => x.EmpId == id && x.ComId == comId && x.dtDate == date)
                         .FirstOrDefaultAsync();

            return View(model);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Attendance model)
        {
            try
            {
                if (model != null)
                {
                    _context.Remove(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
            }
            return View(model);
        }

        public async Task<IActionResult> ProcessStatus(DateTime date)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            try
            {
                var SpParameter = new List<SqlParameter>();
                SpParameter.Add(new SqlParameter("@ComId", comId));
                SpParameter.Add(new SqlParameter("@dtDate", date));

                var result = await Task.Run(() => _context.Database
            .ExecuteSqlRawAsync(@"exec UpdateAttendanceStatus @ComId, @dtDate", SpParameter.ToArray()));

                //_context.Database.ExecuteSqlRawAsync("EXEC UpdateAttendanceStatus @ComId, @dtDate", comId, date);
                //return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(Guid id)
        {
          return (_context.Attendances?.Any(e => e.ComId == id)).GetValueOrDefault();
        }
    }
}
