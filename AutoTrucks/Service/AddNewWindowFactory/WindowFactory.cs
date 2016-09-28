using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views.MainWindowViews;

namespace Service.AddNewWindowFactory
{
    public class WindowFactory : IWindowFactory
    {
        public void CreateNewWindow(object dataContext)
        {
            LoginView view = new LoginView();
            view.DataContext = dataContext;
            view.ShowDialog();
        }
    }
}
