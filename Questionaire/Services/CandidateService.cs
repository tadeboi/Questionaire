using Microsoft.EntityFrameworkCore;
using Questionaire.Domain;
using Questionaire.DTOs;
using Questionaire.Helper;
using Questionaire.Infra;
using Questionaire.Interfaces;

namespace Questionaire.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;
        private static bool _ensureCreated { get; set; } = false;
        public CandidateService(AppDbContext context)
        {
            _context = context;

            if (!_ensureCreated)
            {
            _context.Database.EnsureCreated();
            _ensureCreated = true;
            }
        }

        public async Task<BaseResponse> GetQuestions(Guid programId){

            var program = await _context.Programs.FirstOrDefaultAsync(p => p.Id == programId);
            
            if(program != null){

                return new BaseResponse(true, program.Questions);
            }

            return new BaseResponse(false, "This program does not exist");
        }

        public async Task<BaseResponse> SubmitResponse(List<CandidateResponseDTO> candidateResponseDTOs){
            try
            {
                foreach (var candidateResponse in candidateResponseDTOs){
                    var program = await _context.Programs.FirstOrDefaultAsync(q => q.Id == candidateResponse.ProgramId);

                    if(program != null){
                        var question = program.Questions.FirstOrDefault(q => q.Id == candidateResponse.QuestionId);

                        if (question.QuestionType == QuestionType.Date && !(candidateResponse.Response is DateTime)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.Dropdown && !(candidateResponse.Response is string)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.MultipleChoice && !(candidateResponse.Response is List<string>)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.Number && !(candidateResponse.Response is int)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.Paragraph && !(candidateResponse.Response is string)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.YesNo && !(candidateResponse.Response is bool)){
                            return new BaseResponse(false, "Bad Request");
                        }
                        else if (question.QuestionType == QuestionType.MultipleChoice){
                            var response = (List<string>?)candidateResponse.Response;
                            if(response.Count > question.MaxChoices){
                                return new BaseResponse(false, "Bad Request");
                            }
                        }
                    }
                    var entity = new CandidateResponse{
                        ProgramId = candidateResponse.ProgramId,
                        QuestionId = candidateResponse.QuestionId,
                        Response = (string?)candidateResponse.Response
                    };

                    await _context.CandidateResponses.AddAsync(entity);
                }

                await _context.SaveChangesAsync();

                return new BaseResponse(true, ProcessStatus.Successful.ToString());
            }
            catch (Exception ex){
                return new BaseResponse(false, ex.Message);
            }
        }

    }
}