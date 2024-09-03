using Moq;
using RecallSystem.Application.Services;
using RecallSystem.Domain.Entities;
using RecallSystem.Domain.Interfaces;

namespace RecallSystem.Tests
{
    public class RecallServiceTests
    {
        [Fact]
        public async Task GetAllRecallsAsync_ShouldReturnAllRecalls()
        {
            // Arrange
            var mockRepo = new Mock<IRecallRepository>();
            var expectedRecalls = new List<Recall>
            {
                new Recall { Id = 1, Titulo = "Recall 1" },
                new Recall { Id = 2, Titulo = "Recall 2" }
            };
            mockRepo.Setup(repo => repo.GetAllRecallsAsync()).ReturnsAsync(expectedRecalls);

            var service = new RecallService(mockRepo.Object);

            // Act
            var result = await service.GetAllRecallsAsync();

            // Assert
            Assert.Equal(expectedRecalls.Count, result.Count());
            Assert.Equal(expectedRecalls, result);
        }

        [Fact]
        public async Task GetRecallsByChassiAsync_ShouldReturnRecallsForChassi()
        {
            // Arrange
            var mockRepo = new Mock<IRecallRepository>();
            var chassi = "CHASSI123";
            var expectedExecucoes = new List<ExecucaoRecall>
            {
                new ExecucaoRecall { Id = 1, Chassi = chassi, Recall = new Recall { Id = 1, Titulo = "Recall 1" } }
            };
            mockRepo.Setup(repo => repo.GetRecallsByChassiAsync(chassi)).ReturnsAsync(expectedExecucoes);

            var service = new RecallService(mockRepo.Object);

            // Act
            var result = await service.GetRecallsByChassiAsync(chassi);

            // Assert
            Assert.Single(result);
            Assert.Equal(chassi, result.First().Chassi);
        }
    }
}