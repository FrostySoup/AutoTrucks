using Model.DataFromView;
using Model.SearchCRUD;

namespace Model.DataToView
{
    public class SearchTrucksSearches
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

        public SearchTrucksSearches() { }

        public SearchTrucksSearches(SearchDataFromView searchData)
        {
            EquipmentClasses = searchData.equipmentType.ToString();
            Origin = searchData.originProvince.ToString();
            Destination = searchData.destinationProvince.ToString();
            Pickup = "UnknownField";
            DHO = searchData.dho.ToString();
            DHD = searchData.dhd.ToString();
            FP = searchData.fullOrPartial.ToString();
            Lenght = searchData.length;
            Weight = searchData.weight;
            this.SearchData = searchData;
        }
    }
}
