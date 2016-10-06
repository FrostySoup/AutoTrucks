using Model;
using Model.SendData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.PopUpWindowViewModels
{
    public interface ILoginViewModel : INotifyPropertyChanged
    {
        Login loginCredentials { get; set; }

        bool loginCompleted { get; set; }
    }
}
