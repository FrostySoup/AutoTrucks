using Model.DataFromView;
using Model.SearchCRUD;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Media;

namespace Model.DataToView
{
    public class SearchAssetsSearches
    {
        public SearchDataFromView SearchData { get; set; }
        public string EquipmentClasses { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string Pickup { get; set; }

        public string DHO { get; set; }

        public string DHD { get; set; }

        public string FP { get; set; }

        public string Lenght { get; set; }

        public string Weight { get; set; }

        public Brush BackgroundColor { get; set; }

        public Brush ForegroundColor { get; set; }

        public bool Marked { get; set; }

        public string SearchBack { get; set; }

        public SearchAssetsSearches() { }

        public SearchAssetsSearches(SearchDataFromView searchData)
        {
            Marked = false;
            string equipment = "";
            foreach (var item in searchData.equipmentClasses)
            {
                equipment += item.ToString() + ", ";
            }
            BackgroundColor = new SolidColorBrush(searchData.backgroundColor);
            ForegroundColor = new SolidColorBrush(searchData.foregroundColor);
            EquipmentClasses = equipment;
            Origin = searchData.originProvince.ToString();
            Destination = searchData.destinationProvince.ToString();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Pickup = String.Format("{0:MM/dd}-{1:MM/dd}", searchData.availFrom, searchData.availTo);
            DHO = searchData.dho.ToString();
            DHD = searchData.dhd.ToString();
            FP = searchData.fullOrPartial.ToString();
            Lenght = searchData.length;
            Weight = searchData.weight;
            SearchBack = searchData.searchBack.ToString() + " hr";
            this.SearchData = searchData;
        }
    }
}
