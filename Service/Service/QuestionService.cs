using AutoMapper;
using BusinessObject.Model;
using DataAccessObject;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Question;
using Service.RequestAndResponse.Response.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repo;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        public QuestionService(IQuestionRepository repo, UserManager<Account> um, IMapper mapper) 
        { _repo = repo; 
          _userManager = um;
          _mapper = mapper;
        }

        public async Task<BaseResponse<Question>> CreateQuestionAsync(string customerId, CreateQuestionRequest req)
        {
            var user = await _userManager.FindByIdAsync(customerId);
            if (user == null) return new BaseResponse<Question>("Customer not found", StatusCodeEnum.NotFound_404, null);

            var q = new Question { CustomerID = customerId, Content = req.Content, CreatedAt = System.DateTime.UtcNow };
            var added = await _repo.AddAsync(q);
            return new BaseResponse<Question>("Question created", StatusCodeEnum.Created_201, added);
        }

        /*public async Task<BaseResponse<IEnumerable<Question>>> GetAllQuestionsAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Any() ? new BaseResponse<IEnumerable<Question>>("Fetched", StatusCodeEnum.OK_200, list)
                               : new BaseResponse<IEnumerable<Question>>("No questions", StatusCodeEnum.NotFound_404, null);
        }*/

        public async Task<BaseResponse<IEnumerable<GetAllQuestionResponse>>> GetAllQuestionsAsync()
        {
            var list = await _repo.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<GetAllQuestionResponse>>(list);

            return list.Any() ? new BaseResponse<IEnumerable<GetAllQuestionResponse>>("Fetched", StatusCodeEnum.OK_200, mapped)
                               : new BaseResponse<IEnumerable<GetAllQuestionResponse>>("No questions", StatusCodeEnum.NotFound_404, null);
        }

        public async Task<BaseResponse<Question>> GetQuestionByIdAsync(int id)
        {
            var q = await _repo.GetByIdAsync(id);
            return q != null ? new BaseResponse<Question>("Fetched", StatusCodeEnum.OK_200, q)
                             : new BaseResponse<Question>("Not found", StatusCodeEnum.NotFound_404, null);
        }
    }
}
