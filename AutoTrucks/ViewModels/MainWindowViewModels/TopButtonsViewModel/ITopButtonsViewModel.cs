using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.MainWindowViewModels
{
    public interface ITopButtonsViewModel : INotifyPropertyChanged
    {
        void AddCommand(ICommand changePostTrucksViewModelCommand, ICommand changePostLoadsViewModelCommand);
    }
}
