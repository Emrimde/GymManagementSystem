
namespace GymManagementSystem.Core.DTO.Client;

public class ClientAgeValidationResponse
{
    public int Age { get; set; }

    public static implicit operator ClientAgeValidationResponse(HttpResponseMessage v)
    {
        throw new NotImplementedException();
    }
}
