using RospatentHackathon.Commands;
using RospatentHackathon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RospatentHackathon.ViewModels;

class SimilarSearchViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private SimilarSearchModel _similarSearchModel = new SimilarSearchModel();

    private bool _idSearch = true;
    public bool TextSearchEnable
    {
        get => !_idSearch;
        set
        {
            _idSearch = !value;
            OnPropertyChanged();
        }
    }
    public bool IdSearchEnable
    {
        get => _idSearch;
        set
        {
            _idSearch = value;
            OnPropertyChanged();
        }
    }

    private int _searchTypeIndex;
    public int SearchTypeIndex
    {
        get => _searchTypeIndex;
        set
        {
            _searchTypeIndex = value;
            IdSearchEnable = value == 0;
            TextSearchEnable = value == 1;
            _similarSearchModel.Type = _idSearch ? SearchTypeEnum.Id : SearchTypeEnum.Text;
            OnPropertyChanged();
        }
    }

    public string RequestText
    {
        get => _similarSearchModel.RequestText;
        set
        {
            _similarSearchModel.RequestText = value;
            OnPropertyChanged();
        }
    }
    
    public string RequestID
    {
        get => _similarSearchModel.RequestID;
        set
        {
            _similarSearchModel.RequestID = value;
            OnPropertyChanged();
        }
    }
    
    public int Count
    {
        get => _similarSearchModel.Count;
        set
        {
            _similarSearchModel.Count = value;
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
                    Crutch.SearchResult.SetSimilarSearchModelAndSearch(_similarSearchModel, "Поиск хожих документов");
                }, (param)=>
                {
                    if (IdSearchEnable)
                        return true;
                    return RequestText.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length >= 50;
                });
            return _searchCommand;
        }
    }

    public SimilarSearchViewModel()
    {
        RequestText = "Text";
        RequestID = "ID";
        SearchTypeIndex = 0;
        Count = 30;
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
