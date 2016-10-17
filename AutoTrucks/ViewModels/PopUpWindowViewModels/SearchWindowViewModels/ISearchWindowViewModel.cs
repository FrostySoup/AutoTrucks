using Model.DataFromView;
using Model.DataToView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PopUpWindowViewModels
{
    public interface ISearchWindowViewModel : INotifyPropertyChanged
    {
        SearchDataFromView searchData { get; set; }

        bool saveData { get; set; }
    }
}
