using Model.DataHelpers;
using Service.AddNewWindowFactory;
using Service.Commands;
using Service.SerializeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels.PopUpWindowViewModels.BlacklistViewModel
{
    public class BlacklistViewModel : NotifyPropertyChangedAbstract, IBlacklistViewModel
    {
        private ObservableCollection<StringWrapper> companiesCollection;

        private readonly IWindowFactory windowFactory;

        public ICommand DeleteSelectedDataSourcesCommand { get; private set; }

        public ISerializeService serializeService;

        public BlacklistViewModel(IWindowFactory windowFactory, ISerializeService serializeService)
        {
            companiesCollection = new ObservableCollection<StringWrapper>();

            this.windowFactory = windowFactory;

            this.serializeService = serializeService;

            this.DeleteSelectedDataSourcesCommand = new DelegateCommand(o => this.DeleteSelectedDataSources());
        }

        private void DeleteSelectedDataSources()
        {
            
        }

        public void RefreshBlacklist()
        {
            var companies = serializeService.DeserializeCompanyName();
            foreach(var company in companies)
            {
                companiesCollection.Add(new StringWrapper()
                {
                    Value = company
                });
            }
        }

        public ObservableCollection<StringWrapper> CompaniesCollection
        {
            get
            {
                return companiesCollection;
            }
            set
            {
                companiesCollection = value;
                OnPropertyChanged("CompaniesCollection");
            }
        }       
    }
}
