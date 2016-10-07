using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enums
{
    public enum FullOrPartial
    {
        [Description("Full")]
        Full,
        [Description("Partial")]
        Partial,
        [Description("Both")]
        Both
    }
}
