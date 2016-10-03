using Service.AddNewWindowFactory;
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
    public abstract class SearchViewModelAbstract : INotifyPropertyChanged
    {
        protected ISearchWindowViewModel searchWindowViewModel;

        protected IWindowFactory windowFactory;     

        protected void OpenWindowConnections()
        {
            searchWindowViewModel = new SearchWindowViewModel();
            windowFactory.CreateNewSearchWindow(searchWindowViewModel);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
