// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;

namespace SearchItApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModelCompany : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IUnitOfWork _UnitOfWork; 

        public IndexModelCompany(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hostEnv,
            IUnitOfWork unitofWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnv = hostEnv;
            _UnitOfWork = unitofWork;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModelCompany Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModelCompany
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public string CompName { get; set; }
            public string? CompWebsite { get; set; }
            public string? CompEmail { get; set; }
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Country { get; set; }
            public string? PostalCode { get; set; }
            public string? PhoneNumber { get; set; }
            public string? TypeOfCompany { get; set; }


        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var company = _UnitOfWork.Company.GetFirstOrDefault(u=>u.Id == user.CompanyId);
            Username = userName;

            Input = new InputModelCompany
            {
                
                 CompName = company.CompName,
                 CompWebsite =  company.CompWebsite,
                 CompEmail =  company.CompEmail,
                 StreetAddress = company.StreetAddress,
                 City =  company.City,
                 State = company.State,
                 Country =  company.Country,
                 PostalCode = company.PostalCode,
                 PhoneNumber = company.PhoneNumber,
                 TypeOfCompany = company.TypeOfCompany,


            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            var user = await _userManager.GetUserAsync(User);


            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            //if (file != null)
            //{
            //    string rootpath = _hostEnv.WebRootPath;

            //    string fileName = Guid.NewGuid().ToString();
            //    var uploads = Path.Combine(rootpath, @"Images\Users");
            //    var extension = Path.GetExtension(file.FileName);
            //    if (user.ImageUrl != null)
            //    {
            //        var oldImagePath = Path.Combine(rootpath, user.ImageUrl.TrimStart('\\'));
            //        if (System.IO.File.Exists(oldImagePath))
            //        {
            //            System.IO.File.Delete(oldImagePath);
            //        }
            //    }
            //    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            //    {
            //        file.CopyTo(fileStream);
            //    }
            //    user.ImageUrl = @"\Images\Users\" + fileName + extension;

            //}
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}
            var company = _UnitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
            if(company == null)
            {
                return NotFound($"Unable to load Company with ID '{user.CompanyId}'.");

            }
                 company.CompName = company.CompName;
                 company.CompWebsite = company.CompWebsite;
                 company.CompEmail = company.CompEmail;
                 company.StreetAddress = company.StreetAddress;
                 company.City = company.City;
                 company.State = company.State;
                 company.Country = company.Country;
                company.PostalCode = company.PostalCode;
                 company.PhoneNumber = company.PhoneNumber;
                 company.TypeOfCompany = company.TypeOfCompany;
            
            _UnitOfWork.Company.Update(company);
            _UnitOfWork.Save();
        
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            
            StatusMessage = "Your Company Profile has been updated";
            return RedirectToPage();
        }
    }
}
