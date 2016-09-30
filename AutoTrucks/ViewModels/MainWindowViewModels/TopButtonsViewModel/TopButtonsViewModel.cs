using Model.ReceiveData.Login;
using Service.Commands;
using Service.ConnexionService;
using Service.FillDataFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.MainWindowViewModels
{
    public class TopButtonsViewModel : ITopButtonsViewModel
    {

        private string token;

        private string date;

        public ICommand LoginConnexionCommand { get; private set; }

        public TopButtonsViewModel()
        {          
            this.LoginConnexionCommand = new DelegateCommand(o => this.LoginUserIntoConextion());
        }

        private void LoginUserIntoConextion()
        {
            ConnectConnexionService connectConnexionService = new ConnectConnexionService();
            ReceivedLogin receivedLogin = connectConnexionService.LoginToConnexion();
            token = System.Text.Encoding.UTF8.GetString(receivedLogin.Token.Primary);
            date = receivedLogin.Expiration.ToString();
            OnPropertyChanged("Token");
            OnPropertyChanged("Date");
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
