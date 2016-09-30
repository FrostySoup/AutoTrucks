using Model.SendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FillDataFactory
{
    public static class AddUserFactory
    {
        public static Login GetUser()
        {
            return new Login()
            {
                loginId = "TES7",
                password = "teservices",
                thirdPartyId = "SampleClient.NET"
            };
        }
    }
}
