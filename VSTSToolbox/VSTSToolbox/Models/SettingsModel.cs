using System;
using System.IO;
using Newtonsoft.Json;
using VSTSToolbox.Constants;

namespace VSTSToolbox.Models
{
    public class SettingsModel
    {
        public string TfsUrl { get; set; } = "www.dev.azure.com";
        public string PAT { get; set; } = "";

        public bool SaveSettings()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this);
                if (!Directory.Exists(FilePaths.SettingsDirectory))
                {
                    Directory.CreateDirectory(FilePaths.SettingsDirectory);
                }
                File.WriteAllText(FilePaths.SettingsPath, json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool LoadSettings()
        {
            try
            {
                if (!File.Exists(FilePaths.SettingsPath))
                    return false;
                var json = File.ReadAllText(FilePaths.SettingsPath);
                var setting = JsonConvert.DeserializeObject<SettingsModel>(json);
                TfsUrl = setting.TfsUrl;
                PAT = setting.PAT;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
