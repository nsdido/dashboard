using System.Net;
using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public class DeviceRepository : IDeviceRepository {
    public IEnumerable<DeviceModel> GetAll()
    {
        var url = "http://43.131.48.203:8083/catalog/device/list";


        using var client = new HttpClient();

        var response = client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<DeviceModel>();
        }

        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DefinedDevice>(responseBody);

        return result.List.ToList();
    }

    public void Create(DefinedEntity place)
    {
        throw new NotImplementedException();
    }

    public void Update(DefinedEntity place)
    {
        throw new NotImplementedException();
    }
}