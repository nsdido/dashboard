using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IHistoricalDataRepository {

    IEnumerable<HistoricalData> GetHistoricalData(string sensorName, long placeId);
}