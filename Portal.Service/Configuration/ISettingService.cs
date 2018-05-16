using Portal.core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Service.Configuration
{
   public  interface ISettingService
    {
        /// Gets a setting by identifier
        /// </summary>
        /// <param name="settingId">Setting identifier</param>
        /// <returns>Setting</returns>
        Setting GetSettingById(int settingId);

        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// Deletes settings
        /// </summary>
        /// <param name="settings">Settings</param>
        void DeleteSettings(IList<Setting> settings);

    }
}
