using System.Net;
using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public class LightRepository : ILightRepository{
    
    public IEnumerable<LightModel> GetAll(DateTime start_at)
    {
        var url = "http://43.131.48.203:8083/data/Light/list?start_at=" + "2024-05-12 18:10:30";


        using var client = new HttpClient();

        var response = client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<LightModel>();
        }

        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MeasuredLightView>(responseBody);

        return result.list.ToList();
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