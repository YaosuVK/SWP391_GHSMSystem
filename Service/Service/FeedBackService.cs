using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.FeedBacks;
using Service.RequestAndResponse.Response.FeedBacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    /// <summary>
    /// FeedBack Service implementation with comprehensive business logic
    /// Handles CRUD operations, validation, and auto-calculation for feedback system
    /// </summary>
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public FeedBackService(IFeedBackRepository feedBackRepository, IAccountRepository accountRepository, 
            IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _accountRepository = accountRepository;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GetAllFeedBackResponse>> GetAllFeedBacksAsync()
        {
            try
            {
                var feedBacks = await _feedBackRepository.GetAllFeedBacksAsync();
                var feedBackResponses = _mapper.Map<List<FeedBackResponse>>(feedBacks);

                var response = new GetAllFeedBackResponse
                {
                    FeedBacks = feedBackResponses,
                    TotalCount = feedBackResponses.Count
                };

                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Retrieved all feedbacks successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error retrieving feedbacks: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> GetFeedBackByIdAsync(int feedBackId)
        {
            try
            {
                var feedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (feedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.NotFound_404,
                        IsSuccess = false,
                        Message = "Feedback not found",
                        Data = null
                    };
                }

                var feedBackResponse = _mapper.Map<FeedBackResponse>(feedBack);
                var response = new GetFeedBackByIdResponse
                {
                    FeedBack = feedBackResponse
                };

                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Retrieved feedback successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error retrieving feedback: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<GetAllFeedBackResponse>> GetFeedBacksByCustomerIdAsync(string customerId)
        {
            try
            {
                var feedBacks = await _feedBackRepository.GetFeedBacksByCustomerIdAsync(customerId);
                var feedBackResponses = _mapper.Map<List<FeedBackResponse>>(feedBacks);

                var response = new GetAllFeedBackResponse
                {
                    FeedBacks = feedBackResponses,
                    TotalCount = feedBackResponses.Count
                };

                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Retrieved customer feedbacks successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error retrieving customer feedbacks: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<GetAllFeedBackResponse>> GetFeedBacksByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                var feedBacks = await _feedBackRepository.GetFeedBacksByAppointmentIdAsync(appointmentId);
                var feedBackResponses = _mapper.Map<List<FeedBackResponse>>(feedBacks);

                var response = new GetAllFeedBackResponse
                {
                    FeedBacks = feedBackResponses,
                    TotalCount = feedBackResponses.Count
                };

                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Retrieved appointment feedbacks successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error retrieving appointment feedbacks: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> CreateFeedBackAsync(CreateFeedBackRequest request)
        {
            try
            {
                // Validate customer exists
                var customer = await _accountRepository.GetByAccountIdAsync(request.CustomerID);
                if (customer == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.NotFound_404,
                        IsSuccess = false,
                        Message = "Customer not found",
                        Data = null
                    };
                }

                // Validate appointment exists
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentID);
                if (appointment == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.NotFound_404,
                        IsSuccess = false,
                        Message = "Appointment not found",
                        Data = null
                    };
                }

                // Check if customer already provided feedback for this appointment
                var existingFeedBack = await _feedBackRepository.ExistsFeedBackByCustomerAndAppointmentAsync(request.CustomerID, request.AppointmentID);
                if (existingFeedBack)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.BadRequest_400,
                        IsSuccess = false,
                        Message = "Customer has already provided feedback for this appointment",
                        Data = null
                    };
                }

                // Validate rating range (1-5)
                if (request.ServiceRate < 1.0 || request.ServiceRate > 5.0 || 
                    request.FacilityRate < 1.0 || request.FacilityRate > 5.0)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.BadRequest_400,
                        IsSuccess = false,
                        Message = "Ratings must be between 1.0 and 5.0",
                        Data = null
                    };
                }

                var feedBack = _mapper.Map<FeedBack>(request);
                var createdFeedBack = await _feedBackRepository.CreateFeedBackAsync(feedBack);

                // Retrieve the created feedback with related data
                var retrievedFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(createdFeedBack.FeedBackID);
                var feedBackResponse = _mapper.Map<FeedBackResponse>(retrievedFeedBack);
                
                var response = new GetFeedBackByIdResponse
                {
                    FeedBack = feedBackResponse
                };

                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.Created_201,
                    IsSuccess = true,
                    Message = "Feedback created successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error creating feedback: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> UpdateFeedBackAsync(int feedBackId, UpdateFeedBackRequest request)
        {
            try
            {
                var existingFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (existingFeedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.NotFound_404,
                        IsSuccess = false,
                        Message = "Feedback not found",
                        Data = null
                    };
                }

                // Validate rating range (1-5)
                if (request.ServiceRate < 1.0 || request.ServiceRate > 5.0 || 
                    request.FacilityRate < 1.0 || request.FacilityRate > 5.0)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.BadRequest_400,
                        IsSuccess = false,
                        Message = "Ratings must be between 1.0 and 5.0",
                        Data = null
                    };
                }

                // Map the updates
                _mapper.Map(request, existingFeedBack);
                existingFeedBack.FeedBackID = feedBackId; // Ensure ID is preserved

                var updatedFeedBack = await _feedBackRepository.UpdateFeedBackAsync(existingFeedBack);
                if (updatedFeedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>
                    {
                        StatusCode = StatusCodeEnum.InternalServerError_500,
                        IsSuccess = false,
                        Message = "Failed to update feedback",
                        Data = null
                    };
                }

                // Retrieve the updated feedback with related data
                var retrievedFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                var feedBackResponse = _mapper.Map<FeedBackResponse>(retrievedFeedBack);
                
                var response = new GetFeedBackByIdResponse
                {
                    FeedBack = feedBackResponse
                };

                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Feedback updated successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error updating feedback: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<object>> DeleteFeedBackAsync(int feedBackId)
        {
            try
            {
                var existingFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (existingFeedBack == null)
                {
                    return new BaseResponse<object>
                    {
                        StatusCode = StatusCodeEnum.NotFound_404,
                        IsSuccess = false,
                        Message = "Feedback not found",
                        Data = null
                    };
                }

                var deleted = await _feedBackRepository.DeleteFeedBackAsync(feedBackId);
                if (!deleted)
                {
                    return new BaseResponse<object>
                    {
                        StatusCode = StatusCodeEnum.InternalServerError_500,
                        IsSuccess = false,
                        Message = "Failed to delete feedback",
                        Data = null
                    };
                }

                return new BaseResponse<object>
                {
                    StatusCode = StatusCodeEnum.OK_200,
                    IsSuccess = true,
                    Message = "Feedback deleted successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<object>
                {
                    StatusCode = StatusCodeEnum.InternalServerError_500,
                    IsSuccess = false,
                    Message = $"Error deleting feedback: {ex.Message}",
                    Data = null
                };
            }
        }
    }
} 