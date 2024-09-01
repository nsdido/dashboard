using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IEntityRepository {

    void CreateEntity(DefinedEntity place);
    void UpdateEntity(DefinedEntity place);

    IEnumerable<DefinedEntity> GetEntities();
}