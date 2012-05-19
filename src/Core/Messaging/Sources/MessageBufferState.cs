using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harvester.Core.Messaging.Sources
{
    public enum MessageBufferState
    {
        Connected,
        Broken,
        Closed
    }
}
