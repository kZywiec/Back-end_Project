using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.LogEntities
{
    public enum ActionLog
    {
        [Description("Operation fo uplading file by user")]
        Upload,
        [Description("Operation fo file editing by user")]
        Edit,
        [Description("Operation fo file downloading by user")]
        Download
    }
}
