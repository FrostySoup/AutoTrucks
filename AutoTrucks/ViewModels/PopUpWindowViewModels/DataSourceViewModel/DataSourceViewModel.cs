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
using Model.SendData;

namespace ViewModels.PopUpWindowViewModels
{
    public class DataSourceViewModel : NotifyPropertyChangedAbstract, IDataSourceViewModel
    {
        private ObservableCollection<DataSource> dataSourceCollection;

        private readonly IWindowFactory windowFactory;

        private ILoginViewModel loginViewModel;

        public ICommand OpenWindowCommand { get; private set; }

        public ICommand DeleteSelectedDataSourcesCommand { get; private set; }

        public ISerializeService serializeService;

        public DataSourceViewModel(IWindowFactory windowFactory, ISerializeService serializeService, ILoginViewModel loginViewModel)
        {
            dataSourceCollection = new ObservableCollection<DataSource>();

            DataSources = serializeService.ReturnDataSource();            

            this.windowFactory = windowFactory;

            this.loginViewModel = loginViewModel;

            this.serializeService = serializeService;

            this.OpenWindowCommand = new DelegateCommand(o => this.OpenWindowLogin());

            this.DeleteSelectedDataSourcesCommand = new DelegateCommand(o => this.DeleteSelectedDataSources());
        }

        private void DeleteSelectedDataSources()
        {
            dataSourceCollection = new ObservableCollection<DataSource>(dataSourceCollection
                .Where(x => x.Selected == false));
            dataSourceCollection = serializeService.SerializeDataSourceList(dataSourceCollection);
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
            windowFactory.CreateNewLoginWindow(loginViewModel);
            SaveLoginCredentials(loginViewModel.loginCredentials);      
        }

        private void SaveLoginCredentials(Login loginCredentials)
        {
            if (loginViewModel.loginCredentials != null)
            {
                DataSource loginToDataSource = new DataSource()
                {
                    UserName = loginViewModel.loginCredentials.loginId,
                    Password = loginViewModel.loginCredentials.password,
                    Selected = false
                };
                if (loginViewModel.loginCompleted)
                {
                    dataSourceCollection = serializeService.SerializeDataSource(loginToDataSource);
                    OnPropertyChanged("DataSources");
                }
            }
        }
    }
}
