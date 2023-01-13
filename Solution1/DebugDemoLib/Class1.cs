using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDemoLib
{
    public class DebugTask : Microsoft.Build.Utilities.Task
    {
        public string InputValue { get; set; }

        [Output]
        public string Output { get; set; }
        public override bool Execute()
        {
            //Debugger.Launch();

            //while(!Debugger.IsAttached) { }

            var j = InputValue.Length;
            string m = new string(Enumerable.Repeat('A', j).ToArray());
            Output = m;
            return true;
        }

        public DebugTask()
        {

        }
    }
}
