using BusinessObject.Model;
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

        public double? ServicesPrice { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

        public bool Status { get; set; }
    }
}
