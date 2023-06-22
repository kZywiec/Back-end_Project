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
        [Description("Operation of uplading file by user")]
        Upload,
        [Description("Operation of editing file by user")]
        Edit,
        [Description("Operation of downloading file by user")]
        Download,
        [Description("Operation of deleteing file by user")]
        Delete
    }
}
