using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IHumidityRepository {

    void Create(DefinedEntity place);
    void Update(DefinedEntity place);

    IEnumerable<HumidityModel> GetAll(DateTime start_at);
}