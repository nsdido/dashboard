namespace Climate_Watch.Models.Configurations;

public class InfluxDbModel {
    
    public string InfluxIP { get; set; }
    public string InfluxName { get; set; }
    public string InfluxPassword { get; set; }
    public int InfluxPort { get; set; }
    public string InfluxUsername { get; set; }
    public string LastUpdate { get; set; }
}