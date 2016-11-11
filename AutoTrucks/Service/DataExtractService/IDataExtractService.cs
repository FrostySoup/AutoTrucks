using Model.DataFromView;
using Model.ReceiveData.AlarmMatch;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataExtractService
{
    public interface IDataExtractService
    {
        ObservableCollection<PostDataFromView> ExtractEquipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData);

        ObservableCollection<PostDataFromView> ExtractShipmentFromData(LookupAssetSuccessData data, LookupAlarmSuccessData lookupAlarmSuccessData);

        DisplayFoundAsset ConvertContextToDisplayFoundAsset(Stream inputStream);
    }
}
