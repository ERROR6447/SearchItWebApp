using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Models
{
    public class BookMark
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
       
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Postings Postings { get; set; }
       
    }
}
