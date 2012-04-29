using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using NLog;
using NLog.Layouts;
using NLog.Targets;

namespace Harvester.Integration.NLog
{
    [Target("Harvester")]
    public sealed class HarvesterTarget : TargetWithLayout
    {
        private readonly IWriteMessages writer;
        private readonly IMessageBuffer buffer;

        public HarvesterTarget()
        {
            Layout = new Log4JXmlEventLayout();

            buffer = new SharedMemoryBuffer("Local\\HRVST_DBWIN", OutputDebugString.BufferSize);
            writer = new OutputDebugStringWriter("HarvestDBWinMutex", buffer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                buffer.Dispose();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            writer.Write(Layout.Render(logEvent));
        }
    }
}

