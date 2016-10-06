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
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ILoginViewModel loginViewModel;

        private readonly IWindowFactory windowFactory;

        private ITopButtonsViewModel topButtonsViewModel;
        private IMainWindowDisplayViewModel postLoadsViewModel;
        private IMainWindowDisplayViewModel postTrucksViewModel;
        private IMainWindowDisplayViewModel searchLoadsViewModel;
        private IMainWindowDisplayViewModel searchTrucksViewModel;

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public MainWindowViewModel(ITopButtonsViewModel topButtonsViewModel, IMainWindowDisplayViewModel postLoadsViewModel,
             IMainWindowDisplayViewModel searchLoadsViewModel, ILoginViewModel loginViewModel, IWindowFactory windowFactory,
             IMainWindowDisplayViewModel searchTrucksViewModel, IMainWindowDisplayViewModel postTrucksViewModel)
        {           
            this.windowFactory = windowFactory;
            this.topButtonsViewModel = topButtonsViewModel;           

            this.ChangePostTrucksViewModelCommand = new DelegateCommand(o => this.ChangeViewModel(MainWindowViewModelsEnum.PostTrucksViewModel));
            this.ChangePostLoadsViewModelCommand = new DelegateCommand(o => this.ChangeViewModel(MainWindowViewModelsEnum.PostLoadsViewModel));

            this.topButtonsViewModel.AddCommand(ChangePostTrucksViewModelCommand, ChangePostLoadsViewModelCommand);

            this.postLoadsViewModel = postLoadsViewModel;
            this.searchLoadsViewModel = searchLoadsViewModel;
            this.searchTrucksViewModel = searchTrucksViewModel;
            this.postTrucksViewModel = postTrucksViewModel;

            this.loginViewModel = loginViewModel;
        }

        private void ChangeViewModel(MainWindowViewModelsEnum ViewModel)
        {
            //initiating VIEWMODEL
            switch (ViewModel) {
                case MainWindowViewModelsEnum.PostLoadsViewModel:
                    if (postLoadsViewModel != null)
                        postLoadsViewModel = null;
                    else
                        postLoadsViewModel = new PostLoadsViewModel();
                    this.OnPropertyChanged("PostLoadsViewModel");
                    break;
                case MainWindowViewModelsEnum.PostTrucksViewModel:
                    if (postTrucksViewModel != null)
                        postTrucksViewModel = null;
                    else
                        postTrucksViewModel = new PostTrucksViewModel();
                    this.OnPropertyChanged("PostTrucksViewModel");
                    break;
            }
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
        #endregion

    }
}
