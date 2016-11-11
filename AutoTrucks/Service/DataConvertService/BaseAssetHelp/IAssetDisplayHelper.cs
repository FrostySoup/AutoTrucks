using Model.ReceiveData.AlarmMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService.BaseAssetHelp
{
    public interface IAssetDisplayHelper
    {
        DisplayFoundAsset ConvertAssetToDisplayFoundAsset(BaseAsset baseAsset, FmeStatus status, PostingCallback callback, bool ltl, Dimensions dimensions, string assetId);
    }
}
