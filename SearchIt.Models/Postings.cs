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
        public string PostLocation { get; set; }
        public string MinExp { get; set; }
        public int TotalVacancies { get; set; }
        public double PostSal { get; set; }
        public int PostLike { get; set; }
        public int PostDisLike { get; set; }
        public int? CompanyId { get;set; } 
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public string TypeOfJob { get; set; }
        

    }
}
