using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models
{
    [Keyless]
    public class AppliedFor
    {
        public int? PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public Postings Postings { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public DateTime? AppliedAt { get; set; } = DateTime.Now;
    }
}
