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
    public class KeySkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeySkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KeySkills
        public async Task<IActionResult> Index()
        {
            return View(await _context.KeySkills.ToListAsync());
        }

        // GET: KeySkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySkills = await _context.KeySkills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keySkills == null)
            {
                return NotFound();
            }

            return View(keySkills);
        }

        // GET: KeySkills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KeySkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] KeySkills keySkills)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keySkills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keySkills);
        }

        // GET: KeySkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySkills = await _context.KeySkills.FindAsync(id);
            if (keySkills == null)
            {
                return NotFound();
            }
            return View(keySkills);
        }

        // POST: KeySkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] KeySkills keySkills)
        {
            if (id != keySkills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keySkills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeySkillsExists(keySkills.Id))
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
            return View(keySkills);
        }

        // GET: KeySkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keySkills = await _context.KeySkills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keySkills == null)
            {
                return NotFound();
            }

            return View(keySkills);
        }

        // POST: KeySkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keySkills = await _context.KeySkills.FindAsync(id);
            _context.KeySkills.Remove(keySkills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeySkillsExists(int id)
        {
            return _context.KeySkills.Any(e => e.Id == id);
        }
    }
}
