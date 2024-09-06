using System.Diagnostics;
using System.Globalization;
using Climate_Watch.Data;
using Climate_Watch.Models;
using Climate_Watch.Repository;
using CW_Website.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Climate_Watch.Controllers {
    public class HomeController : Controller {
        private ApplicationDbContext _context;
        private IHistoricalDataRepository _historicalDataRepository;
        private IPlaceRepository _placeRepository;
        private IEntityRepository _entityRepository;
        private IDeviceRepository _deviceRepository;
        private ITemperatureRepository _temperatureRepository;
        private IConfigurationRepository _configurationRepository;
        private IServiceRepository _serviceRepository;
        private IHumidityRepository _humidityRepository;
        private ILightRepository _lightRepository;
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context,
            IHistoricalDataRepository historicalDataRepository,
            IPlaceRepository placeRepository,
            IConfigurationRepository configurationRepository,
            IEntityRepository entityRepository,
            IDeviceRepository deviceRepository,
            ITemperatureRepository temperatureRepository,
            IServiceRepository serviceRepository,
            IHumidityRepository humidityRepository,
            ILightRepository lightRepository)
        {
            _logger = logger;
            _context = context;
            _historicalDataRepository = historicalDataRepository;
            _placeRepository = placeRepository;
            _configurationRepository = configurationRepository;
            _entityRepository = entityRepository;
            _deviceRepository = deviceRepository;
            _temperatureRepository = temperatureRepository;
            _serviceRepository = serviceRepository;
            _humidityRepository = humidityRepository;
            _lightRepository = lightRepository;
        }

        public IActionResult Index()
        {
            _configurationRepository.RegisterWebsite();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSensors(long placeId)
        {
            var sensors = new List<SensorModel>()
            {
                new SensorModel()
                {
                    PlaceId = 1,
                    SensorName = "Temperature",
                    State = true
                },
                new SensorModel()
                {
                    PlaceId = 1,
                    SensorName = "Smoke",
                    State = false
                },
                new SensorModel()
                {
                    PlaceId = 1,
                    SensorName = "Humidity",
                    State = true
                }
            };
            return PartialView("_SensorList", sensors);
        }


        [HttpPost]
        public IActionResult UpdateSensors(List<SensorModel> sensors)
        {
            foreach (var sensor in sensors)
            {
                // var existingSensor = _context.Sensors.Find(sensor.SensorID);
                // if (existingSensor != null)
                // {
                //     existingSensor.State = sensor.State;
                //     _context.Sensors.Update(existingSensor);
                // }
            }

            // _context.SaveChanges();
            return Json(new { success = true });
        }

        [Authorize]
        public IActionResult HistoricalDataView()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var places = placesFromService.Select(_ => new DefinedHome()
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName
            }).ToList();

            var model = new SensorComboBoxViewModel
            {
                SensorOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "temperature", Text = "Temperature" },
                    new SelectListItem { Value = "smoke", Text = "Smoke" },
                    new SelectListItem { Value = "humidity", Text = "Humidity" }
                },
                PlaceOptions = places,
                Data = new List<HistoricalData>()
            };

            return View(model);

            // return View(result);
        }

        [HttpPost]
        public IActionResult HistoricalDataView(SensorComboBoxViewModel model)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var _places = placesFromService.Select(_ => new DefinedHome
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName,
                PlaceAdminUsername = _.PlaceAdminUsername,
                SendDataInterval = _.SendDataInterval,
            }).ToList();

            if (model.SelectedPlaceId == 0 || string.IsNullOrEmpty(model.SelectedSensorId))
            {
                ViewBag.Message = "You have to select one place and one sensor to see the result";

                return View(new SensorComboBoxViewModel
                {
                    SensorOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "temperature", Text = "Temperature" },
                        new SelectListItem { Value = "smoke", Text = "Smoke" },
                        new SelectListItem { Value = "humidity", Text = "Humidity" }
                    },
                    PlaceOptions = _places.Select(_ => new DefinedHome
                    {
                        PlaceID = _.PlaceID,
                        PlaceName = _.PlaceName
                    }).ToList(),
                    Data = new List<HistoricalData>()
                });
            }

            var data = _historicalDataRepository.GetHistoricalData(model.SelectedSensorId, model.SelectedPlaceId);

            var result = new SensorComboBoxViewModel
            {
                SensorOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "temperature", Text = "Temperature" },
                    new SelectListItem { Value = "smoke", Text = "Smoke" },
                    new SelectListItem { Value = "humidity", Text = "Humidity" }
                },
                PlaceOptions = _places.Select(_ => new DefinedHome
                {
                    PlaceID = _.PlaceID,
                    PlaceName = _.PlaceName
                }).ToList(),
                Data = data.ToList()
            };

            // if (!string.IsNullOrEmpty(model.SelectedSensorId))
            // {
            //     string mn = char.ToUpper(model.SelectedSensorId[0]) + model.SelectedSensorId.Substring(1);
            //
            //     result.Data = result.Data.Where(_ => _.SensorName ==mn ).ToList();
            // }
            return View(result);
        }

        public static string ToCamelCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Split the string into words
            string[] words = text.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

            // Handle the first word separately
            string camelCaseText = words[0].ToLower(CultureInfo.InvariantCulture);

            // Process remaining words
            for (int i = 1; i < words.Length; i++)
            {
                string word = words[i];
                camelCaseText += char.ToUpper(word[0], CultureInfo.InvariantCulture) +
                                 word.Substring(1).ToLower(CultureInfo.InvariantCulture);
            }

            return camelCaseText;
        }

        public IActionResult DefinedEntityView()
        {
            var result = _entityRepository.GetEntities();

            return View(result);
        } 
        
        public IActionResult DefinedService()
        {
            var result = _serviceRepository.GetAll();

            return View(result);
        }
        
        public IActionResult DefinedServiceView()
        {
            //var result = IServiceRepository.GetAll();

            var result = _serviceRepository.GetAll();

            return View(result);
        }
        
        public IActionResult DefinedDevice()
        {
            var result = _deviceRepository.GetAll();

            return View(result);
        }
        
        public IActionResult DefinedDeviceView()
        {
            //var result = IServiceRepository.GetAll();

            var result = _deviceRepository.GetAll();

            return View(result);
        }
        public IActionResult MeasuredTemperatureView()
        {
            var result = _temperatureRepository.GetAll(DateTime.Now);

            return View(result);
        }

        public IActionResult MeasuredHumidityView()
        {
            var result = _humidityRepository.GetAll(DateTime.Now);

            return View(result);
        }
        
        
        public IActionResult DefinedLight()
        {
            var result = _lightRepository.GetAll(DateTime.Now);

            return View(result);
        }

        public IActionResult MeasuredLightView()
        {
            var result = _lightRepository.GetAll(DateTime.Now);

            return View(result);
        }
        
        
        //====================================================== api must be deleted
        public IActionResult DefinedPlaceView()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var _places = placesFromService.Select(_ => new DefinedHome
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName,
                PlaceAdminUsername = _.PlaceAdminUsername,
                SendDataInterval = _.SendDataInterval,
                RaspberryPiAddress = _.RaspberryPiAddress
            }).ToList();

            var result = _places;

            return View(result);
        }

        public IActionResult CreatePlaceView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePlaceView(DefinedHome place)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (ModelState.IsValid)
            {
                _placeRepository.CreatePlace(new DefineHome()
                {
                    PlaceName = place.PlaceName,
                    PlaceAdminUsername = userName,
                    SendDataInterval = place.SendDataInterval,
                    RaspberryPiAddress = place.RaspberryPiAddress
                });
                return RedirectToAction("DefinedPlaceView");
            }

            return View(place);
        }

        public IActionResult EditPlaceView(int id)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var _places = placesFromService.Select(_ => new DefinedHome
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName,
                PlaceAdminUsername = _.PlaceAdminUsername,
                SendDataInterval = _.SendDataInterval,
                RaspberryPiAddress = _.RaspberryPiAddress
            }).ToList();

            var place = _places.FirstOrDefault(p => p.PlaceID == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        [HttpPost]
        public IActionResult EditPlaceView(DefinedHome place)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var _places = placesFromService.Select(_ => new DefinedHome
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName,
                PlaceAdminUsername = _.PlaceAdminUsername,
                SendDataInterval = _.SendDataInterval,
                RaspberryPiAddress = _.RaspberryPiAddress
            }).ToList();

            if (!ModelState.IsValid)
                return View(place);

            var existingPlace = _places.FirstOrDefault(p => p.PlaceID == place.PlaceID);
            if (existingPlace == null)
                return View(place);

            var placeEntity = new DefineHome
            {
                PlaceID = place.PlaceID,
                PlaceName = place.PlaceName,
                PlaceAdminUsername = place.PlaceAdminUsername,
                RaspberryPiAddress = place.RaspberryPiAddress,
                SendDataInterval = place.SendDataInterval,
            };

            _placeRepository.UpdatePlace(placeEntity);

            return RedirectToAction("DefinedPlaceView");
        }

        [Route("/ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //=====================

        [Authorize]
        public IActionResult UserInPlaceView()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var places = placesFromService.Select(_ => new DefinedHome()
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName
            }).ToList();

            var model = new UserInPlaceViewModel
            {
                PlaceOptions = places,
                Users = new List<string>()
            };
            return View(model);

            // return View(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UserInPlaceView(UserInPlaceViewModel model)
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var users = _placeRepository.GetUsersInPlace(model.SelectedPlaceId);

            var placesFromService = _placeRepository.GetPlaces(userName);
            var places = placesFromService.Select(_ => new DefinedHome()
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName
            }).ToList();

            var result = new UserInPlaceViewModel
            {
                PlaceOptions = places,
                Users = users.ToList()
            };
            return View(result);

            // return View(result);
        }


        [Authorize]
        public IActionResult AddUserToPlaceView()
        {
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var places = placesFromService.Select(_ => new DefinedHome()
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName
            }).ToList();

            var model = new AddUserToPlaceViewModel
            {
                PlaceOptions = places,
                NewUserName = ""
            };
            return View(model);

            // return View(result);
        }


        [HttpPost]
        public IActionResult AddUserToPlaceView(AddUserToPlaceViewModel model)
        {
            _placeRepository.AddUserToPlace(new AddUserToPlace()
            {
                PlaceID = model.SelectedPlaceId,
                AdminUsername = model.NewUserName
            });


            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var placesFromService = _placeRepository.GetPlaces(userName);
            var places = placesFromService.Select(_ => new DefinedHome()
            {
                PlaceID = _.PlaceID,
                PlaceName = _.PlaceName
            }).ToList();

            var result = new AddUserToPlaceViewModel
            {
                PlaceOptions = places,
                NewUserName = ""
            };
            return View(result);
        }
    }
}