using Model;
using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.MainWindowViewModels;
using ViewModels.MainWindowViewModels.MainWindowViewModelsInterfaces;

namespace ViewModels.PopUpWindowViewModels
{
    public class DataSourceViewModel : IDataSourceViewModel
    {
        private ObservableCollection<DataSource> model;

        private readonly IWindowFactory windowFactory;

        private ILoginViewModel loginViewModel;

        public ICommand OpenWindowCommand { get; private set; }

        public DataSourceViewModel(IWindowFactory windowFactory)
        {
            DataSources = new ObservableCollection<DataSource>();

            DataSources.Add(new DataSource()
            {
                UserName = "Testas",
                Selected = true,
                Source = "Connexion"
            });

            this.windowFactory = windowFactory;

            this.loginViewModel = new LoginViewModel();

            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowLogin());
        }

        public ObservableCollection<DataSource> DataSources
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged("DataSources");
            }
        }

        private void OpenWindowLogin()
        {
            windowFactory.CreateNewLoginWindow(loginViewModel);
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
