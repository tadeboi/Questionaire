using Microsoft.AspNetCore.Mvc;
using Questionaire.DTOs;
using Questionaire.Helper;
using Questionaire.Interfaces;

namespace Questionaire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        readonly BaseResponse _baseResponse = new BaseResponse(false, ResponseMessages.InternalError);
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost("CreateProgram")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        [ProducesResponseType(typeof(FieldValidator), 400)]
        public async Task<IActionResult> CreateProgram(ProgramDTO model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, _baseResponse);
            }

            var response = await _programService.CreateProgram(model);

            return Ok(response);
        }

        [HttpPut("EditQuestion")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        [ProducesResponseType(typeof(FieldValidator), 400)]
        public async Task<IActionResult> EditQuestion(Guid programId, QuestionDTO questionDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, _baseResponse);
            }

            var response = await _programService.EditQuestion(programId, questionDTO);

            return Ok(response);
        }

        [HttpGet("GetQuestionTypes")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 500)]
        [ProducesResponseType(typeof(FieldValidator), 400)]
        public async Task<IActionResult> GetQuestionTypes()
        {
            var response = await _programService.GetQuestionTypes();

            return Ok(response);
        }
    }
}