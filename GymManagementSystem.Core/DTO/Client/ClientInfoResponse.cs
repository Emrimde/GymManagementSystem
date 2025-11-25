using Microsoft.AspNetCore.Http.Authentication.Internal;

namespace GymManagementSystem.Core.DTO.Client;
public class ClientInfoResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;

    public override string ToString()
    {
        return FullName;
    }
}
