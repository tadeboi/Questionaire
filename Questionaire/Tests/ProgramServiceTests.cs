using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Questionaire.Domain;
using Questionaire.DTOs;
using Questionaire.Helper;
using Questionaire.Infra;
using Questionaire.Services;
using Xunit;

namespace Questionaire.Tests
{
    public class ProgramServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly ProgramService _programService;
        private readonly List<ProgramQuestion> _programs;

        public ProgramServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _programService = new ProgramService(_mockContext.Object);

            _programs = new List<ProgramQuestion>
            {
                new ProgramQuestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Program 1",
                    Description = "Description 1",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = Guid.NewGuid(),
                            QuestionName = "Question 1",
                            QuestionType = QuestionType.MultipleChoice,
                            Options = new List<string> { "Option 1", "Option 2" },
                            MaxChoices = 1
                        }
                    }
                },
                new ProgramQuestion
                {
                    Id = Guid.NewGuid(),
                    Title = "Program 2",
                    Description = "Description 2",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            Id = Guid.NewGuid(),
                            QuestionName = "Question 2",
                            QuestionType = QuestionType.Paragraph,
                            Options = null,
                            MaxChoices = null
                        }
                    }
                }
            };
        }

        [Fact]
        public async Task CreateProgram_ValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var programDTO = new ProgramDTO
            {
                Title = "New Program",
                Description = "New Description",
                Questions = new List<QuestionModel>
                {
                    new QuestionModel
                    {
                        QuestionName = "New Question",
                        QuestionType = QuestionType.MultipleChoice,
                        Options = new List<string> { "Option 1", "Option 2" },
                        MaxChoices = 1
                    }
                }
            };

            // Act
            var result = await _programService.CreateProgram(programDTO);

            // Assert
            Assert.True(result.Status == true);
            Assert.Equal("Successful", result.Data);
        }

        [Fact]
        public async Task EditQuestion_ValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var programId = _programs[0].Id;
            var questionId = _programs[0].Questions[0].Id;
            var questionDTO = new QuestionDTO
            {
                Id = questionId,
                QuestionName = "Updated Question",
                QuestionType = QuestionType.Paragraph,
                Options = null,
                MaxChoices = null
            };

            _mockContext.Object.Programs.AddRange(_programs);

            // Act
            var result = await _programService.EditQuestion(programId, questionDTO);

            // Assert
            Assert.True(result.Status == true);
            Assert.Equal("Successful", result.Data);
        }

        [Fact]
        public async Task GetQuestionTypes_ReturnsAllQuestionTypes()
        {
            // Act
            var result = await _programService.GetQuestionTypes();

            // Assert
            Assert.True(result.Status == true);
            var enumData = Assert.IsAssignableFrom<IEnumerable<EnumData>>(result.Data);
            Assert.Equal(Enum.GetNames(typeof(QuestionType)).Length, enumData.Count());
        }

        [Fact]
        public async Task GetAllPrograms_ReturnsAllPrograms()
        {
            // Arrange
            _mockContext.Object.Programs.AddRange(_programs);

            // Act
            var result = await _programService.GetAllPrograms();

            // Assert
            Assert.True(result.Status == true);
            var programs = Assert.IsAssignableFrom<List<ProgramQuestion>>(result.Data);
            Assert.Equal(_programs.Count, programs.Count);
        }
    }
}