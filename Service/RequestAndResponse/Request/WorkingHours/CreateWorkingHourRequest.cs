using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Request.WorkingHours
{
    public class CreateWorkingHourRequest
    {
        [Required(ErrorMessage = "ClinicID là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "ClinicID phải lớn hơn 0.")]
        public int ClinicID { get; set; }

        [Required(ErrorMessage = "Ngày trong tuần là bắt buộc.")]
        [EnumDataType(typeof(DayInWeek), ErrorMessage = "Giá trị DayInWeek không hợp lệ.")]
        public DayInWeek DayInWeek { get; set; }

        [Required(ErrorMessage = "Ca làm việc là bắt buộc.")]
        [EnumDataType(typeof(Shift), ErrorMessage = "Giá trị Shift không hợp lệ.")]
        public Shift Shift { get; set; }

        [Required(ErrorMessage = "Giờ bắt đầu làm việc là bắt buộc.")]
        public TimeOnly OpeningTime { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc làm việc là bắt buộc.")]
        public TimeOnly ClosingTime { get; set; }

        public bool Status { get; set; }
    }
}
