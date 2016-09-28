using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views.MainWindowViews;

namespace Services.NewWindowFactory
{
    public class ProductionWindowFactory : IWindowFactory
    {
        public void CreateNewWindow()
        {
            LoginView view = new LoginView();
            view.Show();
        }
    }
}
