using Http;
using Rospatent;
using RospatentHackathon.Commands;
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
    private bool _loading = false;

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
        _loading = true;
        Crutch.MyTab.GoToList();
        LoadedInfo = $"Загрузка..";
        Data = new SearchResultModel();
        Data = await HttpApiClient.Search(_model);
        LoadedInfo = $"Showed {(_model.Page - 1) *_model.DocumentsLimit+1}-{(_model.Page - 1) * _model.DocumentsLimit+Data.Downloaded}" +
                    $" из {Data.total}";
        _loading = false;
        UpdateButtons();
    }

    public RelayCommand _prevPageCommand;
    public RelayCommand PrevPageCommand
    {
        get
        {
            if (_prevPageCommand == null)
                _prevPageCommand = new RelayCommand(param =>
                {
                    _model.Page--;
                    Search();
                    UpdateButtons();
                }, (param) => _model!=null&&_model.Page > 1 && !_loading);
            return _prevPageCommand;
        }
    }

    public RelayCommand _nextPageCommand;
    public RelayCommand NextPageCommand
    {
        get
        {
            if (_nextPageCommand == null)
                _nextPageCommand = new RelayCommand(param =>
                {
                    _model.Page++;
                    Console.WriteLine($"modelPage:{_model.Page}");
                    Search();
                    UpdateButtons();
                }, (param) => true && !_loading);
            return _nextPageCommand;
        }
    }

    private void UpdateButtons()
    {
        NextPageCommand.UpdateCanExecute();
        PrevPageCommand.UpdateCanExecute();
    }

    public SearchResultViewModel()
    {
        Crutch.SearchResult = this;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
