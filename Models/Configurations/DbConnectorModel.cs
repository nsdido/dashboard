namespace Climate_Watch.Models.Configurations;

public class DbConnectorModel {
    public string Ip { get; set; }
    public int Port { get; set; }
    public string Status { get; set; }
    public string GetSensorDataTopic { get; set; }
    public string LastUpdate { get; set; }
}