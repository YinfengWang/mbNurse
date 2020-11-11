namespace AutoUpdate.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"), CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        [SpecialSetting(SpecialSetting.WebServiceUrl), DefaultSettingValue("http://192.168.2.109/MobileWebSrv/AutoUpdateWebSrv.asmx"), ApplicationScopedSetting, DebuggerNonUserCode]
        public string AutoUpdate_AuotUpdate_AutoUpdateWebSrv
        {
            get
            {
                return (string) this["AutoUpdate_AuotUpdate_AutoUpdateWebSrv"];
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }
    }
}

