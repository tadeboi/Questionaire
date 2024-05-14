using Questionaire.Domain;

namespace Questionaire.DTOs
{
    public class QuestionDTO
    {
        public Guid Id { get; set;}
        public QuestionType QuestionType { get; set; }
        public string QuestionName { get; set; }
        public List<string>? Options { get; set; }
        public int? MaxChoices { get; set; }
    }
}