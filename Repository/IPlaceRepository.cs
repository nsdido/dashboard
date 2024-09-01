using Climate_Watch.Models;

namespace Climate_Watch.Repository;

public interface IPlaceRepository {

    void CreatePlace(DefineHome place);
    void UpdatePlace(DefineHome place);

    IEnumerable<DefinedHome> GetPlaces(string telegramId);

    IEnumerable<string> GetUsersInPlace(long placeId);

    void AddUserToPlace(AddUserToPlace place);
}