using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Projekt.Data
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }
        public string content { get; set; }
        public Post post { get; set; }
        public BlogUser author { get; set; }

        [ForeignKey("author")]
        public string authorFK { get; set; }
    }
}
