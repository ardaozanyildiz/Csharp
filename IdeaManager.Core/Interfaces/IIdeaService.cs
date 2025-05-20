using IdeaManager.Core.Entities;

namespace IdeaManager.Core.Interfaces
{
    public interface IIdeaService
    {
        Task<List<Idea>> GetAllAsync();
        Task SubmitIdeaAsync(Idea idea);
        Task VoteForIdeaAsync(int ideaId);
    }
}