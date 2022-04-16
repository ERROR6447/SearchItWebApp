﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;
using SearchIt.Models.ViewModels;

namespace SearchItApp.Controllers
{
    public class PostingsController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> _UserManager;
        public PostingsController(IUnitOfWork context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _UserManager = userManager;

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

        public IActionResult AllPosts()
        {
            var UserId = _UserManager.GetUserId(User);
            var UserInfo= _context.ApplicationUser.GetFirstOrDefault(x=>x.Id == UserId);
            ApplicationUser CurUser = _context.ApplicationUser.GetFirstOrDefault(x => x.Id == UserInfo.Id);
            var AllPosts = _context.Postings.GetAll(x => x.CompanyId == CurUser.CompanyId, includeProperties: "Company");
            IEnumerable<AllPostingsVIewModel> AllCompPosts = from posts in AllPosts
                                                             where posts.CompanyId == CurUser.CompanyId
                                                             select(new AllPostingsVIewModel()
                                                             {
                                                                 Post = posts,
                                                                 Responses = _context.Apply.GetAll(x => x.PostId == posts.Id).Count(),
                                                             });

            return View(AllCompPosts);


        }

        public IActionResult ViewResponses(int id)
        {
            
            return View(id);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetResponses(int id)
        {
            IEnumerable<ApplyFor> AllResponses = _context.Apply.GetAll(u=>u.PostId == id,includeProperties: "Postings,User") ;

            return Json(new { data=AllResponses });
        }

        #endregion 

        [HttpDelete]
        public IActionResult RejectApplicant(int id)
        {
            ApplyFor post = _context.Apply.GetFirstOrDefault(u => u.ApplyId == id);
            post.ApplyStatus = "Rejected";
            _context.Apply.Update(post);
            _context.Save();
            return Json(new { success = true, message = "Rejected Successfully" });
        }





    }
}
