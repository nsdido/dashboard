using System.Net;
using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public class TemperatureRepository : ITemperatureRepository {
    public IEnumerable<TemperatureModel> GetAll(DateTime start_at)
    {
        var url = "http://43.131.48.203:8083/data/temperature/list?start_at=" + "2024-05-12 18:10:30";


        using var client = new HttpClient();

        var response = client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<TemperatureModel>();
        }

        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MeasuredTemperature>(responseBody);

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