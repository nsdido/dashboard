using Climate_Watch.Models.Configurations;
using Climate_Watch.Repository;
using CW_Website.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Climate_Watch.Controllers {
    public class ConfigurationController(IConfigurationRepository configurationRepository) : Controller {
        private readonly IConfigurationRepository _configurationRepository = configurationRepository;


        [Authorize]
        public IActionResult MonitoringSettingsView()
        {
            var monitoringSettings = _configurationRepository.GetMonitoringSettings();
            return View(monitoringSettings);
        }

        [HttpPost]
        public IActionResult MonitoringSettingsView(MonitoringModel model)
        {
            var monitoringSettings = _configurationRepository.UpdateMonitoringSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult DbConnectorSettingsView()
        {
            var model = _configurationRepository.GetDbConnectorSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult DbConnectorSettingsView(DbConnectorModel model)
        {
            // var monitoringSettings = _monitoringRepository.UpdateMonitoringSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult InfluxDbSettingsView()
        {
            var model = _configurationRepository.GetInfluxDbSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult InfluxDbSettingsView(InfluxDbModel model)
        {
            _configurationRepository.UpdateInfluxDbSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult HistoricalDataSettingsView()
        {
            var model = _configurationRepository.GetHistoricalDataSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult HistoricalDataSettingsView(HistoricalDataModel model)
        {
             _configurationRepository.UpdateHistoricalDataSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult WebsiteSettingsView()
        {
            var model = _configurationRepository.GetWebsiteSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult WebsiteSettingsView(WebsiteDataModel model)
        {
            _configurationRepository.UpdateWebsiteSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult UACSettingsView()
        {
            var model = _configurationRepository.GetUACSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult UACSettingsView(UACDataModel model)
        {
            _configurationRepository.UpdateUACSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult TelegramSettingsView()
        {
            var model = _configurationRepository.GetTelegramSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult TelegramSettingsView(TelegramDataModel model)
        {
            _configurationRepository.UpdateTelegramSettings(model);

            return View(model);
        }


        [Authorize]
        public IActionResult GeneralSettingsView()
        {
            var model = _configurationRepository.GetGeneralSettings();
            return View(model);
        }

        [HttpPost]
        public IActionResult GeneralSettingsView(GeneralDataModel model)
        {
            _configurationRepository.UpdateGeneralSettings(model);

            return View(model);
        }
        
    }
}