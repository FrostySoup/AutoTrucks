using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AddNewWindowFactory
{
    public interface IWindowFactory
    {
        void CreateNewWindow(object dataContext);
    }
}
