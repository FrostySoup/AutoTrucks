using Model.DataFromView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataConvertService
{
    public interface IDataConvertPostAssetService
    {
        PostAssetOperation PostDataFromViewShipmentToBaseAsset(PostDataFromView postData);
        PostAssetOperation PostDataFromViewEquipmentToBaseAsset(PostDataFromView postData);
    }
}
