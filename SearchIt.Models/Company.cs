using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]

        public string CompName { get; set; }
        [Display(Name = "Company Website")]

        public string? CompWebsite { get; set; }
        [Display(Name = "Company Email")]

        public string? CompEmail { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Postal Code")]

        public string? PostalCode { get; set; }
        [Display(Name = "Phone Number")]

        public string? PhoneNumber { get; set; }
        [Display(Name = "Type of Company")]

        public string? TypeOfCompany { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        public int retpostid {get;set;}
    }
}
