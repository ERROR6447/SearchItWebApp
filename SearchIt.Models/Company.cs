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
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        public int retpostid {get;set;}
    }
}
