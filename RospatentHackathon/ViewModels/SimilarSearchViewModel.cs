﻿using System;
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
