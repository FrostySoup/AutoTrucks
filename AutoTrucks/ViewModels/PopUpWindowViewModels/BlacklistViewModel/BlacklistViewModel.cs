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

        public ICommand DeleteSelectedCompaniesCommand { get; private set; }

        public ISerializeService serializeService;

        public BlacklistViewModel(IWindowFactory windowFactory, ISerializeService serializeService)
        {
            companiesCollection = new ObservableCollection<StringWrapper>();

            this.windowFactory = windowFactory;
            this.serializeService = serializeService;
            this.DeleteSelectedCompaniesCommand = new DelegateCommand(o => this.DeleteSelectedDataSources());
        }

        private void DeleteSelectedDataSources()
        {
            companiesCollection = new ObservableCollection<StringWrapper>(companiesCollection
                .Where(x => !x.Checked).AsEnumerable());

            serializeService.SerializeCompanyNamesList(companiesCollection);
            OnPropertyChanged("CompaniesCollection");
        }

        public void RefreshBlacklist()
        {
            companiesCollection = new ObservableCollection<StringWrapper>();
            var companies = serializeService.DeserializeCompanyName();

            foreach(var company in companies)
            {
                companiesCollection.Add(new StringWrapper()
                {
                    Value = company
                });
            }

            OnPropertyChanged("CompaniesCollection");
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
