using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class ImageService
    {
        [Key]
        public int ImageServiceID { get; set; }

        [Required]
        public string? Image { get; set; }

        public int? ServicesID { get; set; }
        [ForeignKey("ServicesID")]
        public Services? Service { get; set; }
    }
}
