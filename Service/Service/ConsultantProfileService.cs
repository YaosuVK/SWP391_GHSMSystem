using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.ConsultantProfiles;
using Service.RequestAndResponse.Response.Blogs;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.ConsultantProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ConsultantProfileService : IConsultantProfileServive
    {
        private readonly IMapper _mapper;
        private readonly IConsultantProfileRepository _consultantProfileRepository;
        private readonly IAccountRepository _accountRepository;

        public ConsultantProfileService(IMapper mapper, IConsultantProfileRepository consultantProfileRepository,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _consultantProfileRepository = consultantProfileRepository;
            _accountRepository = accountRepository;
        }

        public async Task<BaseResponse<CreateConsultantProfile>> CreateConsultantProfile(CreateConsultantProfile request)
        {
            var consultant = await _accountRepository.GetByStringId(request.AccountID);
            if (consultant == null)
            {
                return new BaseResponse<CreateConsultantProfile>("Missing Consultant, Please try again.", StatusCodeEnum.BadRequest_400, null);
            }

            if(request.ConsultantPrice <= 0)
            {
                return new BaseResponse<CreateConsultantProfile>("ConsultantPrice must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            ConsultantProfile consultantProfile = _mapper.Map<ConsultantProfile>(request);

            await _consultantProfileRepository.AddAsync(consultantProfile);

            var response = _mapper.Map<CreateConsultantProfile>(consultantProfile);
            return new BaseResponse<CreateConsultantProfile>("Create consultantProfile as base success", StatusCodeEnum.Created_201, response);
        }

        public async Task<BaseResponse<IEnumerable<GetAllConsultantProfile?>>> GetAllConsultantProfile()
        {
            IEnumerable<ConsultantProfile> consultantProfile = await _consultantProfileRepository.GetAllConsultantProfile();
            if (consultantProfile == null)
            {
                return new BaseResponse<IEnumerable<GetAllConsultantProfile>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var consultantProfiles = _mapper.Map<IEnumerable<GetAllConsultantProfile>>(consultantProfile);
            if (consultantProfiles == null)
            {
                return new BaseResponse<IEnumerable<GetAllConsultantProfile>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<GetAllConsultantProfile>>("Get all category as base success",
                StatusCodeEnum.OK_200, consultantProfiles);
        }

        public async Task<BaseResponse<GetConsultantProfileResponse?>> GetConsultantProfileByAccountID(string accountId)
        {
            ConsultantProfile? consultantProfile = await _consultantProfileRepository.GetConsultantProfileByAccountID(accountId);
            var result = _mapper.Map<GetConsultantProfileResponse>(consultantProfile);
            if (result == null)
            {
                return new BaseResponse<GetConsultantProfileResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<GetConsultantProfileResponse?>("Get ConsultantProfile as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<GetConsultantProfileResponse?>> GetConsultantProfileByID(int? consultantProfileID)
        {
            ConsultantProfile? consultantProfile = await _consultantProfileRepository.GetConsultantProfileByID(consultantProfileID);
            var result = _mapper.Map<GetConsultantProfileResponse>(consultantProfile);
            if (result == null)
            {
                return new BaseResponse<GetConsultantProfileResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<GetConsultantProfileResponse?>("Get ConsultantProfile as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<UpdateConsultantProfile>> UpdateConsultantProfile(int consultantProfileID, UpdateConsultantProfile request)
        {
            ConsultantProfile consultantProfileExist = await _consultantProfileRepository.GetConsultantProfileByID(consultantProfileID);

            if (consultantProfileExist == null)
            {
                return new BaseResponse<UpdateConsultantProfile>("Missing ConsultantProfile, Please try again.", StatusCodeEnum.BadRequest_400, null);
            }

            if (request.ConsultantPrice <= 0)
            {
                return new BaseResponse<UpdateConsultantProfile>("ConsultantPrice must > 0", StatusCodeEnum.BadRequest_400, null);
            }

            _mapper.Map(request, consultantProfileExist);

            await _consultantProfileRepository.UpdateAsync(consultantProfileExist);

            var result = _mapper.Map<UpdateConsultantProfile>(consultantProfileExist);

            return new BaseResponse<UpdateConsultantProfile>("Update Category as base success", StatusCodeEnum.OK_200, result);
        }
    }
}
