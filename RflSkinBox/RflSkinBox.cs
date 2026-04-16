using Newtonsoft.Json;
using Oxide.Core;
using System;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("RflSkinBox", "H57", "0.0.0")]
    class RflSkinBox : RustPlugin
    {
        #region ConfigurationFileAndLoading

        // Class that contains all the configuration file properties.
        class ConfigData
        {
            [JsonProperty(PropertyName = "skinbox command")]
            public string skinboxcommand = "rflskin";

        }

        // declaration on a var that holds the properties data in the configuration file.
        private ConfigData? configurationData;


        // 
        private bool loadConfigurationVariables()
        {
            try
            {
                configurationData = Config.ReadObject<ConfigData>();
            }
            catch
            {
                configurationData = new ConfigData();
                return false;
            }
            saveConfig(configurationData);
            return true;
        }

        // takes the Data in configurationData and writes it into config file.
        private void saveConfig(ConfigData config)
        {
            Config.WriteObject(config, true);
        }

        // if no configuration files exist creates one with the default value in ConfigData
        protected override void LoadDefaultConfig()
        {
            Puts("Creating new configuration file with default value...");
            configurationData = new ConfigData();
            saveConfig(configurationData);
        }

        // on server init register a new permission to grant to group or user, then checks if the function loadConfigurationVariables return true
        // if loadConfigurationVariables return true it means that that there was no problem when trying to read data from configuration file.
        // if loadConfigurationVariables return false there was an error when trying to read data from configuration file is so it loads default values from configuration file
        void Init()
        {
            permission.RegisterPermission("hskinbox.vip", this);
            if (!loadConfigurationVariables())
            {
                Puts("Error while loading config variables, loading default config properties. Delete config file and reload.");
            }
        }
        #endregion

        private string _steamApiKey = "3D3B9AD5A9BE44BCC613433683A61212";
        private int _rustAppId = 252490;

        class SkinInfo
        {
            public string _classId { get; set; }
            public string _name { get; set; }
            public string _imageUrl { get; set; }
        }

        private Dictionary<string, SkinInfo> _skins = new Dictionary<string, SkinInfo>();

        private void GetAssetPrices()
        {
            string steamUrl = "https://api.steampowered.com/ISteamEconomy/GetAssetPrices/v1/?" + $"key={_steamApiKey}&appid={_rustAppId}&currency=1";

            webrequest.Enqueue(steamUrl, null, (code, response) =>
            {
                if (code != 200 || string.IsNullOrEmpty(response))
                {
                    Puts($"getAssetClassId error: {code}");
                    return;
                }

                var data = JsonConvert.DeserializeObject<SkinInfo>(response);

                foreach (var asset in data.assets)
                {

                }

                // ici tu traites le JSON
            }, this, Core.Libraries.RequestMethod.GET, null);

        }



    }
}
