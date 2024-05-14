using Microsoft.AspNetCore.Mvc;
using Questionaire.DTOs;
using Questionaire.Helper;
using Questionaire.Interfaces;

namespace Questionaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        readonly BaseResponse _baseResponse = new BaseResponse(false, ResponseMessages.InternalError);
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("GetQuestions")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        [ProducesResponseType(typeof(FieldValidator), 400)]
        public async Task<IActionResult> GetQuestions(Guid programId)
        {
            var response = await _candidateService.GetQuestions(programId);

            return Ok(response);
        }

        [HttpPost("SubmitResponse")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        [ProducesResponseType(typeof(FieldValidator), 400)]
        public async Task<IActionResult> SubmitResponse(List<CandidateResponseDTO> candidateResponseDTOs)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, _baseResponse);
            }

            var response = await _candidateService.SubmitResponse(candidateResponseDTOs);

            return Ok(response);
        }
    }
}
