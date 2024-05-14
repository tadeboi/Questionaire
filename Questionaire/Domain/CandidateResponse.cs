using System.ComponentModel.DataAnnotations.Schema;

namespace Questionaire.Domain
{
    public class CandidateResponse
    {
        public Guid Id{ get; set; }
        public Guid ProgramId { get; set; }
        public Guid QuestionId { get; set; }
        public string? Response { get; set; }

    }
}