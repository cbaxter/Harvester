using Harvester.Core.Messaging.Sources;
using Harvester.Core.Messaging.Sources.DbWin;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace Harvester.Integration.Log4Net
{
    public sealed class HarvesterAppender : AppenderSkeleton
    {
        private readonly IWriteMessages writer;
        private readonly IMessageBuffer buffer;

        public HarvesterAppender()
        {
            Layout = new XmlLayoutSchemaLog4j();

            buffer = new SharedMemoryBuffer("Local\\HRVST_DBWIN", OutputDebugString.BufferSize);
            writer = new OutputDebugStringWriter("HarvestDBWinMutex", buffer);
        }

        protected override void OnClose()
        {
            base.OnClose();

            buffer.Dispose();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            writer.Write(RenderLoggingEvent(loggingEvent));
        }
    }
}
