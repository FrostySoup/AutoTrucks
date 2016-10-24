using Service.Commands;
using System.Windows.Input;
using ViewModels.MainWindowViewModels.MainWindowDisplayViewModels.PostAssetsViewModels;
using System;
using Service.AddNewWindowFactory;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;
using Model.DataFromView;
using System.Collections.ObjectModel;

namespace ViewModels.MainWindowViewModels
{
    public class PostTrucksViewModel : AssetsAbstractViewModel, IMainWindowDisplayViewModel
    {
        public ICommand OpenPostAssetWindowCommand { get; private set; }
        public ICommand PostTruckCommand { get; private set; }

        public PostTrucksViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel) : base(windowFactory, postWindowViewModel)
        {
            this.OpenPostAssetWindowCommand = new DelegateCommand(o => this.OpenPostAssetWindow());
            this.PostTruckCommand = new DelegateCommand(o => this.PostTruck());
        }

        private void PostTruck()
        {
            throw new NotImplementedException();
        }

        protected override void AddNewPost(PostDataFromView postDataFromView)
        {
            postAssets.Add(postDataFromView);
        }

        public ObservableCollection<PostDataFromView> PostToDisplay
        {
            get
            {
                return postAssets;
            }
        }
    }
}
