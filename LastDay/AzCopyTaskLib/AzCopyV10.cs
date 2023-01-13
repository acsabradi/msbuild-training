using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzCopyTaskLib
{
    public class AzCopyV10 : ToolTask
    {
        [Required]
        public string Destination { get; set; }

        public string SourceFile { get; set; }
        public string SourceDirectory { get; set; }
        public bool Recursive { get; set; }

        public string AzCopyPath { get; set; }

        protected override string ToolName => "azcopy.exe";

        protected override bool ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(AzCopyPath))
            {
                Log.LogWarning("AzCopy path not specified; using working directory or PATH");
            }
            else
            {
                if (!File.Exists(Path.Combine(AzCopyPath, ToolName)))
                {
                    Log.LogError($"azcopy.exe not found at {AzCopyPath}");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(SourceFile) && string.IsNullOrWhiteSpace(SourceDirectory))
            {
                Log.LogError("Either sourcefile or sourcedirectory must be specified!");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(SourceFile) && !string.IsNullOrWhiteSpace(SourceDirectory))
            {
                Log.LogError("Either sourcefile or sourcedirectory must be specified!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Destination))
            {
                Log.LogError("Destination must not be empty!");
                return false;
            }

            if (Recursive && string.IsNullOrWhiteSpace(SourceDirectory))
            {
                Log.LogError("Recursive mode is only available for directories!");
                return false;
            }
            azCopyComputedPath = string.IsNullOrWhiteSpace(AzCopyPath) ? ToolName : Path.Combine(AzCopyPath, ToolName);

            return base.ValidateParameters();
        }

        private string azCopyComputedPath;

        protected override string GenerateFullPathToTool() => azCopyComputedPath;
        protected override string GenerateCommandLineCommands()
        {
            var commandLineBuilder = new CommandLineBuilder();
            commandLineBuilder.AppendSwitch("copy");
            commandLineBuilder.AppendFileNameIfNotNull(SourceFile);
            if (!string.IsNullOrWhiteSpace(SourceDirectory))
            {
                commandLineBuilder.AppendFileNameIfNotNull(Path.Combine(SourceDirectory, "*"));
            }
            commandLineBuilder.AppendSwitch(Destination);
            if (Recursive)
            {
                commandLineBuilder.AppendSwitch("--recursive");
            }
            return commandLineBuilder.ToString();
        }
    }
}
