using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace Service.ColorListHolder
{
    public interface IColorListHolder
    {
        ObservableCollection<ColorItem> GetColors();
    }
}
