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
using ViewModels.MainWindowViewModels.MainWindowViewModelsInterfaces;

namespace ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ILoginViewModel loginViewModel;

        private readonly IWindowFactory windowFactory;

        private ITopButtonsViewModel topButtonsViewModel;
        private IPostLoadsViewModel postLoadsViewModel;
        private IPostTrucksViewModel postTrucksViewModel;
        private ISearchLoadsViewModel searchLoadsViewModel;
        private ISearchTrucksViewModel searchTrucksViewModel;

        public ICommand ChangePostTrucksViewModelCommand { get; private set; }
        public ICommand ChangePostLoadsViewModelCommand { get; private set; }

        public MainWindowViewModel(ITopButtonsViewModel topButtonsViewModel, IPostLoadsViewModel postLoadsViewModel,
             ISearchLoadsViewModel searchLoadsViewModel, ILoginViewModel loginViewModel, IWindowFactory windowFactory,
             ISearchTrucksViewModel searchTrucksViewModel, IPostTrucksViewModel postTrucksViewModel)
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
            ShowLoginWindow();
        }

        private void ChangeViewModel(MainWindowViewModelsEnum ViewModel)
        {
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

        private void ShowLoginWindow()
        {

        }

        #region OnPropertyChanged data

        public ISearchTrucksViewModel SearchTrucksViewModel
        {
            get { return searchTrucksViewModel; }
            set
            {
                searchTrucksViewModel = value;
                this.OnPropertyChanged("SearchTrucksViewModel");
            }
        }

        public IPostTrucksViewModel PostTrucksViewModel
        {
            get { return postTrucksViewModel; }
            set
            {
                postTrucksViewModel = value;
                this.OnPropertyChanged("PostTrucksViewModel");
            }
        }

        public IPostLoadsViewModel PostLoadsViewModel
        {
            get { return postLoadsViewModel; }
            set
            {
                postLoadsViewModel = value;
                this.OnPropertyChanged("PostLoadsViewModel");
            }
        }

        public ISearchLoadsViewModel SearchLoadsViewModel
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
