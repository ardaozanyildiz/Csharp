using IdeaManager.Core.Enums;

namespace IdeaManager.Core.Entities;

public class Idea
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public IdeaStatus Status { get; set; } = IdeaStatus.Pending;  // Enum IdeaStatus

    public int VoteCount { get; set; } = 0;

    // Relations
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public int? ProjectId { get; set; }  // idée validée -> projet
    public Project? Project { get; set; }
}
