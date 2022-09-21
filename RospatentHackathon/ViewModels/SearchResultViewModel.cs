using Http;
using Rospatent;
using RospatentHackathon.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RospatentHackathon.ViewModels;

public class SearchResultViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public SearchResultModel _data = new SearchResultModel();
    public SearchResultModel Data
    {
        get => _data;
        private set
        {
            _data = value;
            OnPropertyChanged();
        }
    }

    public string _loadedInfo = "Выполните поисковый запрос";
    public string LoadedInfo 
    { 
        get=> _loadedInfo;
        private set
        {
            _loadedInfo = value;
            OnPropertyChanged();
        }
    }
    
    public string _searchType = "Выполните поисковый запрос";
    public string SearchType
    { 
        get=> _searchType;
        private set
        {
            _searchType = value;
            OnPropertyChanged();
        }
    }

    private PatentSearchModel _model;

    public void SetSearchModelAndSearch(PatentSearchModel model, string searchInfo)
    {
        _model = model;
        SearchType = searchInfo;
        Search();
    }

    private async void Search()
    {
        if(_model == null)
            return;
        Crutch.MyTab.GoToRead();
        LoadedInfo = $"Загрузка..";
        Data = new SearchResultModel();
        Data = await HttpApiClient.Search(_model);
        LoadedInfo = $"Showed {Data.Downloaded}/{Data.total}";
    }

    public SearchResultViewModel()
    {
        Crutch.SearchResult = this;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
