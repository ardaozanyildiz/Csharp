using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Interfaces;

namespace IdeaManager.UI.ViewModels
{
    public partial class IdeaListViewModel : ObservableObject
    {
        private readonly IIdeaService _ideaService;

        public ObservableCollection<Idea> Ideas { get; } = new();

        public IdeaListViewModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        [RelayCommand]
        public async Task RefreshAsync()
        {
            var all = await _ideaService.GetAllAsync();
            Ideas.Clear();
            foreach (var idea in all)
                Ideas.Add(idea);
        }
    }
}
