using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class ImageBlog
    {
        [Key]
        public int ImageBlogID { get; set; }

        [Required]
        public string? Image { get; set; }

        public int? BlogID { get; set; }
        [ForeignKey("BlogID")]
        public Blog? Blog { get; set; }
    }
}
