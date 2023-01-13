using Microsoft.Build.Evaluation;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SevenZTaskLib
{
    public class SevenZ : ToolTask
    {

        private void ReInitMsBuildProjectDir()
        {
            using (XmlReader projectFileReader=XmlReader.Create(BuildEngine.ProjectFileOfTaskNode))
            {
                Project project = new Project(projectFileReader);
                MSBuildProjectDirectory = project.GetPropertyValue("MSBuildProjectDirectory");
            }
        }

        private string MSBuildProjectDirectory;
        public string OutputzipPath { get; set; }
        public string InputFolder { get; set; }

        protected override string ToolName => "7z.exe";

        private bool ProbeLocation(string location)
        {
            string probeLocation = Path.Combine(location, ToolName);
            Log.LogMessage($"Probing for 7z.exe at: {probeLocation}");
            if (File.Exists(probeLocation))
            {
                Log.LogMessage("7z.exe found!");
                return true;
            }
            return false;
        }

        protected override bool ValidateParameters()
        {
            if (!base.ValidateParameters())
                return false;

            if (!Directory.Exists(InputFolder))
            {
                Log.LogError($"Input folder {InputFolder} does not exist!");
                return false;
            }

            return true;
        }

        protected override string GenerateFullPathToTool()
        {
            ReInitMsBuildProjectDir();
            if (ProbeLocation(MSBuildProjectDirectory))
                return Path.Combine(MSBuildProjectDirectory, ToolName);

            if(ProbeLocation(@"C:\Program Files\7-Zip"))
                return Path.Combine(MSBuildProjectDirectory, ToolName);

            Log.LogWarning("7z.exe used from PATH");
            return ToolName;
        }

        protected override string GenerateCommandLineCommands()
        {
            // TODO: CommandLineBuilder
            return $"a {OutputzipPath} {InputFolder}";
        }
    }
}
