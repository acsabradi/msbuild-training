using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildMessengerTaskLib
{
    public class BuildMessengerTask : Microsoft.Build.Utilities.Task
    {
        public override bool Execute()
        {
            Log.LogMessageFromText("Hello from nuget installed task!", Microsoft.Build.Framework.MessageImportance.High);
            return true;
        }
    }
}
