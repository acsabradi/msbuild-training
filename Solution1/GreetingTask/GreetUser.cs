using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSBuildTask = Microsoft.Build.Utilities.Task;

namespace GreetingTask
{
    public abstract class TaskBase : MSBuildTask
    {
        protected abstract void Validate();
        protected abstract void ExecuteInternal();
        public override sealed bool Execute()
        {
            try
            {
                Validate();
                ExecuteInternal();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }
    }

    public class GretUser2 : TaskBase
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Output]
        public string FullName { get; set; }
        protected override void ExecuteInternal()
        {
            Log.LogMessageFromText($"Hello {FirstName} {LastName}", MessageImportance.High);
            FullName = $"{FirstName} {LastName}";
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("FirstName cannot be empty", nameof(FirstName));

        }


        public class GreetUser : MSBuildTask
        {
            [Required]
            public string FirstName { get; set; }
            public string LastName { get; set; }

            [Output]
            public string FullName { get; set; }
            public override bool Execute()
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    Log.LogError("Firstname is invalid");
                    return false;
                }
                Log.LogMessageFromText($"Hello {FirstName} {LastName}", MessageImportance.High);
                FullName = $"{FirstName} {LastName}";
                return true;
            }
        }
    }
}
