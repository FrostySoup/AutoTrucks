using Service.AddNewWindowFactory;
using Service.Commands;
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

        private ITopButtonsViewModel topButtonsViewModel;
        private IMainWindowDisplayViewModel postLoadsViewModel;
        private IMainWindowDisplayViewModel postTrucksViewModel;
        private IMainWindowDisplayViewModel searchLoadsViewModel;
        private IMainWindowDisplayViewModel searchTrucksViewModel;

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public MainWindowViewModel(ITopButtonsViewModel topButtonsViewModel, IMainWindowDisplayViewModel postLoadsViewModel,
             IMainWindowDisplayViewModel searchLoadsViewModel,
             IMainWindowDisplayViewModel searchTrucksViewModel, IMainWindowDisplayViewModel postTrucksViewModel)
        {           
            this.topButtonsViewModel = topButtonsViewModel;           

            this.ChangePostTrucksViewModelCommand = new DelegateCommand(o => this.ChangeViewModel());
            this.ChangePostLoadsViewModelCommand = new DelegateCommand(o => this.ChangeViewModel());

            this.topButtonsViewModel.AddCommand(ChangePostTrucksViewModelCommand, ChangePostLoadsViewModelCommand);

            this.postLoadsViewModel = postLoadsViewModel;
            this.searchLoadsViewModel = searchLoadsViewModel;
            this.searchTrucksViewModel = searchTrucksViewModel;
            this.postTrucksViewModel = postTrucksViewModel;
        }

        private void ChangeViewModel()
        {
          
        }

        #region OnPropertyChanged data

        public IMainWindowDisplayViewModel SearchTrucksViewModel
        {
            get { return searchTrucksViewModel; }
            set
            {
                searchTrucksViewModel = value;
                this.OnPropertyChanged("SearchTrucksViewModel");
            }
        }

        public IMainWindowDisplayViewModel PostTrucksViewModel
        {
            get { return postTrucksViewModel; }
            set
            {
                postTrucksViewModel = value;
                this.OnPropertyChanged("PostTrucksViewModel");
            }
        }

        public IMainWindowDisplayViewModel PostLoadsViewModel
        {
            get { return postLoadsViewModel; }
            set
            {
                postLoadsViewModel = value;
                this.OnPropertyChanged("PostLoadsViewModel");
            }
        }

        public IMainWindowDisplayViewModel SearchLoadsViewModel
        {
            get { return searchLoadsViewModel; }
            set
            {
                searchLoadsViewModel = value;
                this.OnPropertyChanged("SearchLoadsViewModel");
            }
        }

        public ITopButtonsViewModel TopButtonsViewModel
        {
            get { return topButtonsViewModel; }
            set
            {
                topButtonsViewModel = value;
                this.OnPropertyChanged("TopButtonsViewModel");
            }
        }

        #endregion

        

    }
}
