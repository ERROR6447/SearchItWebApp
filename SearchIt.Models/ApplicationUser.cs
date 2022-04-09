
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
        public string? UserName { get; set; }
        
        public DateTime? Dob { get; set; }
        public  string? Gender { get; set; }
        public string Highest_Qualification { get; set; }
        public int ? Experience { get; set; }
        public string? LastPosition { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        public string? CvUrl { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AreaOfInterest { get; set; }
        public string? PreferredLocation { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public DateTime? JoinedAt { get; set; } = DateTime.Now;

    }
}
