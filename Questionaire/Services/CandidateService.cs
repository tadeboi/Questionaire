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
                    
                    var entity = new CandidateResponse{
                        ProgramId = candidateResponse.ProgramId,
                        QuestionId = candidateResponse.QuestionId,
                        Response = candidateResponse.Response.ToString()
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