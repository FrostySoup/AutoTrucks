using System.Collections.Generic;

namespace Service.ConnexionService
{
    public interface ISessionCacheSingleton
    {
        List<ISessionFacade> sessions
        {
            get;
        }
        void RenewSessionsForEachData();
    }
}