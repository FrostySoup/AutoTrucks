using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ViewModels.MainWindowViewModels
{
    public class TopButtonsViewModel : ITopButtonsViewModel
    {


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
