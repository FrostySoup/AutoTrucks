using Model.ReceiveData.Login;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels.MainWindowViewModels
{
    public class TopButtonsViewModel : NotifyPropertyChangedAbstract, ITopButtonsViewModel
    {

        private readonly IWindowFactory windowFactory;

        private IDataSourceViewModel dataSourceViewModel;

        public ICommand OpenWindowCommand { get; private set; }

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public TopButtonsViewModel(IWindowFactory windowFactory, IDataSourceViewModel dataSourceViewModel)
        {
            this.windowFactory = windowFactory;
            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowConnections());
            this.dataSourceViewModel = dataSourceViewModel;
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
    }
}
