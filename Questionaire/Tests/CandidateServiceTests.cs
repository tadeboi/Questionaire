using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Questionaire.Domain;
using Questionaire.DTOs;
using Questionaire.Infra;
using Questionaire.Services;
using Xunit;

namespace Questionaire.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly CandidateService _candidateService;
        private readonly List<ProgramQuestion> _programs;

        public CandidateServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _candidateService = new CandidateService(_mockContext.Object);

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
                        },
                        new Question
                        {
                            Id = Guid.NewGuid(),
                            QuestionName = "Question 2",
                            QuestionType = QuestionType.Paragraph,
                            Options = null,
                            MaxChoices = null
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
                            QuestionName = "Question 3",
                            QuestionType = QuestionType.Date,
                            Options = null,
                            MaxChoices = null
                        }
                    }
                }
            };
        }

        [Fact]
        public async Task GetQuestions_ValidProgramId_ReturnsQuestions()
        {
            // Arrange
            var programId = _programs[0].Id;
            _mockContext.Object.Programs.AddRange(_programs);

            // Act
            var result = await _candidateService.GetQuestions(programId);

            // Assert
            Assert.True(result.Status == true);
            var questions = Assert.IsAssignableFrom<List<Question>>(result.Data);
            Assert.Equal(_programs[0].Questions.Count, questions.Count);
        }

        [Fact]
        public async Task GetQuestions_InvalidProgramId_ReturnsError()
        {
            // Arrange
            var programId = Guid.NewGuid();

            // Act
            var result = await _candidateService.GetQuestions(programId);

            // Assert
            Assert.False(result.Status == false);
            Assert.Equal("This program does not exist", result.Data);
        }

        [Fact]
        public async Task SubmitResponse_ValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var candidateResponseDTOs = new List<CandidateResponseDTO>
            {
                new CandidateResponseDTO
                {
                    ProgramId = _programs[0].Id,
                    QuestionId = _programs[0].Questions[0].Id,
                    Response = new List<string> { "Option 1" }
                },
                new CandidateResponseDTO
                {
                    ProgramId = _programs[0].Id,
                    QuestionId = _programs[0].Questions[1].Id,
                    Response = "Free text response"
                },
                new CandidateResponseDTO
                {
                    ProgramId = _programs[1].Id,
                    QuestionId = _programs[1].Questions[0].Id,
                    Response = DateTime.Now
                }
            };

            _mockContext.Object.Programs.AddRange(_programs);

            // Act
            var result = await _candidateService.SubmitResponse(candidateResponseDTOs);

            // Assert
            Assert.True(result.Status == true);
            Assert.Equal("Successful", result.Data);
        }

    }
}