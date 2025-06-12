using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [ForeignKey("ClinicID")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }

        public string Name { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public bool Status { get; set; }

        public ICollection<Services> Services { get; set; }
    }
}
