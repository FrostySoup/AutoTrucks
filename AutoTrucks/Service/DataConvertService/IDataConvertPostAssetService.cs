using Model.DataFromView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ReceiveData.AlarmMatch;
using System.IO;

namespace Service.DataConvertService
{
    public interface IDataConvertPostAssetService
    {
        PostAssetOperation PostDataFromViewShipmentToBaseAsset(PostDataFromView postData);
        PostAssetOperation PostDataFromViewEquipmentToBaseAsset(PostDataFromView postData);
    }
}
