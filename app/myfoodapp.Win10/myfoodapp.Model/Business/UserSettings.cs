namespace myfoodapp.Business
{
    public class UserSettings
    {
        public bool isDebugLedEnable { get; set; }
        public bool isScreenSaverEnable { get; set; }
        public bool isSleepModeEnable { get; set; }
        public bool isSigFoxComEnable { get; set; }
        public bool isDiagnosticModeEnable { get; set; }
        public bool isTempHumiditySensorEnable { get; set; }
        public int measureFrequency { get; set; }
        public string productionSiteId { get; set; }
        public string hubMessageAPI { get; set; }
        public string SSID { get; set; }
        public string ACCESS_POINT_PWD { get; set; }
        public string PackageVersion { get; set; }
    }
}
