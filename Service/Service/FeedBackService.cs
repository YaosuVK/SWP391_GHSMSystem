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

                return new BaseResponse<GetAllFeedBackResponse>("Retrieved all feedbacks successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>($"Error retrieving feedbacks: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> GetFeedBackByIdAsync(int feedBackId)
        {
            try
            {
                var feedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (feedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse> ("FeedBack not found", StatusCodeEnum.NotFound_404, null);
                }

                var feedBackResponse = _mapper.Map<FeedBackResponse>(feedBack);
                var response = new GetFeedBackByIdResponse
                {
                    FeedBack = feedBackResponse
                };

                return new BaseResponse<GetFeedBackByIdResponse>("Retrieved feedback successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>($"Error retrieving feedbacks: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
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

                return new BaseResponse<GetAllFeedBackResponse>("Retrieved customer feedbacks successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>($"Error retrieving customer feedbacks: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
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

                return new BaseResponse<GetAllFeedBackResponse>("Retrieved appointment feedbacks successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetAllFeedBackResponse>($"Error retrieving appointment feedbacks: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> CreateFeedBackAsync(CreateFeedBackRequest request)
        {
            try
            {
                // Validate customer exists
                var customer = await _accountRepository.GetByStringId(request.CustomerID);
                if (customer == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Customer not found", StatusCodeEnum.NotFound_404, null);
                }

                // Validate appointment exists
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentID);
                if (appointment == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Appointment not found", StatusCodeEnum.NotFound_404, null);
                }

                // Check if customer already provided feedback for this appointment
                var existingFeedBack = await _feedBackRepository.ExistsFeedBackByCustomerAndAppointmentAsync(request.CustomerID, request.AppointmentID);
                if (existingFeedBack)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Customer has already provided feedback for this appointment", StatusCodeEnum.BadRequest_400, null);
                }

                // Validate rating range (1-5)
                if (request.ServiceRate < 1.0 || request.ServiceRate > 5.0 || 
                    request.FacilityRate < 1.0 || request.FacilityRate > 5.0)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Ratings must be between 1.0 and 5.0", StatusCodeEnum.BadRequest_400, null);
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

                return new BaseResponse<GetFeedBackByIdResponse>("Feedback created successfully", StatusCodeEnum.Created_201, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>($"Error creating feedback: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetFeedBackByIdResponse>> UpdateFeedBackAsync(int feedBackId, UpdateFeedBackRequest request)
        {
            try
            {
                var existingFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (existingFeedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Feedback not found", StatusCodeEnum.NotFound_404, null);
                }

                // Validate rating range (1-5)
                if (request.ServiceRate < 1.0 || request.ServiceRate > 5.0 || 
                    request.FacilityRate < 1.0 || request.FacilityRate > 5.0)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Ratings must be between 1.0 and 5.0", StatusCodeEnum.BadRequest_400, null);
                }

                // Map the updates
                _mapper.Map(request, existingFeedBack);
                existingFeedBack.FeedBackID = feedBackId; // Ensure ID is preserved

                var updatedFeedBack = await _feedBackRepository.UpdateFeedBackAsync(existingFeedBack);
                if (updatedFeedBack == null)
                {
                    return new BaseResponse<GetFeedBackByIdResponse>("Failed to update feedback", StatusCodeEnum.InternalServerError_500, null);
                }

                // Retrieve the updated feedback with related data
                var retrievedFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                var feedBackResponse = _mapper.Map<FeedBackResponse>(retrievedFeedBack);
                
                var response = new GetFeedBackByIdResponse
                {
                    FeedBack = feedBackResponse
                };

                return new BaseResponse<GetFeedBackByIdResponse>("Feedback updated successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetFeedBackByIdResponse>($"Error updating feedback: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> DeleteFeedBackAsync(int feedBackId)
        {
            try
            {
                var existingFeedBack = await _feedBackRepository.GetFeedBackByIdAsync(feedBackId);
                if (existingFeedBack == null)
                {
                    return new BaseResponse<string>("Feedback not found", StatusCodeEnum.NotFound_404, null);
                }

                var deleted = await _feedBackRepository.DeleteFeedBackAsync(feedBackId);
                if (!deleted)
                {
                    return new BaseResponse<string>("Failed to delete feedback", StatusCodeEnum.InternalServerError_500, null);
                }

                return new BaseResponse<string>("Feedback deleted successfully", StatusCodeEnum.OK_200, "Deleted");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>($"Error deleting feedback: {ex.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }
    }
} 