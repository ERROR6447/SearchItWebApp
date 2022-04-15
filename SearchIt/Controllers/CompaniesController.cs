#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchIt.DataAccess.Data;
using SearchIt.Models;


namespace SearchItApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Companies == null)
            {
                return NotFound();
            }

            return View(Companies);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompName,CompWebsite,StreetAddress,City,State,PostalCode,PhoneNumber,Country,CompEmail,TypeOfCompany")] Company Companies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Companies);
                await _context.SaveChangesAsync();
                var id = Companies.Id;
                return LocalRedirect($"/Identity/Account/Register?CompId={id}");
            }
            
           return View(Companies);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Companies = await _context.Companies.FindAsync(id);
            if (Companies == null)
            {
                return NotFound();
            }
            return View(Companies);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompName,CompWebsite,StreetAddress,City,State,PostalCode,PhoneNumber,Country,CompEmail,TypeOfCompany")] Company Companies)
        {
            if (id != Companies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Companies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesExists(Companies.Id))
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
            return View(Companies);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Companies = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Companies == null)
            {
                return NotFound();
            }

            return View(Companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Companies = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(Companies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompaniesExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
