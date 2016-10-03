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

        private string token;

        private string date;

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

            /*
            Task.Run(() =>
            {
                ConnectConnexionService connectConnexionService = new ConnectConnexionService();
                ReceivedLogin receivedLogin = connectConnexionService.LoginToConnexion();
                token = System.Text.Encoding.UTF8.GetString(receivedLogin.Token.Primary);
                date = receivedLogin.Expiration.ToString();
                OnPropertyChanged("Token");
                OnPropertyChanged("Date");
            });*/
        }

        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                date = value;
                OnPropertyChanged("Token");
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
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
