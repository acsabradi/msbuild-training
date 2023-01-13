using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerLib
{
    public class CustomLogger : Logger
    {
        private StreamWriter streamWriter;
        private static readonly string logFilePath = "mylog.txt";
        public override void Initialize(IEventSource eventSource)
        {
            streamWriter = new StreamWriter(logFilePath);
            eventSource.TargetStarted += EventSource_TargetStarted;
            eventSource.TargetFinished += EventSource_TargetFinished;
            eventSource.TaskStarted += EventSource_TaskStarted;
            eventSource.TaskFinished += EventSource_TaskFinished;
        }

        private int currentIndent = 0;

        private void EventSource_TaskFinished(object sender, TaskFinishedEventArgs e)
        {
            streamWriter.WriteLine($"{new string(Enumerable.Repeat(' ', currentIndent * 4).ToArray())}{DateTime.Now}: Task {e.TaskName} finished.");
        }

        private void EventSource_TaskStarted(object sender, TaskStartedEventArgs e)
        {
            streamWriter.WriteLine($"{new string(Enumerable.Repeat(' ', currentIndent * 4).ToArray())}{DateTime.Now}: Task {e.TaskName} started...");
        }

        private void EventSource_TargetFinished(object sender, TargetFinishedEventArgs e)
        {
            currentIndent--;
            streamWriter.WriteLine($"{new string(Enumerable.Repeat(' ', currentIndent * 4).ToArray())}{DateTime.Now}: Target {e.TargetName} finished");
        }

        private void EventSource_TargetStarted(object sender, TargetStartedEventArgs e)
        {
            streamWriter.WriteLine($"{new string(Enumerable.Repeat(' ', currentIndent * 4).ToArray())}{DateTime.Now}: Target {e.TargetName} started...");
            currentIndent++;
        }

        public override void Shutdown()
        {
            streamWriter?.Close();
            base.Shutdown();
        }
    }
}
