using Rospatent;
using RospatentHackathon.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RospatentHackathon.ViewModels;

public class SearchResultViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public SearchResultModel Data { get; private set; }

    public SearchResultViewModel()
    {
        ViewModelsVault.SearchResult = this;
        //SearchResults.Add();
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
