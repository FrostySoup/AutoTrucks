using Model;
using Model.ReceiveData.Login;
using Service.Commands;
using Service.ConnexionService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.MainWindowViewModels.MainWindowViewModelsInterfaces;

namespace ViewModels.MainWindowViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private string username;

        private string password;

       

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            this.LoginCommand = new DelegateCommand(o => this.LoginUser());
        }

        private void LoginUser()
        {
            Task.Run(() =>
            {
                IConnectConnexionService connectConnexionService = new ConnectConnexionService();
                ReceivedLogin receivedLogin = connectConnexionService.LoginToConnexion(username, password);
                if (receivedLogin == null)
                {
                    //Login failed, do something
                }
                int a = 5;
            });
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                this.OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.OnPropertyChanged("Password");
            }
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
