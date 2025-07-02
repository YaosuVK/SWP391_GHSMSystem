using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Categories;
using Service.RequestAndResponse.Request.ConsultantProfiles;
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
