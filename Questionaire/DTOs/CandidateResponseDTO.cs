namespace Questionaire.DTOs
{
    public class CandidateResponseDTO{
        public Guid ProgramId { get; set; }
        public Guid QuestionId { get; set; }
        public object? Response { get; set; }
    }
}