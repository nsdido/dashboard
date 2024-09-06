using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IServiceRepository {
    
    IEnumerable<ServiceModel> GetAll();
}