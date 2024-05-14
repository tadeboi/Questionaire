using Questionaire.DTOs;
using Questionaire.Helper;

namespace Questionaire.Interfaces
{
    public interface IProgramService
    {
        Task<BaseResponse> CreateProgram(ProgramDTO model);
        Task<BaseResponse> EditQuestion(Guid programId, QuestionDTO questionDTO);

        Task<BaseResponse> GetQuestionTypes();
    }
}