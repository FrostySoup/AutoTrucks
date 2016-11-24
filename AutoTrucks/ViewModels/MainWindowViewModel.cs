using Service.AddNewWindowFactory;
using Service.Commands;
using Service.ConnexionService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels.MainWindowViewModels;
using ViewModels.PopUpWindowViewModels;

namespace ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedAbstract, INotifyPropertyChanged
    {

        private ITopButtonsViewModel _topButtonsViewModel;
        private IMainWindowDisplayViewModel _postLoadsViewModel;
        private IMainWindowDisplayViewModel _postTrucksViewModel;
        private IMainWindowDisplayViewModel _searchLoadsViewModel;
        private IMainWindowDisplayViewModel _searchTrucksViewModel;

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public MainWindowViewModel(ITopButtonsViewModel topButtonsViewModel, IMainWindowDisplayViewModel postLoadsViewModel,
             IMainWindowDisplayViewModel searchLoadsViewModel, IMainWindowDisplayViewModel searchTrucksViewModel, 
             IMainWindowDisplayViewModel postTrucksViewModel)
        {                     
            ChangePostTrucksViewModelCommand = new DelegateCommand(o => this.ChangeViewModel());
            ChangePostLoadsViewModelCommand = new DelegateCommand(o => this.ChangeViewModel());

            _topButtonsViewModel = topButtonsViewModel;
            _topButtonsViewModel.AddCommand(ChangePostTrucksViewModelCommand, ChangePostLoadsViewModelCommand);

            _postLoadsViewModel = postLoadsViewModel;
            _searchLoadsViewModel = searchLoadsViewModel;
            _searchTrucksViewModel = searchTrucksViewModel;
            _postTrucksViewModel = postTrucksViewModel;
        }
        /*
        private void SetCapabilieties()
        {
            if (sessionCacheSingleton.sessions[0] != null)
            {
                var res = connectConnexionService.RetrieveUserCapabilities(sessionCacheSingleton.sessions[0], new CapabilityType[] {
                    CapabilityType.ManagePostings,
                    CapabilityType.Search,
                    CapabilityType.AlarmMatch
                    });
                int a = 45;
            }
        }*/

        private void ChangeViewModel()
        {
          
        }

        #region OnPropertyChanged data

        public IMainWindowDisplayViewModel SearchTrucksViewModel
        {
            get { return _searchTrucksViewModel; }
            set
            {
                _searchTrucksViewModel = value;
                this.OnPropertyChanged("SearchTrucksViewModel");
            }
        }

        public IMainWindowDisplayViewModel PostTrucksViewModel
        {
            get { return _postTrucksViewModel; }
            set
            {
                _postTrucksViewModel = value;
                this.OnPropertyChanged("PostTrucksViewModel");
            }
        }

        public IMainWindowDisplayViewModel PostLoadsViewModel
        {
            get { return _postLoadsViewModel; }
            set
            {
                _postLoadsViewModel = value;
                this.OnPropertyChanged("PostLoadsViewModel");
            }
        }

        public IMainWindowDisplayViewModel SearchLoadsViewModel
        {
            get { return _searchLoadsViewModel; }
            set
            {
                _searchLoadsViewModel = value;
                this.OnPropertyChanged("SearchLoadsViewModel");
            }
        }

        public ITopButtonsViewModel TopButtonsViewModel
        {
            get { return _topButtonsViewModel; }
            set
            {
                _topButtonsViewModel = value;
                this.OnPropertyChanged("TopButtonsViewModel");
            }
        }

        #endregion

        

    }
}
