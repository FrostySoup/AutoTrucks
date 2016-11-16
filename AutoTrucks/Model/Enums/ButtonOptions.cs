using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enums
{
    public enum ButtonOptions
    {
        [Description("Add to blacklist")]
        AddToBlackList,
        [Description("Remove asset")]
        RemoveAsset,
        Call
    }
}
