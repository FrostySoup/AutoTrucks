using Model;
using Model.ReceiveData.Login;
using Model.SendData;
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
        private string username = "TES7";

        private string password = "teservices";

        private string message;

        public Login loginCredentials { get; set; }

        public bool loginCompleted { get; set; }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            loginCompleted = false;
            this.LoginCommand = new DelegateCommand(o => this.LoginUser());
        }

        private void LoginUser()
        {
            message = "Trying to log in......";
            this.OnPropertyChanged("Message");
            if (DataIsValid())
            {
                Task.Run(() =>
                {
                    bool correctLogin = ConnectConnexionServiceSingleton.Instance.CheckIfValidLoginToConnexion(username, password);

                    if (correctLogin == false)
                    {
                        message = "Failed to log in";
                    }
                    else
                    {
                        loginCompleted = true;
                        message = "Log in succesful";
                        loginCredentials = new Login
                        {
                            loginId = username,
                            password = password,
                            thirdPartyId = "Connexion"
                        };
                    }
                    this.OnPropertyChanged("Message");
                });
            }
            else
            {
                message = "Username lenght (4-16) Password lenght (4-30)";
                this.OnPropertyChanged("Message");
            }
        }

        //Validation will be transported elsewhere later
        private bool DataIsValid()
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
                return false;
            if (password.Length < 4 || password.Length > 30)
                return false;
            if (username.Length < 4 || username.Length > 16)
                return false;
            return true;
        }

        #region On property changed Members

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                this.OnPropertyChanged("Message");
            }
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
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
