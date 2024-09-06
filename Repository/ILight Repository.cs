using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface ILightRepository {

    void Create(DefinedEntity place);
    void Update(DefinedEntity place);

    IEnumerable<LightModel> GetAll(DateTime start_at);
}