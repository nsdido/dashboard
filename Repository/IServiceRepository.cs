using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IServiceRepository {

    void Create(DefinedEntity place);
    void Update(DefinedEntity place);

    IEnumerable<ServiceModel> GetAll();
}