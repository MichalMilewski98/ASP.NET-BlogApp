using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Projekt.Data
{
    public class Post
    {
        [Key]
        public int postId { get; set; }


        [StringLength(10, MinimumLength = 3)]
        [Required]
        public string title { get; set; }

        [StringLength(900, MinimumLength = 5)]
        [Required]
        public string content { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime date { get; set; }

        public ICollection<Comment> comments { get; set; } = new List<Comment>();

        public BlogUser author { get; set; }

        [ForeignKey("author")]
        public string authorFK { get; set; }

    }
}
