using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface ITemperatureRepository {

    void Create(DefinedEntity place);
    void Update(DefinedEntity place);

    IEnumerable<TemperatureModel> GetAll(DateTime start_at);
}