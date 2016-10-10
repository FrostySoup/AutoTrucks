using Model.ReceiveData.Login;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.FillDataFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class TopButtonsViewModel : ITopButtonsViewModel
    {

        private readonly IWindowFactory windowFactory;

        private IDataSourceViewModel dataSourceViewModel;

        public ICommand OpenWindowCommand { get; private set; }

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public TopButtonsViewModel(IWindowFactory windowFactory)
        {
            this.windowFactory = windowFactory;

            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());
            dataSourceViewModel = new DataSourceViewModel(windowFactory);
        }

        private void OpenWindowConnections()
        {
            windowFactory.CreateNewDataSourceWindow(dataSourceViewModel);      
        }

        public void AddCommand(ICommand changePostTrucksViewModelCommand, ICommand changePostLoadsViewModelCommand)
        {
            ChangePostTrucksViewModelCommand = changePostTrucksViewModelCommand;
            ChangePostLoadsViewModelCommand = changePostLoadsViewModelCommand;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #endregion
    }
}
