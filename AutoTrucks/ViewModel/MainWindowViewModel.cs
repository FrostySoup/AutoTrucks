using Service.AddNewWindowFactory;
using Service.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.MainWindowViewModels;
using ViewModel.MainWindowViewModels.MainWindowViewModelsInterfaces;
using Views.MainWindowViews;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ILoginViewModel loginViewModel;

        private readonly IWindowFactory windowFactory;


        private ITopButtonsViewModel topButtonsViewModel;
        private IMainWindowCanvasViewModel postLoadsViewModel;
        //private IMainWindowCanvasViewModel postTrucksViewModel;
        private IMainWindowCanvasViewModel searchLoadsViewModel;
        //private IMainWindowCanvasViewModel searchTrucksViewModel;



        public MainWindowViewModel(ITopButtonsViewModel topButtonsViewModel, IMainWindowCanvasViewModel postLoadsViewModel,
             IMainWindowCanvasViewModel searchLoadsViewModel, ILoginViewModel loginViewModel, IWindowFactory windowFactory)
        {           
            this.windowFactory = windowFactory;
            this.topButtonsViewModel = topButtonsViewModel;
            this.postLoadsViewModel = postLoadsViewModel;
            this.searchLoadsViewModel = searchLoadsViewModel;
            ShowLoginWindow();
        }

        private void ShowLoginWindow()
        {
            windowFactory.CreateNewWindow(loginViewModel);
        }

        public IMainWindowCanvasViewModel PostLoadsViewModel
        {
            get { return postLoadsViewModel; }
            set
            {
                postLoadsViewModel = value;
                this.OnPropertyChanged("PostLoadsViewModel");
            }
        }

        public IMainWindowCanvasViewModel SearchLoadsViewModel
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
