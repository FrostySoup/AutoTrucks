using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AddNewWindowFactory
{
    public interface IWindowFactory
    {
        void CloseWindowByName(string windowName);
        void CreateNewDataSourceWindow(object dataContext);
        void CreateNewLoginWindow(object loginViewModel);
        void CreateNewSearchWindow(object dataContext);
        void CreateNewRemoteConnectionWindow(object dataContext);
        void CreateNewPostAssetWindow(object dataContext);
        void CreateNewBlacklistWindow(object dataContext);
    }
}
