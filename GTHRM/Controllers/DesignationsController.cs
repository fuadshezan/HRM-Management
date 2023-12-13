using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTHRM.Data;
using GTHRM.Models;
using System.ComponentModel.Design;

namespace GTHRM.Controllers
{
    public class DesignationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DesignationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Designations
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Designations.Include(d => d.Company).Where(x => x.ComId == comId).ToListAsync();
            return View(model);
        }

        // GET: Designations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Designations == null)
            {
                return NotFound();
            }

            var designation = await _context.Designations
                .Include(d => d.Company)
                .FirstOrDefaultAsync(m => m.DesigId == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // GET: Designations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Designation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var comId = Guid.Parse(Request.Cookies["comId"].ToString());
                    model.ComId = comId;

                    await _context.AddAsync(model);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
                return View(model);
            }
            return View(model);

        }

        // GET: Designations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());
            if (id == null || _context.Designations == null)
            {
                return NotFound();
            }

            var model = await _context.Designations.Where(x => x.DesigId == id && x.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Designation model)
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

        // GET: Designations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());

            if (id == null || _context.Designations == null)
            {
                return NotFound();
            }

            var model = await _context.Designations.Where(m => m.DesigId == id && m.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Designation model)
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

        private bool DesignationExists(Guid id)
        {
            return (_context.Designations?.Any(e => e.DesigId == id)).GetValueOrDefault();
        }
    }
}
