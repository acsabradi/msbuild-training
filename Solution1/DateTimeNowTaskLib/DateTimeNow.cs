using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeNowTaskLib
{
    public class DateTimeNow : Microsoft.Build.Utilities.Task
    {
        [Output]
        public string Now { get; set; }
        public bool IsUtc { get; set; }
        public bool DateOnly { get; set; }
        public override bool Execute()
        {
            var now = IsUtc ? DateTime.UtcNow : DateTime.Now;
            Now = DateOnly ? now.ToShortDateString() : now.ToString();
            return true;
        }
    }

    public class DateTimeOffsetter : Microsoft.Build.Utilities.Task
    {
        public int OffsetDays { get; set; }
        
        [Output]
        public DateTime OffsetDate { get; set; }
        public override bool Execute()
        {
            OffsetDate = DateTime.Now.AddDays(OffsetDays);
            return true;
        }
    }
}
