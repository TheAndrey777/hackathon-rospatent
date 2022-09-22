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
        get => _loadedInfo;
        private set
        {
            _loadedInfo = value;
            OnPropertyChanged();
        }
    }

    public string _searchType = "Выполните поисковый запрос";
    public string SearchType
    {
        get => _searchType;
        private set
        {
            _searchType = value;
            OnPropertyChanged();
        }
    }

    private PatentSearchModel _patentModel;
    private SimilarSearchModel _similarModel;
    private bool _loading = false;

    public void SetSearchModelAndSearch(PatentSearchModel model, string searchInfo)
    {
        _patentModel = model;
        SearchType = searchInfo;
        SearchPatent();
    }
    
    public void SetSimilarSearchModelAndSearch(SimilarSearchModel model, string searchInfo)
    {
        _similarModel = model;
        SearchType = searchInfo;
        SearchSimilar();
    }

    private async void SearchPatent()
    {
        if (_patentModel == null)
            return;
        _loading = true;
        Crutch.MyTab.GoToList();
        LoadedInfo = $"Загрузка..";
        Data = new SearchResultModel();
        Data = await HttpApiClient.Search(_patentModel);
        LoadedInfo = $"Показано {(_patentModel.Page - 1) * _patentModel.DocumentsLimit + 1}-{(_patentModel.Page - 1) * _patentModel.DocumentsLimit + Data.Downloaded}" +
                    $" из {Data.total}";
        _loading = false;
        UpdateButtons();
    }
    
    private async void SearchSimilar()
    {
        if (_similarModel == null)
            return;
        _loading = true;
        Crutch.MyTab.GoToList();
        LoadedInfo = $"Загрузка..";
        Data = new SearchResultModel();
        Data = await HttpApiClient.SimilarSearch(_similarModel);
        LoadedInfo = $"Показано {_similarModel.Count}";
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
                    _patentModel.Page--;
                    SearchPatent();
                    UpdateButtons();
                }, (param) => _patentModel != null && _patentModel.Page > 1 && !_loading);
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
                    _patentModel.Page++;
                    Console.WriteLine($"modelPage:{_patentModel.Page}");
                    SearchPatent();
                    UpdateButtons();
                }, (param) => _patentModel != null && Data != null && ((_patentModel.Page) * _patentModel.DocumentsLimit <= Data.total) && !_loading);
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
