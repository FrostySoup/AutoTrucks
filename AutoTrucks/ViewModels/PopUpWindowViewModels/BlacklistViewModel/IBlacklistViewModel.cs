using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PopUpWindowViewModels.BlacklistViewModel
{
    public interface IBlacklistViewModel : INotifyPropertyChanged
    {
        void RefreshBlacklist();
    }
}
