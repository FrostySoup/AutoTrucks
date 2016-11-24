using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfmiServices.TfmiAlarmService;

namespace Service.DataConvertService.LocationHelp
{
    public interface ILocationHelper
    {
        string GeographicLocationToString(GeographicLocation geographicLocation);
        string GeographicLocationToStringAlarmService(TfmiServices.TfmiAlarmService.GeographicLocation geographicLocation);
    }
}
