using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views.MainWindowViews;
using Views.PopUpWindowViews;

namespace Service.AddNewWindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        public void CreateNewDataSourceWindow(object dataContext)
        {
            DataSourceView view = new DataSourceView();
            view.DataContext = dataContext;
            view.ShowDialog();
        }

        public void CreateNewLoginWindow(object dataContext)
        {
            LoginView view = new LoginView();
            view.DataContext = dataContext;
            view.ShowDialog();
        }
    }
}
