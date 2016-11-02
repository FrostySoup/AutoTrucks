using Model.DataFromView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataExtractService
{
    public interface IDataExtractService
    {
        ObservableCollection<PostDataFromView> ExtractEquipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData);

        ObservableCollection<PostDataFromView> ExtractShipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData);
    }
}
