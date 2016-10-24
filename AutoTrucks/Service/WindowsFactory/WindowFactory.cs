using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Views.MainWindowViews;
using Views.PopUpWindowViews;

namespace Service.AddNewWindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        public void ClosePostWindow()
        {
            if (Application.Current != null)
                if (Application.Current.Windows != null)
                {
                    Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Title == "Post Window");
                    if (win != null)
                        win.Close();
                }
        }

        public void CloseSearchWindow()
        {
            if (Application.Current != null)
                if (Application.Current.Windows != null)
                {
                    Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Title == "Search Window");
                    if (win != null)
                        win.Close();
                }
        }

        public void CreateNewDataSourceWindow(object dataContext)
        {
            if (dataContext != null)
            {
                DataSourceView view = new DataSourceView();
                view.DataContext = dataContext;
                view.ShowDialog();
            }
        }

        public void CreateNewLoginWindow(object dataContext)
        {
            if (dataContext != null)
            {
                LoginView view = new LoginView();
                view.DataContext = dataContext;
                view.ShowDialog();
            }
        }

        public void CreateNewPostAssetWindow(object dataContext)
        {
            if (dataContext != null)
            {
                PostAssetWindowView view = new PostAssetWindowView();
                view.DataContext = dataContext;
                view.ShowDialog();
            }
        }

        public void CreateNewSearchWindow(object dataContext)
        {
            if (dataContext != null)
            {
                SearchWindowView view = new SearchWindowView();
                view.DataContext = dataContext;
                view.ShowDialog();
            }
        }
    }
}
