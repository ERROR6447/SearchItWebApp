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
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApplyId { get;set; }
        [ForeignKey("ApplyId")]
        [ValidateNever]
        public ApplyFor Apply { get; set; }
        [Required]
        [Display(Name = "Amount")]

        public double Amt { get; set; }
        [Display(Name = "Offer Description")]

        public string? OfferDescrip { get; set; }
    }
}
