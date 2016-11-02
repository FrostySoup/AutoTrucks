using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.ConnexionService.AlarmService
{
    public interface IAlarmService
    {
        int Execute(ISessionFacade session, Uri alarmUrl);
    }
}
