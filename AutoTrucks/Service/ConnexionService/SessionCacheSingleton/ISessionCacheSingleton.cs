
using Model.DataFromView;
using System.Collections.Generic;

namespace Service.ConnexionService
{
    public interface ISessionCacheSingleton
    {
        List<ISessionFacade> sessions
        {
            get;
        }

        RemoteConnection remoteURI
        {
            get;
        }

        void UpdateAlarmAdress();

        void RenewSessionsForEachData();
    }
}