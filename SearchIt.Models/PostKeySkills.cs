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
    public class PostKeySkills
    {
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        [ValidateNever]
        public KeySkills KeySkills { get; set; }
    }
}
