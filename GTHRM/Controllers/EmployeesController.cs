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
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            if (!Request.Cookies.ContainsKey("comId"))
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Employees
                                .Include(e => e.Company)
                                .Include(e => e.Department)
                                .Include(e => e.Designation)
                                .Where(x=>x.ComId== comId)
                                .ToListAsync();
            return View(model);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Shift)
                .Where(m => m.EmpId == id && m.ComId == comId)
                .FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var comId = Request.Cookies["comId"].ToString();
            ViewData["Department"] = new SelectList(_context.Departments.Where(x=>x.ComId.ToString()== comId), "DeptId", "DepartmentName");
            ViewData["Designation"] = new SelectList(_context.Designations.Where(x => x.ComId.ToString() == comId), "DesigId", "DesigName");
            ViewData["Shift"] = new SelectList(_context.Shifts.Where(x => x.ComId.ToString() == comId), "ShiftId", "ShiftName");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Employee model)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            try
            {

                if (ModelState.IsValid)
                {
                    model.ComId =comId;
                    await _context.AddAsync(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }

            ViewData["Department"] = new SelectList(_context.Departments.Where(x => x.ComId == comId), "DeptId", "DepartmentName");
            ViewData["Designation"] = new SelectList(_context.Designations.Where(x => x.ComId == comId), "DesigId", "DesigName");
            ViewData["Shift"] = new SelectList(_context.Shifts.Where(x => x.ComId == comId), "ShiftId", "ShiftName");

            return View(model);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var model = await _context.Employees.Where(x => x.EmpId == id && x.ComId == comId).FirstOrDefaultAsync();
            if (model == null)
            {
                return NotFound();
            }
            //ViewData["ComId"] = new SelectList(_context.Company, "ComId", "CompanyName", employee.ComId);
            ViewData["Department"] = new SelectList(_context.Departments.Where(x => x.ComId == comId), "DeptId", "DepartmentName");
            ViewData["Designation"] = new SelectList(_context.Designations.Where(x => x.ComId == comId), "DesigId", "DesigName");
            ViewData["Shift"] = new SelectList(_context.Shifts.Where(x => x.ComId == comId), "ShiftId", "ShiftName");
            return View(model);
            //return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee model)
        {
            var comId = Guid.Parse(Request.Cookies["comId"].ToString());
            model.ComId = comId;
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(model.EmpId))
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
            ViewData["ComId"] = new SelectList(_context.Company, "ComId", "CompanyName", model.ComId);
            ViewData["DeptId"] = new SelectList(_context.Departments, "DeptId", "DepartmentName", model.DeptId);
            ViewData["DesigId"] = new SelectList(_context.Designations, "DesigId", "DesigName", model.DesigId);
            ViewData["Shift"] = new SelectList(_context.Shifts.Where(x => x.ComId == comId), "ShiftId", "ShiftName");

            return View(model);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            Guid comId = Guid.Parse(Request.Cookies["comId"].ToString());
            var model = await _context.Employees
                                .Include(e=>e.Department)
                                .Include(e => e.Designation)
                                .Where(x=>x.EmpId==id && x.ComId==comId).FirstOrDefaultAsync();
        
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Employee model)
        {
            try
            {
                _context.Remove(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
            }
            return View(model);
            
        }

        private bool EmployeeExists(Guid id)
        {
          return (_context.Employees?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }

        
    }
}
