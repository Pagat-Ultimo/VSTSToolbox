using System.IO;

namespace VSTSToolbox.Constants
{
    public class FilePaths
    {
        public static string SettingsDirectory => @"Settings";
        public static string SettingsFileName => @"IPASettings.sl";
        public static string SettingsPath => Path.Combine(SettingsDirectory, SettingsFileName);
    }
}
