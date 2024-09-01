using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IDeviceRepository {

    void Create(DefinedEntity place);
    void Update(DefinedEntity place);

    IEnumerable<DeviceModel> GetAll();
}