using System.ComponentModel.DataAnnotations;
using Questionaire.Domain;

namespace Questionaire.DTOs
{
    public class ProgramDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    public class QuestionModel
    {
        public QuestionType QuestionType { get; set; }
        public string QuestionName { get; set; }
        public List<string>? Options { get; set; }
        public int? MaxChoices { get; set; }
    }
}