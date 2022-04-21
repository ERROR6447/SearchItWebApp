﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchIt.DataAccess.Repository.IRepository;
using SearchIt.Models;

namespace SearchItApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly IUnitOfWork _UnitOfWork; 

        public IndexModel(
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
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            
            
            public DateTime? Dob { get; set; }
            public string? Gender { get; set; }
            public string Highest_Qualification { get; set; }

            public int? Experience { get; set; }
            public string? LastPosition { get; set; }
            public string? ImageUrl { get; set; }
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? PostalCode { get; set; }
            public string? Country { get; set; }

            public string? AreaOfInterest { get; set; }
            public string? PreferredLocation { get; set; }
            public string? FullName { get; set; }
            public List<string> AreasOfInterests { get; set; }
            public List<string> KeySkills { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            ViewData["ListOfKeySkills"]  = _UnitOfWork.KeySkills.GetAll().Select(i => new SelectListItem
            {
                Value = i.Name,
                Text = i.Name,
            }).ToList();

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,

                Dob = user.Dob,
                Gender = user.Gender,
                Highest_Qualification = user.Highest_Qualification,
                Experience = user.Experience,
                LastPosition = user.LastPosition,
                ImageUrl = user.ImageUrl,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                
                PreferredLocation = user.PreferredLocation,
                Country = user.Country,
                FullName = user.FullName,
                AreasOfInterests = user.AreaOfInterest !=null?user.AreaOfInterest.Split(',').ToList():new List<string>(),
                KeySkills = user.KeySkills != null?user.KeySkills.Split(',').ToList():new List<string>(),
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
            string uploads;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            if (file != null)
            {
                string rootpath = _hostEnv.WebRootPath;

                string fileName = Guid.NewGuid().ToString();
                
                    uploads = Path.Combine(rootpath, @"Images\Users");

               
                
                var extension = Path.GetExtension(file.FileName);
                if (user.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(rootpath, user.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
               
                    user.ImageUrl = @"\Images\Users\" + fileName + extension;

               
                    

                

            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            user.Dob = Input.Dob;
            user.Gender = Input.Gender;
            user.Highest_Qualification = Input.Highest_Qualification;
            user.Experience = Input.Experience;
            user.LastPosition = Input.LastPosition;

            user.StreetAddress = Input.StreetAddress;
            user.City = Input.City;
            user.State = Input.State;
            user.PostalCode = Input.PostalCode;
            user.AreaOfInterest = string.Join(',', Input.AreasOfInterests);
            user.PreferredLocation = Input.PreferredLocation;
            user.Country = Input.Country;
            user.FullName = Input.FullName;
            user.KeySkills = String.Join(',', Input.KeySkills);
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
