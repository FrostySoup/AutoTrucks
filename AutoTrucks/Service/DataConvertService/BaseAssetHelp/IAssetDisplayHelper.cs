using Model.ReceiveData.AlarmMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfmiServices.TfmiAlarmService;

namespace Service.DataConvertService.BaseAssetHelp
{
    public interface IAssetDisplayHelper
    {
        DisplayFoundAsset ConvertAssetIntoDisplayFoundAsset(TfmiServices.TfmiAlarmService.BaseAsset item, TfmiServices.TfmiAlarmService.FmeStatus status, TfmiServices.TfmiAlarmService.PostingCallback callback, bool ltl, TfmiServices.TfmiAlarmService.Dimensions dimensions, string basisAssetId);
    }
}
