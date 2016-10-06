using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class SearchLoadsViewModel : SearchViewModelAbstract, IMainWindowDisplayViewModel
    {

        public ICommand OpenSearchWindowCommand { get; private set; }

        public SearchLoadsViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;

            this.OpenSearchWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());
        }
    }
}
