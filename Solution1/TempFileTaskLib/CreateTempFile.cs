using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempFileTaskLib
{
    public class CreateTempFile : Microsoft.Build.Utilities.Task
    {
        [Output]
        public ITaskItem TempFile { get; set; }
        public override bool Execute()
        {
            try
            {
                var tempFileName = System.IO.Path.GetTempFileName();
                TempFile = new TaskItem(tempFileName);
                //TempFile.SetMetadata()
                return true;
            }
            catch (Exception ex) 
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }
    }
}
