using Questionaire.DTOs;
using Questionaire.Helper;

namespace Questionaire.Interfaces
{
    public interface ICandidateService
    {
        Task<BaseResponse> GetQuestions(Guid programId);
        Task<BaseResponse> SubmitResponse(List<CandidateResponseDTO> candidateResponseDTOs);
    }
}