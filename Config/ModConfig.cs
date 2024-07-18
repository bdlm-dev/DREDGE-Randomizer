using System.Reflection;
using Winch.Core;

namespace Randomizer.Config
{
    public class ModConfig : JSONConfig
    {
        private static Dictionary<string, string> DefaultConfigs = new();
        private static Dictionary<string, ModConfig> Instances = new();
        private const string defaultConfigFile = "config.json";

        private ModConfig(string modName, string fileName) : base(GetConfigPath(modName, fileName), GetDefaultConfig(modName))
        {
        }

        private static string GetDefaultConfig(string modName)
        {
            if (!DefaultConfigs.ContainsKey(modName))
            {
                //WinchCore.Log.Debug($"No 'DefaultConfig' attribute found in mod_meta.json for {modName}");
                return "{}";
            }
            return DefaultConfigs[modName];
        }

        private static string GetConfigPath(string configLocation, string fileName)
        {
            // ripped config handling- dont need to get to via root/mod/config
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string output = Path.Combine(basePath, fileName);
            if (Directory.Exists(Path.Combine(basePath, configLocation)))
                return output;
            Directory.CreateDirectory(Path.Combine("Config", configLocation));
            return output;
        }

        private static ModConfig GetConfig(string modName, string fileName, string subDirectory)
        {
            string _path = Path.Combine(modName, subDirectory, fileName);
            WinchCore.Log.Debug("getconfig " + _path);
            if (!Instances.ContainsKey(_path))
                Instances.Add(_path, new ModConfig(_path, fileName));
            return Instances[_path];
        }

        public static T? GetProperty<T>(string modName, string key, T? defaultValue, string fileName = defaultConfigFile, string subDirectory = "")
        {
            return GetConfig(modName, fileName, subDirectory).GetProperty(key, defaultValue);
        }

        public static void RegisterDefaultConfig(string modName, string config)
        {
            DefaultConfigs.Add(modName, config);
        }

        public static Dictionary<string, object?> GetFullConfig(string modName, string fileName = defaultConfigFile, string subDirectory = "")
        {
            return GetConfig(modName, fileName, subDirectory).Config;
        }                                                                                                                                            
    }
}