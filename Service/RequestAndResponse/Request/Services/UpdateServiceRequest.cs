using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Services
{
    public class UpdateServiceRequest
    {
        public int ServicesID { get; set; }

        [MaxLength(1000, ErrorMessage = "Tên dịch vụ không được vượt quá 1000 ký tự.")]
        public string ServicesName { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string Description { get; set; }

        public double? ServicesPrice { get; set; }

        public ServiceType? ServiceType { get; set; }

        public bool Status { get; set; }
    }
}
