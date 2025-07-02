using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.Clinic
{
    public class UpdateClinicRequest
    {
        [Required(ErrorMessage = "Tên phòng khám không được để trống.")]
        [MaxLength(255, ErrorMessage = "Tên phòng khám không được vượt quá 255 ký tự.")]
        public string Name { get; set; }

        [MaxLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [MaxLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [EnumDataType(typeof(ClinicStatus), ErrorMessage = "Giá trị trạng thái không hợp lệ.")]
        public ClinicStatus Status { get; set; }
    }
}
