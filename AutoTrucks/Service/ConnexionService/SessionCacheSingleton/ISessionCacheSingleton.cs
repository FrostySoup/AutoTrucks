
using System.Collections.Generic;

namespace Service.ConnexionService
{
    public interface ISessionCacheSingleton
    {
        List<ISessionFacade> sessions
        {
            get;
        }

        System.Uri defaultURL { get; }

        void RenewSessionsForEachData();
    }
}