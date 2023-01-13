using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalSizeTaskLib
{
    public class TotalSize : Microsoft.Build.Utilities.Task
    {
        public ITaskItem[] SourceItems { get; set; }
        [Output]
        public long Size { get; set; }

        public override bool Execute()
        {
            try
            {
                long totalSize = 0;
                foreach (var source in SourceItems)
                {
                    string path = source.GetMetadata("FullPath");
                    totalSize = totalSize + new FileInfo(path).Length;
                }
                Size = totalSize;
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
