using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ClinicService : IClinicService
    {
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;

        public ClinicService(IMapper mapper, IClinicRepository clinicRepository)
        {
            _mapper = mapper;
            _clinicRepository = clinicRepository;
        }

        public async Task<BaseResponse<Clinic>> CreateClinic(CreateClinicRequest request)
        {
            Clinic clinic = _mapper.Map<Clinic>(request);
            clinic.CreateAt = DateTime.Now;
            clinic.UpdateAt = DateTime.Now;
            await _clinicRepository.AddAsync(clinic);

            return new BaseResponse<Clinic>("Create category as base success", StatusCodeEnum.Created_201, clinic);
        }

        public async Task<BaseResponse<Clinic?>> GetClinicById(int clinicId)
        {
            var clinic = await _clinicRepository.GetClinicById(clinicId);
            if (clinic == null)
            {
                return new BaseResponse<Clinic?>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }

            return new BaseResponse<Clinic?>("Get all bookings as base success",
                StatusCodeEnum.OK_200, clinic);
        }

        public async Task<BaseResponse<Clinic>> UpdateClinic(int clinicID, UpdateClinicRequest request)
        {
            var clinicExist = await _clinicRepository.GetByIdAsync(clinicID);
            if (clinicExist == null)
            {
                return new BaseResponse<Clinic>($"Không tìm thấy phòng khám với ID = {clinicID}", StatusCodeEnum.NotFound_404, null);
            }

            // 2. Map dữ liệu từ request vào entity đã tồn tại
            _mapper.Map(request, clinicExist);
            clinicExist.UpdateAt = DateTime.UtcNow;
            
            // 3. Cập nhật
            await _clinicRepository.UpdateAsync(clinicExist);

            // 4. Trả về kết quả
            return new BaseResponse<Clinic>( "Cập nhật phòng khám thành công.", StatusCodeEnum.OK_200, clinicExist );
        }
    }
}
