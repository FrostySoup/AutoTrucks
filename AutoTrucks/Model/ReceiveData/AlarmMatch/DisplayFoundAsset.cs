
using System.Windows.Media;

namespace Model.ReceiveData.AlarmMatch
{

    public class DisplayFoundAsset
    {
        public string AssetId { get; set; }
        public string Avail { get; set; }
        public string Truck { get; set; }
        public string FullOrPartial { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Length { get; set; }
        public string Weight { get; set; }
        public Brush BackgroundColor { get; set; }
    }
}
