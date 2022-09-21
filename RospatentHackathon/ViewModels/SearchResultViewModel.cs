using Rospatent;
using RospatentHackathon.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RospatentHackathon.ViewModels;

public class SearchResultViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public SearchResultModel Data { get; private set; } = new SearchResultModel();
    public int Total => Data.total;

    public void SetData(SearchResultModel res)
    {
        Data = res;
        Crutch.MyTab.GoToRead();
        Update();
    }

    private void Update()
    {
        OnPropertyChanged(nameof(Total));
    }

    public SearchResultViewModel()
    {
        Crutch.SearchResult = this;
        //SearchResults.Add();
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
