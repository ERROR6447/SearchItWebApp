#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;

namespace SearchItApp.Controllers
{
    public class PostingsController : Controller
    {
        private readonly IUnitOfWork _context;

        public PostingsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Postings
        public async Task<IActionResult> Index()
        {
            var searchItAppContext= _context.Postings.GetAll(includeProperties:"Company").ToList();
            
            return View(searchItAppContext);
        }

        // GET: Postings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postings = _context.Postings.GetFirstOrDefault(u=>u.Id == id,includeProperties:"Company");

            //var postings = await _context.Postings
            //    .Include(p => p.Company)
           //     .FirstOrDefaultAsync(m => m.Id == id);
            if (postings == null)
            {
                return NotFound();
            }

            return View(postings);
        }

        // GET: Postings/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = _context.Company.GetAll().Select(u => new SelectListItem
            {
                Text=u.CompName,
                Value=u.Id.ToString(),
            });
            return View();
        }

        // POST: Postings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostName,PostDescription,MinExp,TotalVacancies,PostMinSal,PostMaxSal,PostLike,PostDisLike,CompanyId,TypeOfJob,StreetAddress,City,State,PostalCode,PhoneNumber,CreatedAt")] Postings postings)
        {
            if (ModelState.IsValid)
            {
                _context.Postings.Add(postings);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = _context.Company.GetAll().Select(u => new SelectListItem
            {
                Text = u.CompName,
                Value = u.Id.ToString(),
            });
            return View(postings);
        }

        // GET: Postings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postings = _context.Postings.GetFirstOrDefault(u=> u.Id == id);
            if (postings == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = _context.Company.GetAll().Select(u => new SelectListItem
            {
                Text = u.CompName,
                Value = u.Id.ToString(),
            });
            return View(postings);
        }

        // POST: Postings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostName,PostDescription,MinExp,TotalVacancies,PostMinSal,PostMaxSal,PostLike,PostDisLike,CompanyId,TypeOfJob,StreetAddress,City,State,PostalCode,PhoneNumber,CreatedAt")] Postings postings)
        {
            if (id != postings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Postings.Update(postings);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostingsExists(postings.Id))
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
            ViewData["CompanyId"] = _context.Company.GetAll().Select(u => new SelectListItem
            {
                Text = u.CompName,
                Value = u.Id.ToString(),
            });
            return View(postings);
        }

        // GET: Postings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var postings = _context.Postings.GetFirstOrDefault(u => u.Id == id, includeProperties: "Company");
            //var postings = await _context.Postings
            //    .Include(p => p.Company)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (postings == null)
            {
                return NotFound();
            }

            return View(postings);
        }

        // POST: Postings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postings = _context.Postings.GetFirstOrDefault(u => u.Id == id);    
            //var postings = await _context.Postings.FindAsync(id);
            _context.Postings.Remove(postings);
             _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PostingsExists(int id)
        {
            if(_context.Postings.GetFirstOrDefault(e => e.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public void Like(int cartId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == cartId);
            _context.Postings.IncrementLike(post, 1);
            _context.Save();

        }
        public void DisLike(int cartId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == cartId);
            _context.Postings.IncrementDisLike(post, 1);
            _context.Save();

        }
        public void DecLike(int cartId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == cartId);
            _context.Postings.DecrementLike(post, 1);
            _context.Save();

        }
        public void DecDisLike(int cartId)
        {
            Postings post = _context.Postings.GetFirstOrDefault(u => u.Id == cartId);
            _context.Postings.DecrementDisLike(post, 1);
            _context.Save();

        }

        public IActionResult PostDetails(int id)
        {
            Postings post = _context.Postings.GetFirstOrDefault(i => i.Id == id, includeProperties: "Company");

            return View(post);
        }
    }
}
