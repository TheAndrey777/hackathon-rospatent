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
    public string Applicant
    {
        get => _model.Applicant;
        set
        {
            _model.Applicant = value;
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
            _model.PublicationDateFrom = value;
            OnPropertyChanged();
        }
    }

    public PatentSearchViewModel()
    {
        _model.Sort = PatentSortEnum.Relevance;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
