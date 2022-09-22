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
            OnPropertyChanged();
        }
    }

    //public int TypeIndex
    //{
    //    get => (int)_model.Sort;
    //    set
    //    {
    //        _model.Sort = (PatentSortEnum)value;
    //        OnPropertyChanged();
    //    }
    //}

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
