using System.Net;
using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public class RuleRepository : IRuleRepository {
   
    public IEnumerable<RuleModel> GetAll(int page, int size)
    {
        var url = "http://43.131.48.203:8083/rule/list?page=" +page + "&size=" +size;


        using var client = new HttpClient();

        var response = client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<RuleModel>();
        }

        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<DefinedRule>(responseBody);

        return result.List.ToList();
    }

    public void Save(DefinedEntity place)
    {
        throw new NotImplementedException();
    }

    public void Running(DefinedEntity place)
    {
        throw new NotImplementedException();
    }

   
}