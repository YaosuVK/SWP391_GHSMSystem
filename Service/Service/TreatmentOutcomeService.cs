using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.TreatmentOutcomes;
using Service.RequestAndResponse.Response.TreatmentOutcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TreatmentOutcomeService : ITreatmentOutcomeService
    {
        private readonly ITreatmentOutcomeRepository _treatmentOutcomeRepository;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public TreatmentOutcomeService(ITreatmentOutcomeRepository treatmentOutcomeRepository, IMapper mapper,
            ILabTestRepository labTestRepository, IAppointmentRepository appointmentRepository)
        {
            _treatmentOutcomeRepository = treatmentOutcomeRepository;
            _mapper = mapper;
            _labTestRepository = labTestRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetAllTreatmentOutcomesAsync()
        {
            try
            {
                var treatmentOutcomes = await _treatmentOutcomeRepository.GetAllAsync();
                var response = _mapper.Map<IEnumerable<GetAllTreatmentOutcomeResponse>>(treatmentOutcomes);
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>("Get all treatment outcomes successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> GetTreatmentOutcomeByIdAsync(int id)
        {
            try
            {
                var treatmentOutcome = await _treatmentOutcomeRepository.GetTreatmentOutcomeWithDetailsAsync(id);
                if (treatmentOutcome == null)
                {
                    return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Treatment outcome not found", StatusCodeEnum.NotFound_404, null);
                }

                var response = _mapper.Map<GetTreatmentOutcomeByIdResponse>(treatmentOutcome);
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Get treatment outcome successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByCustomerIdAsync(string customerId)
        {
            try
            {
                var treatmentOutcomes = await _treatmentOutcomeRepository.GetTreatmentOutcomesByCustomerIdAsync(customerId);
                var response = _mapper.Map<IEnumerable<GetAllTreatmentOutcomeResponse>>(treatmentOutcomes);
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>("Get treatment outcomes by customer successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByConsultantIdAsync(string consultantId)
        {
            try
            {
                var treatmentOutcomes = await _treatmentOutcomeRepository.GetTreatmentOutcomesByConsultantIdAsync(consultantId);
                var response = _mapper.Map<IEnumerable<GetAllTreatmentOutcomeResponse>>(treatmentOutcomes);
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>("Get treatment outcomes by consultant successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> GetTreatmentOutcomesByAppointmentIdAsync(int appointmentId)
        {
            try
            {
                var treatmentOutcomes = await _treatmentOutcomeRepository.GetTreatmentOutcomesByAppointmentIdAsync(appointmentId);
                var response = _mapper.Map<IEnumerable<GetAllTreatmentOutcomeResponse>>(treatmentOutcomes);
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>("Get treatment outcomes by appointment successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>> SearchTreatmentOutcomesAsync(string? search, int pageIndex, int pageSize)
        {
            try
            {
                var treatmentOutcomes = await _treatmentOutcomeRepository.SearchTreatmentOutcomesAsync(search, pageIndex, pageSize);
                var response = _mapper.Map<IEnumerable<GetAllTreatmentOutcomeResponse>>(treatmentOutcomes);
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>("Search treatment outcomes successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<GetAllTreatmentOutcomeResponse>>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> CreateTreatmentOutcomeAsync(CreateTreatmentOutcomeRequest request)
        {
            try
            {
                var treatmentOutcome = _mapper.Map<TreatmentOutcome>(request);
                
                treatmentOutcome.CreateAt = DateTime.Now;
                treatmentOutcome.UpdateAt = DateTime.Now;

                var labTests = await _labTestRepository.GetUnassignedLabTestsByCustomerId(treatmentOutcome.CustomerID);

                if (labTests != null)
                {
                    treatmentOutcome.LabTests = labTests;
                }

                var createdTreatmentOutcome = await _treatmentOutcomeRepository.AddAsync(treatmentOutcome);

                var appointment = await _appointmentRepository.GetAppointmentByIdCanNullAsync(request.AppointmentID);
                if (appointment == null)
                {
                    return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Appointment not found", StatusCodeEnum.NotFound_404, null);
                }

                if(appointment.TreatmentID != null)
                {
                    return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Hey, Already have Treatment Outcome, Cannot Create!", StatusCodeEnum.NotFound_404, null);
                }

                appointment.TreatmentID = createdTreatmentOutcome.TreatmentID;
                await _appointmentRepository.UpdateAsync(appointment);

                var result = await _treatmentOutcomeRepository.GetTreatmentOutcomeWithDetailsAsync(createdTreatmentOutcome.TreatmentID);
                var response = _mapper.Map<GetTreatmentOutcomeByIdResponse>(result);
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Create treatment outcome successfully", StatusCodeEnum.Created_201, response);
            }
            catch (Exception ex)
            {
                string fullError = $"{ex.Message}";
                if (ex.InnerException != null)
                {
                    fullError += $" | Inner: {ex.InnerException.Message}";
                }
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>(fullError, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<GetTreatmentOutcomeByIdResponse>> UpdateTreatmentOutcomeAsync(UpdateTreatmentOutcomeRequest request)
        {
            try
            {
                var existingTreatmentOutcome = await _treatmentOutcomeRepository.GetTreatmenOutComeByTreatmentIdAsync(request.TreatmentID);
                if (existingTreatmentOutcome == null)
                {
                    return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Treatment outcome not found", StatusCodeEnum.NotFound_404, null);
                }

                _mapper.Map(request, existingTreatmentOutcome);

                var labTests = await _labTestRepository.GetUnassignedLabTestsByCustomerId(existingTreatmentOutcome.CustomerID);

                if (labTests != null && labTests.Any())
                {
                    if (existingTreatmentOutcome.LabTests == null)
                    {
                        existingTreatmentOutcome.LabTests = new List<LabTest>();
                    }

                    foreach (var labTest in labTests)
                    {
                        existingTreatmentOutcome.LabTests.Add(labTest);
                    }
                }

                await _treatmentOutcomeRepository.UpdateAsync(existingTreatmentOutcome);
                var result = await _treatmentOutcomeRepository.GetTreatmentOutcomeWithDetailsAsync(request.TreatmentID);
                var response = _mapper.Map<GetTreatmentOutcomeByIdResponse>(result);
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>("Update treatment outcome successfully", StatusCodeEnum.OK_200, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<GetTreatmentOutcomeByIdResponse>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> DeleteTreatmentOutcomeAsync(int id)
        {
            try
            {
                var treatmentOutcome = await _treatmentOutcomeRepository.GetByIdAsync(id);
                if (treatmentOutcome == null)
                {
                    return new BaseResponse<string>("Treatment outcome not found", StatusCodeEnum.NotFound_404, null);
                }

                await _treatmentOutcomeRepository.DeleteAsync(treatmentOutcome);
                return new BaseResponse<string>("Delete treatment outcome successfully", StatusCodeEnum.OK_200, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(ex.Message, StatusCodeEnum.InternalServerError_500, null);
            }
        }
    }
} 