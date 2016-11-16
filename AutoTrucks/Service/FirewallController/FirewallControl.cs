using NATUPNPLib;
using NETCONLib;
using NetFwTypeLib;
using System;

namespace Service.FirewallController
{
    public class FirewallControl : IFirewallControl
    {
        private const string CLSID_FIREWALL_MANAGER =
  "{304CE942-6E39-40D8-943A-B913C40C9CD4}";
        private static NetFwTypeLib.INetFwMgr GetFirewallManager()
        {
            Type objectType = Type.GetTypeFromCLSID(
              new Guid(CLSID_FIREWALL_MANAGER));
            return Activator.CreateInstance(objectType)
              as NetFwTypeLib.INetFwMgr;
        }

        public void AddNewPortToFirewall(string port)
        {
            INetFwMgr manager = GetFirewallManager();
            bool isFirewallEnabled =
              manager.LocalPolicy.CurrentProfile.FirewallEnabled;
            if (isFirewallEnabled == false)
                manager.LocalPolicy.CurrentProfile.FirewallEnabled = true;

            int portResults;
            if (Int32.TryParse(port, out portResults))
            {
                GloballyOpenPort(portResults, NET_FW_SCOPE_.NET_FW_SCOPE_ALL, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP, NET_FW_IP_VERSION_.NET_FW_IP_VERSION_ANY);
            }
        }

        private const string PROGID_OPEN_PORT = "HNetCfg.FWOpenPort";

        private bool GloballyOpenPort(int portNo,
          NET_FW_SCOPE_ scope, NET_FW_IP_PROTOCOL_ protocol,
          NET_FW_IP_VERSION_ ipVersion)
        {
            Type type = Type.GetTypeFromProgID(PROGID_OPEN_PORT);
            INetFwOpenPort port = Activator.CreateInstance(type)
             as INetFwOpenPort;
            port.Name = "Alarm match";
            port.Port = portNo;
            port.Scope = scope;
            port.Protocol = protocol;
            port.IpVersion = ipVersion;
            INetFwMgr manager = GetFirewallManager();
            try
            {
                manager.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(port);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
