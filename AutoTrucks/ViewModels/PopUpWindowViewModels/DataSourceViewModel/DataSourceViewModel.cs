using Model;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.SerializeServices;
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
        private ObservableCollection<DataSource> dataSourceCollection;

        private readonly IWindowFactory windowFactory;

        private ILoginViewModel loginViewModel;

        public ICommand OpenWindowCommand { get; private set; }

        public ICommand DeleteSelectedDataSourcesCommand { get; private set; }

        public DataSourceViewModel(IWindowFactory windowFactory)
        {

            DataSources = SerializeServiceSingleton.Instance.ReturnDataSource();            

            this.windowFactory = windowFactory;           

            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowLogin());

            this.DeleteSelectedDataSourcesCommand = new DelegateCommand(o => this.DeleteSelectedDataSources());
        }

        private void DeleteSelectedDataSources()
        {
            dataSourceCollection = new ObservableCollection<DataSource>(dataSourceCollection
                .Where(x => x.Selected == false));
            SerializeServiceSingleton.Instance.SerializeDataSourceList(dataSourceCollection);
            OnPropertyChanged("DataSources");
        }

        public ObservableCollection<DataSource> DataSources
        {
            get
            {
                return dataSourceCollection;
            }
            set
            {
                dataSourceCollection = value;
                OnPropertyChanged("DataSources");
            }
        }


        private void OpenWindowLogin()
        {
            loginViewModel = new LoginViewModel();
            windowFactory.CreateNewLoginWindow(loginViewModel);
            DataSource loginToDataSource = new DataSource()
            {
                UserName = loginViewModel.loginCredentials.loginId,
                Password = loginViewModel.loginCredentials.password,
                Selected = false
            };
            if (loginViewModel.loginCompleted)
            {
                if (SerializeServiceSingleton.Instance.SerializeDataSource(loginToDataSource))
                {
                    dataSourceCollection.Add(loginToDataSource);
                    OnPropertyChanged("DataSources");
                }
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
