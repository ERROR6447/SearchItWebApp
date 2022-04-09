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
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Ratings { get; set; }
        public string? Comment { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public Postings Postings { get; set; }
    }
}
