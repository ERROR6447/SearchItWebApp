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
    public class Postings
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PostName { get; set; }
        public string PostDescription { get; set; }

        public string MinExp { get; set; }
        public int TotalVacancies { get; set; }
        public double PostMinSal { get; set; }
        public double PostMaxSal { get; set; }
        public int PostLike { get; set; } = 0;
        public int PostDisLike { get; set; } = 0;
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public string TypeOfJob { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
