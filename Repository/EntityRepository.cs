using System.Net;
using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public class EntityRepository : IEntityRepository {
    public IEnumerable<DefinedEntity> GetEntities()
    {
        var url = "http://43.131.48.203:8083/data/entity/list";
        

        using var client = new HttpClient();
        
        var response = client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<DefinedEntity>();
        }

        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DefinedEntity>>(responseBody);

        return result.ToList();
    }


    public void CreateEntity(DefinedEntity place)
    {
        throw new NotImplementedException();
    }

    public void UpdateEntity(DefinedEntity place)
    {
        throw new NotImplementedException();
    }
}