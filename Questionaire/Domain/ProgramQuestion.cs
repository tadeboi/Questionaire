using System.ComponentModel.DataAnnotations;

namespace Questionaire.Domain
{
    public class ProgramQuestion
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Question> Questions { get; set; }

    }

    public class Question{
        public Guid Id { get; set;}
        public QuestionType QuestionType { get; set; }
        public string QuestionName { get; set; }
        public List<string>? Options { get; set; }
        public int? MaxChoices { get; set; }
    }

    public enum QuestionType {
        Paragraph = 1,
        YesNo, 
        Dropdown, 
        MultipleChoice, 
        Date, 
        Number
    }
}