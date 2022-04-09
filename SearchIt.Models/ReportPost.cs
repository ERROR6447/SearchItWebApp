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
    public class ReportPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
      
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        
        [ValidateNever]
        public ApplicationUser User { get; set; }
        
        
        public int? PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public Postings Postings { get; set; }
    }
}
