using Climate_Watch.Models.Configurations;

namespace Climate_Watch.Repository;

public interface IConfigurationRepository {

    MonitoringModel GetMonitoringSettings();
    MonitoringModel UpdateMonitoringSettings(MonitoringModel model);
    
    
    DbConnectorModel GetDbConnectorSettings();
    DbConnectorModel UpdateDbConnectorSettings(DbConnectorModel model);
    
    
    InfluxDbModel GetInfluxDbSettings();
    InfluxDbModel UpdateInfluxDbSettings(InfluxDbModel model);
    
    
    HistoricalDataModel GetHistoricalDataSettings();
    HistoricalDataModel UpdateHistoricalDataSettings(HistoricalDataModel model);
    
    WebsiteDataModel GetWebsiteSettings();
    WebsiteDataModel RegisterWebsite();
    WebsiteDataModel UpdateWebsiteSettings(WebsiteDataModel model);
    
    UACDataModel GetUACSettings();
    UACDataModel UpdateUACSettings(UACDataModel model);
    
    TelegramDataModel GetTelegramSettings();
    TelegramDataModel UpdateTelegramSettings(TelegramDataModel model);
    
    GeneralDataModel GetGeneralSettings();
    GeneralDataModel UpdateGeneralSettings(GeneralDataModel model);
    
    

    
    
    
    
    
    
    
    
    
}