using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.LabTests;
using Service.RequestAndResponse.Response.LabTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class LabTestService : ILabTestService
    {
        private readonly ILabTestRepository _labTestRepository;
        private readonly IMapper _mapper;

        public LabTestService(ILabTestRepository labTestRepository, IMapper mapper)
        {
            _labTestRepository = labTestRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetAllLabTestsAsync()
        {
            try
            {
                var labTests = await _labTestRepository.GetAllAsync();
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Get all lab tests successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<GetLabTestByIdResponse>> GetLabTestByIdAsync(int id)
        {
            try
            {
                var labTest = await _labTestRepository.GetLabTestWithDetailsAsync(id);
                if (labTest == null)
                {
                    return new BaseResponse<GetLabTestByIdResponse>("Lab test not found", StatusCodeEnum.NotFound, null);
                }

                var response = _mapper.Map<GetLabTestByIdResponse>(labTest);
                return new BaseResponse<GetLabTestByIdResponse>("Get lab test successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetLabTestByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByCustomerIdAsync(string customerId)
        {
            try
            {
                var labTests = await _labTestRepository.GetLabTestsByCustomerIdAsync(customerId);
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Get lab tests by customer successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByStaffIdAsync(string staffId)
        {
            try
            {
                var labTests = await _labTestRepository.GetLabTestsByStaffIdAsync(staffId);
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Get lab tests by staff successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByTreatmentIdAsync(int treatmentId)
        {
            try
            {
                var labTests = await _labTestRepository.GetLabTestsByTreatmentIdAsync(treatmentId);
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Get lab tests by treatment successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> GetLabTestsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var labTests = await _labTestRepository.GetLabTestsByDateRangeAsync(fromDate, toDate);
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Get lab tests by date range successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllLabTestResponse>>> SearchLabTestsAsync(string? search, int pageIndex, int pageSize)
        {
            try
            {
                var labTests = await _labTestRepository.SearchLabTestsAsync(search, pageIndex, pageSize);
                var response = _mapper.Map<IEnumerable<GetAllLabTestResponse>>(labTests);
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>("Search lab tests successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllLabTestResponse>>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<GetLabTestByIdResponse>> CreateLabTestAsync(CreateLabTestRequest request)
        {
            try
            {
                var labTest = _mapper.Map<LabTest>(request);
                var createdLabTest = await _labTestRepository.AddAsync(labTest);
                var result = await _labTestRepository.GetLabTestWithDetailsAsync(createdLabTest.LabTestID);
                var response = _mapper.Map<GetLabTestByIdResponse>(result);
                return new BaseResponse<GetLabTestByIdResponse>("Create lab test successfully", StatusCodeEnum.Created, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetLabTestByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<GetLabTestByIdResponse>> UpdateLabTestAsync(UpdateLabTestRequest request)
        {
            try
            {
                var existingLabTest = await _labTestRepository.GetByIdAsync(request.LabTestID);
                if (existingLabTest == null)
                {
                    return new BaseResponse<GetLabTestByIdResponse>("Lab test not found", StatusCodeEnum.NotFound, null);
                }

                _mapper.Map(request, existingLabTest);
                await _labTestRepository.UpdateAsync(existingLabTest);
                var result = await _labTestRepository.GetLabTestWithDetailsAsync(request.LabTestID);
                var response = _mapper.Map<GetLabTestByIdResponse>(result);
                return new BaseResponse<GetLabTestByIdResponse>("Update lab test successfully", StatusCodeEnum.OK, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetLabTestByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }

        public async Task<BaseResponse<string>> DeleteLabTestAsync(int id)
        {
            try
            {
                var labTest = await _labTestRepository.GetByIdAsync(id);
                if (labTest == null)
                {
                    return new BaseResponse<string>("Lab test not found", StatusCodeEnum.NotFound, null);
                }

                await _labTestRepository.DeleteAsync(labTest);
                return new BaseResponse<string>("Delete lab test successfully", StatusCodeEnum.OK, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(ex.Message, StatusCodeEnum.InternalServerError, null);
            }
        }
    }
} 