using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "Post Title")]

        public string PostName { get; set; }
        [Display(Name = "Description")]

        public string PostDescription { get; set; }
        [Display(Name = "Minimum Experience")]

        public string MinExp { get; set; }
        [Display(Name = "Openings")]

        public int TotalVacancies { get; set; }
        [Display(Name = "Minimum Salary")]

        public double PostMinSal { get; set; }
        [Display(Name = "Maximum Salary")]

        public double PostMaxSal { get; set; }
        [Display(Name = "Likes")]

        public int PostLike { get; set; } = 0;
        [Display(Name = "DisLikes")]

        public int PostDisLike { get; set; } = 0;
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        [Display(Name = "Type of Job")]

        public string TypeOfJob { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        [Display(Name = "Phone Number")]

        public string? PhoneNumber { get; set; }
        [Display(Name ="Required Skills")]
        public string? ReqSkills { get; set; }
        [NotMapped]
        [ValidateNever]
        public List<SelectListItem> ReqSkillsList { get; set; }
        [NotMapped]
        [ValidateNever]

        public List<string> SelectedReqSkillList { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
    }
}
