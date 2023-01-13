using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWTaskLib
{
    public class HelloWorldTask : ITask
    {
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            var taskLoggingHelper = new TaskLoggingHelper(this);
            taskLoggingHelper.LogMessageFromText("Hello world", MessageImportance.High);
            if (new Random().Next()%2==0)
            {
                taskLoggingHelper.LogError("Nem volt szerencsénk :(");
                return false;
            }
            return true;
        }
    }
}
