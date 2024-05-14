using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Questionaire.Domain;
using Questionaire.DTOs;
using Questionaire.Helper;
using Questionaire.Infra;
using Questionaire.Interfaces;

namespace Questionaire.Services
{
    public class ProgramService : IProgramService
    {
        private readonly AppDbContext _context;
        private static bool _ensureCreated { get; set; } = false;

        public ProgramService(AppDbContext context)
        {
            _context = context;

            if (!_ensureCreated)
            {
            _context.Database.EnsureCreated();
            _ensureCreated = true;
            }
        }

        public async Task<BaseResponse> CreateProgram(ProgramDTO model){

            try{
                var questions = new List<Question>();
                foreach (var question in model.Questions){
                    var newQuestion = new Question {
                        Id = Guid.NewGuid(),
                        QuestionName = question.QuestionName,
                        QuestionType = question.QuestionType,
                        Options = question.Options,
                        MaxChoices = question.MaxChoices
                    };
                    questions.Add(newQuestion);
                }

                var entity = new ProgramQuestion{
                    Title = model.Title,
                    Description = model.Description,
                    Questions = questions
                };
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new BaseResponse(true, ProcessStatus.Successful.ToString());

            }
            catch(Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }

        public async Task<BaseResponse> EditQuestion(Guid programId, QuestionDTO questionDTO){
            try
            {
                var program = await _context.Programs.FirstOrDefaultAsync(p=> p.Id == programId);
                if (program != null){
                    var question = program.Questions.FirstOrDefault(p=> p.Id == questionDTO.Id);
                    question.QuestionName = questionDTO.QuestionName;
                    question.QuestionType = questionDTO.QuestionType;
                    question.Options = questionDTO.Options;
                    question.MaxChoices = questionDTO.MaxChoices;

                    await _context.SaveChangesAsync();
                }
                return new BaseResponse(true, ProcessStatus.Successful.ToString());
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }

        }

        public async Task<BaseResponse> GetQuestionTypes(){

            var data = Enum.GetValues(typeof(QuestionType))
            .Cast<QuestionType>()
            .Select(color => new EnumData
            {
                Key = color.ToString(),
                Value = (int)color
            });

            return new BaseResponse(true, data);
        }

        public async Task<BaseResponse> GetAllPrograms(){
            try
            {
                var data = await _context.Programs.ToListAsync();
                return new BaseResponse(true, data);
            }
            catch(Exception ex)
            {
                return new BaseResponse(false, ex.Message);
            }
        }
    }

}