using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IRuleRepository {

    void Save(DefinedEntity place);
    void Running(DefinedEntity place);

    IEnumerable<RuleModel> GetAll(int page, int size);
}