using Model.DataFromView;
using Service.AddNewWindowFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.PopUpWindowViewModels.PostWindowViewModel;

namespace ViewModels.MainWindowViewModels.MainWindowDisplayViewModels.PostAssetsViewModels
{
    public abstract class AssetsAbstractViewModel : NotifyPropertyChangedAbstract
    {
        private IWindowFactory windowFactory;

        private IPostWindowViewModel postWindowViewModel;

        protected ObservableCollection<PostDataFromView> postAssets;

        protected AssetsAbstractViewModel(IWindowFactory windowFactory, IPostWindowViewModel postWindowViewModel)
        {
            postAssets = new ObservableCollection<PostDataFromView>();
            this.windowFactory = windowFactory;
            this.postWindowViewModel = postWindowViewModel;
        }

        protected void OpenPostAssetWindow()
        {
            windowFactory.CreateNewPostAssetWindow(postWindowViewModel);
            if (postWindowViewModel.saveChanges == true && postWindowViewModel.postData != null)
            {
                AddNewPost(postWindowViewModel.postData);
                postWindowViewModel.saveChanges = false;
                postWindowViewModel.postData = new PostDataFromView();
            }
        }

        protected abstract void AddNewPost(PostDataFromView postDataFromView);
    }
}
