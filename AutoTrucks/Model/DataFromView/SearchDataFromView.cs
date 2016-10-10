using Model.Enums;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Model.DataFromView
{
    public class SearchDataFromView
    {
        public SearchRadius origin { get; set; }

        public SearchRadius destination { get; set; }

        public ObservableCollection<EquipmentClass> equipmentClasses { get; set; }

        public ObservableCollection<EquipmentType> equipmentType { get; set; }

        public int dho { get; set; }

        public int dhd { get; set; }

        public string length { get; set; }

        public string weight { get; set; }

        public string cityOrigin { get; set; }

        public string cityDestination { get; set; }

        public int searchBack { get; set; }

        public bool includeFulls { get; set; }

        public bool includeLtls { get; set; }

        public DateTime availFrom { get; set; }

        public DateTime availTo { get; set; }

        public StateProvince originProvince { get; set; }

        public StateProvince destinationProvince { get; set; }

        public Color? backgroundColor { get; set; }

        public Color? foregroundColor { get; set; }

        public FullOrPartial fullOrPartial { get; set; }

        public SearchDataFromView()
        {
            availFrom = DateTime.Now;
            availTo = DateTime.Now;
            backgroundColor = Color.FromRgb(0, 0, 0);
            foregroundColor = Color.FromRgb(0, 0, 0);
            equipmentType = new ObservableCollection<EquipmentType>();
        }
    }
}
