using Model;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.MainWindowViewModels.MainWindowViewModelsInterfaces;

namespace ViewModel.MainWindowViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        private string username;

        private string password;

        public ICommand LoginUserCommand { get; private set; }

        public LoginViewModel()
        {
            this.LoginUserCommand = new DelegateCommand(o => this.LoginUser());
        }

        private void LoginUser()
        {
            int check = 0;
            check = 5;
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
