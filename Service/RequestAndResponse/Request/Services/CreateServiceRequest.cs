using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Services
{
    public class CreateServiceRequest
    {
        public int ClinicID { get; set; }

        public int CategoryID { get; set; }

        public string ManagerID { get; set; }

        [MaxLength(1000, ErrorMessage = "Tên dịch vụ không được vượt quá 1000 ký tự.")]
        public string ServicesName { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string Description { get; set; }

        [Range(1000, double.MaxValue, ErrorMessage = "Giá dịch vụ phải từ 1.000 VND trở lên.")]
        public double ServicesPrice { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

        public bool Status { get; set; }
    }
}
