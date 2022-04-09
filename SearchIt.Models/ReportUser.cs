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
    public class ReportUser
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("CompanyId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
