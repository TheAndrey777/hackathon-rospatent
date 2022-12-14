using Http;
using RospatentHackathon.Commands;
using RospatentHackathon.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RospatentHackathon.ViewModels;

class PatentSearchViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private PatentSearchModel _model = new PatentSearchModel();

    public string Request
    {
        get => _model.Request;
        set
        {
            _model.Request = value;
            OnPropertyChanged();
        }
    }
    public string DocumentNumber
    {
        get => _model.DocumentNumber;
        set
        {
            _model.DocumentNumber = value;
            OnPropertyChanged();
        }
    }
    public string Author
    {
        get => _model.Author;
        set
        {
            _model.Author = value;
            OnPropertyChanged();
        }
    }
    public string Patentee
    {
        get => _model.Patentee;
        set
        {
            _model.Patentee = value;
            OnPropertyChanged();
        }
    }
    public int SortIndex
    {
        get => (int)_model.Sort;
        set
        {
            _model.Sort = (PatentSortEnum)value;
            OnPropertyChanged();
        }
    }
    public DateTime PublicationDateFrom
    {
        get => _model.PublicationDateFrom;
        set
        {
            _model.PublicationDateFrom = value;
            OnPropertyChanged();
        }
    }
    public DateTime PublicationDateTo
    {
        get => _model.PublicationDateTo;
        set
        {
            _model.PublicationDateTo = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand _searchCommand;
    public RelayCommand SearchCommand
    {
        get
        {
            if (_searchCommand == null)
                _searchCommand = new RelayCommand(param =>
                {
                    _model.Page = 1;
                    Crutch.SearchResult.SetSearchModelAndSearch(_model, "Поиск патентов");
                });
            return _searchCommand;
        }
    }
    
    public RelayCommand _clearCommand;
    public RelayCommand ClearCommand
    {
        get
        {
            if (_clearCommand == null)
                _clearCommand = new RelayCommand(param =>
                {
                    _model = new PatentSearchModel();
                    Request = "";
                    DocumentNumber = "";
                    Author = "";
                    Patentee = "";
                    PublicationDateFrom = new DateTime().AddYears(2000);
                    PublicationDateTo = DateTime.Today;
                    SortIndex = 0;
                });
            return _clearCommand;
        }
    }

    public PatentSearchViewModel()
    {
        ClearCommand.Execute(null);
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
