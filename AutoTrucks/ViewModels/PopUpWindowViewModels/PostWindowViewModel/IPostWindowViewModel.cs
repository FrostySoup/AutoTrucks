using Model.DataFromView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PopUpWindowViewModels.PostWindowViewModel
{
    public interface IPostWindowViewModel : INotifyPropertyChanged
    {
        bool saveChanges { get;set; }

        PostDataFromView postData { get; set; }
    }
}
