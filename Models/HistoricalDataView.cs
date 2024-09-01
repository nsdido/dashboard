using Microsoft.AspNetCore.Mvc.Rendering;

namespace Climate_Watch.Models;

public class HistoricalDataView {
    public List<HistoricalData> Data { get; set; }
}

public class HistoricalData {
    public string SensorName { get; set; }
    public DateTime Time { get; set; }
    public double Value { get; set; }
}


public class SensorComboBoxViewModel
{
    public List<SelectListItem> SensorOptions { get; set; }
    public List<DefinedHome> PlaceOptions { get; set; }
    public string SelectedSensorId { get; set; }
    public long SelectedPlaceId { get; set; }
    
    public List<HistoricalData> Data { get; set; }
}



public class AddUserToPlaceViewModel
{
    public List<DefinedHome> PlaceOptions { get; set; }
    public long SelectedPlaceId { get; set; }
    public string NewUserName { get; set; }
}

public class UserInPlaceViewModel
{
    public List<DefinedHome> PlaceOptions { get; set; }
    public long SelectedPlaceId { get; set; }
    public List<string> Users { get; set; }
}



public class DefinedHome {
    public long PlaceID { get; set; }
    public string PlaceName { get; set; }
    public string RaspberryPiAddress { get; set; }
    public string PlaceAdminUsername { get; set; } = " ";
    public int? SendDataInterval { get; set; } = 0;
}


public class DefinedEntity {
    public string Entity { get; set; }
    public string Desc { get; set; }
}




public class DeviceModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Running_Status { get; set; }
    public int Auth_Status { get; set; }
    public string Created_At { get; set; }
    public string Updated_At { get; set; }
}

public class DefinedDevice
{
    public int Code { get; set; }
    public string Message { get; set; }
    public List<DeviceModel> List { get; set; }
}



public class TemperatureModel
{
    public string Measurement { get; set; }
    public double Value { get; set; }
    public string Created_at { get; set; }
    public string Start_at { get; set; }
    public string End_at { get; set; }
    public string Device { get; set; }
}

public class MeasuredTemperature
{
    public int Code { get; set; }
    public string Message { get; set; }
    public List<TemperatureModel> List { get; set; }
}




public class RuleModel
{
    public int Id { get; set; }
    public string Src { get; set; }
    public string Entity { get; set; }
    public string Field { get; set; }
    public string Compare { get; set; }
    public int Value { get; set; }
    public string Dst { get; set; }
    public string Opt { get; set; }
    public int Is_deleted { get; set; }
    public string Desc { get; set; }
    public string Created_at { get; set; }
    public string Updated_at { get; set; }
}

public class DefinedRule
{
    public int Code { get; set; }
    public string Message { get; set; }
    public List<RuleModel> List { get; set; }
}





public class SensorModel {
    public long PlaceId { get; set; }
    public string SensorName { get; set; }
    public bool State { get; set; }
}

public class DefinedHomeFromService
{
    public string PlaceAdminUsername { get; set; }
    public int PlaceID { get; set; }
    public string PlaceName { get; set; }
    public List<object> RaspberryPiAddress { get; set; }
    public int SendDataInterval { get; set; }
    public int UpdateConfigInterval { get; set; }
}

public class DefineHome {

    public long PlaceID { get; set; }
    public string PlaceName { get; set; }
    
    public string PlaceAdminUsername { get; set; }

    public string RaspberryPiAddress { get; set; }
    public int? SendDataInterval { get; set; }
}


public class AddUserToPlace
{
    public string AdminUsername { get; set; }
    public long PlaceID { get; set; }
}




public class PlaceViewModel
{
    public List<DefinedHome> Places { get; set; }
    public DefinedHome NewPlace { get; set; }
}

