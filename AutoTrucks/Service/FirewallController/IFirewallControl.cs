using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FirewallController
{
    public interface IFirewallControl
    {
        void AddNewPortToFirewall(string port);
    }
}
