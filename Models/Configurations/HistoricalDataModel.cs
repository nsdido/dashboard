namespace Climate_Watch.Models.Configurations;

public class HistoricalDataModel {
    public string Ip { get; set; }
    public string DbConnectorIP { get; set; }
    public string Status { get; set; }
    public int Port { get; set; }
    public string LastUpdate { get; set; }    
    }

public class WebsiteDataModel {
    public string Ip { get; set; }
    public string HistoricalDataIP { get; set; }
    public string UacIP { get; set; }
    public string Status { get; set; }
    
    
    public int Port { get; set; }
    
    
}

public class UACDataModel {
    public string Ip { get; set; }
    public int Port { get; set; }
    public string Status { get; set; }
    public string LastUpdate { get; set; }
}

public class TelegramDataModel {
    public string Ip { get; set; }    
    public string Status { get; set; }
    public string LastUpdate { get; set; }
    public string Token { get; set; }
    public string WarningTopic { get; set; }
    public string HistoricalDataIP { get; set; }
    public string UacIP { get; set; }    
    
}

public class GeneralDataModel {
    public string MessageBrokerIP { get; set; }
    public int MessageBrokerPort { get; set; }
    
    public int RegisterInterval { get; set; }
}
