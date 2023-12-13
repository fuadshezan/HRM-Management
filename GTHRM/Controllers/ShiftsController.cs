using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTHRM.Data;
using GTHRM.Models;

namespace GTHRM.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                RedirectToAction(nameof(Index), "Home");
            }
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Shifts.Include(d => d.Company).Where(x => x.ComId == comId).ToListAsync();
            return View(model);
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Shifts == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .Include(s => s.Company)
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Shift model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var comId = Guid.Parse(Request.Cookies["comId"].ToString());
                    model.ComId = comId;
                    _context.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));



                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            return View(model);
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            if (id == null || _context.Shifts == null)
            {
                return NotFound();
            }

            var model = await _context.Shifts.Where(x => x.ShiftId == id && x.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Shift model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                }

            }

            return View(model);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());

            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var model = await _context.Shifts.Where(m => m.ShiftId == id && m.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Shift model)
        {
            try
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            return View(model);
        }

        private bool ShiftExists(Guid id)
        {
          return (_context.Shifts?.Any(e => e.ShiftId == id)).GetValueOrDefault();
        }
    }
}
