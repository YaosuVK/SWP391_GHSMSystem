using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.WorkingHours
{
    public class UpdateWorkingHourRequest
    {
        [Required(ErrorMessage = "Ca làm việc là bắt buộc.")]
        [EnumDataType(typeof(Shift), ErrorMessage = "Giá trị Shift không hợp lệ.")]
        public Shift Shift { get; set; }

        /*[Required(ErrorMessage = "Giờ bắt đầu làm việc là bắt buộc.")]
        public TimeOnly OpeningTime { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc làm việc là bắt buộc.")]
        public TimeOnly ClosingTime { get; set; }*/

        [Required(ErrorMessage = "Giờ bắt đầu làm việc là bắt buộc.")]
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "Giờ bắt đầu phải theo định dạng HH:mm.")]
        public string OpeningTime { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc làm việc là bắt buộc.")]
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "Giờ kết thúc phải theo định dạng HH:mm.")]
        public string ClosingTime { get; set; }

        public bool Status { get; set; }
    }
}
