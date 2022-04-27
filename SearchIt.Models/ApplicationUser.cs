
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [Display(Name ="Full Name")]
        public string? FullName { get; set; }
        [Display(Name ="Date Of Birth")]

        public DateTime? Dob { get; set; }
        public  string? Gender { get; set; }
        [Display(Name = "Highest Qualification")]

        public string Highest_Qualification { get; set; }
        public int ? Experience { get; set; }
        [Display(Name ="Last Worked as")]
        public string? LastPosition { get; set; }
        [ValidateNever]
        [Display(Name ="Image Upload")]
        public string? ImageUrl { get; set; }
        [Display(Name ="CV Upload")]

        public string? CvUrl { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        [Display(Name = "Phone Number")]

        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Areas Of Interests")]

        public string? AreaOfInterest { get; set; }
        [Display(Name ="Key Skills")]
        public string? KeySkills { get; set; }
        [Display(Name="Preferred Location")]
        public string? PreferredLocation { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public DateTime? JoinedAt { get; set; } = DateTime.Now;
        
    }
}
