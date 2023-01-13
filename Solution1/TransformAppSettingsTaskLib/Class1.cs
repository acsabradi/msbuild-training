using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TransformAppSettingsTaskLib
{
    public class SumTask : Microsoft.Build.Utilities.Task
    {
        [Required]
        public System.Double Value1 { get; set; }

        [Required]
        public System.Double Value2 { get; set; }

        [Output]
        public System.Double Sum { get; set; }

        public override bool Execute()
        {
            Sum = Value1 + Value2;
            return true;
        }
    }
    public class TransformAppSettings : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string NewValue { get; set; }
        [Required]
        public string ConfigPath { get; set; }

        public override bool Execute()
        {
            if (!File.Exists(ConfigPath))
            {
                Log.LogError($"Specified config file path is invalid: {ConfigPath}");
                return false;
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigPath);
            var node = xmlDocument.SelectSingleNode($"/configuration/appSettings/add[@key='{Key}']");
            if (node==null)
            {
                Log.LogWarning($"AppSetting with key {Key} is not present");
                return true;
            }
            node.Attributes["value"].Value = NewValue;
            xmlDocument.Save(ConfigPath);
            return true;
        }
    }
}
