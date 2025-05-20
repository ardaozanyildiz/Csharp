using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Core.Interfaces;
using IdeaManager.Services.Services;

namespace IdeaManager.Tests.Services
{
    public class IdeaServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IRepository<Idea>> _repoMock;
        private readonly IdeaService _service;

        public IdeaServiceTests()
        {
            // creation dun fake repo avec un Idea
            //je crée un Fake IUnitOfWork qui utilise mon fake repo et non ma bd
            // Configure SaveChangesAsync() pour retourner 1 (nbr de lignes affectees)
            //j'instancie le ideaService en lui passant mon faux mock pr tester la logique metier (logique de si tout fonctionne bien)
            _repoMock = new Mock<IRepository<Idea>>();
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock
                .Setup(u => u.IdeaRepository)
                .Returns(_repoMock.Object);
            _uowMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);
            _service = new IdeaService(_uowMock.Object);
        }

        [Fact]
        public async Task SubmitIdeaAsync_EmptyTitle_ThrowsArgumentException()
        {
            //
            var test = new Idea
            {
                Title = "test",
                Description = "cela est un test unitaire"
            };

            
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.SubmitIdeaAsync(test)
            );
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Idea>()), Times.Never);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

    }
}
