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
            // creation dun fake repo a base de Idea
            _repoMock = new Mock<IRepository<Idea>>();

            //je crée un Fake IUnitOfWork qui utilise mon fake repo et non ma bd
            _uowMock = new Mock<IUnitOfWork>();
            _uowMock
                .Setup(u => u.IdeaRepository)
                .Returns(_repoMock.Object);

            // Configure SaveChangesAsync() pour retourner 1 (nbr de lignes affectees)
            _uowMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            //j'instancie le ideaService en lui passant mon faux mock pr tester la logique metier (logique de si tout fonctionne bien)
            _service = new IdeaService(_uowMock.Object);
        }

        [Fact]
        public async Task SubmitIdeaAsync_EmptyTitle_ThrowsArgumentException()
        {
            //technique des trois A
            // Arrange: idee mauvaise (je mets un titre vide)
            var pasDeTitre = new Idea
            {
                Title = "",
                Description = "wadefsrgthyjhgfwd"
            };

            // Act & Assert: l'appel doit jeter ArgumentException
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.SubmitIdeaAsync(pasDeTitre)
            );

            // Et on ne doit JAMAIS appeler AddAsync ni SaveChangesAsync
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Idea>()), Times.Never);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task SubmitIdeaAsync_ValidIdea_CallsAddAndSaveAndSetsDefaults()
        {
            // Arrange: idée valide
            var bonTestQuiFaitPlaisir = new Idea
            {
                Title = "defrgt",
                Description = "sdfghy"
            };

            // Act
            await _service.SubmitIdeaAsync(bonTestQuiFaitPlaisir);

            // Assert 1: AddAsync sest fait call 1 fois avc mm objet
            _repoMock.Verify(r =>
                r.AddAsync(It.Is<Idea>(i =>
                    i.Title == bonTestQuiFaitPlaisir.Title &&
                    i.Description == bonTestQuiFaitPlaisir.Description
                )),
                Times.Once
            );

            // Assert 2: SaveChangesAsync sest fait call 1 fois
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);

            // Assert 3: le service a bien initialiser VoteCount et Status
            Assert.Equal(0, bonTestQuiFaitPlaisir.VoteCount);
            Assert.Equal(IdeaStatus.InProgress, bonTestQuiFaitPlaisir.Status);
        }
    }
}
