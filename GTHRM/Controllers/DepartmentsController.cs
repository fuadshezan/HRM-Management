using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTHRM.Data;
using GTHRM.Models;
using System.Linq.Expressions;

namespace GTHRM.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                return RedirectToAction(nameof(Index),"Home");
            }
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            //var model = _context.Departments.Where(x=>x.ComId== comId).FirstOrDefault();
            var model = await _context.Departments.Include(d => d.Company).Where(x => x.ComId == comId).ToListAsync();
            return View(model);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Company)
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Department model)
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

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var model = await _context.Departments.Where(x => x.DeptId == id && x.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department model)
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

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());

            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var model = await _context.Departments.Where(m => m.DeptId == id && m.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Department model)
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

        private bool DepartmentExists(Guid id)
        {
          return (_context.Departments?.Any(e => e.DeptId == id)).GetValueOrDefault();
        }
    }
}
