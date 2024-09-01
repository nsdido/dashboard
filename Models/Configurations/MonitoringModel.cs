namespace Climate_Watch.Models.Configurations;

public class MonitoringModel {
    public string Status { get; set; }
    public string Ip { get; set; }
    public int Port { get; set; }
    
    public string GetConfigTopic { get; set; }
    public string WarningTopic { get; set; }
    public string HistoricalDataIP { get; set; }
    public int FetchDataInterval { get; set; }
    public int TemperatureThreshold { get; set; }
    public int HumidityThreshold { get; set; }
    public int SmokeThreshold { get; set; }
    public int DeleteDataInterval { get; set; }
    public string LastUpdate { get; set; }
}



public enum MonitoringStatus {
    enable=0,
    disable=1
}