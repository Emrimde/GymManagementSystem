using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Desktop.ViewModel;

namespace GymManagementSystem.Desktop.Mappers;

public static class AuthMapper
{
    public static SignInDto ToSignInDto(string username, string password)
    {
        return new SignInDto
        {
            Username = username,
            Password = password
        };
    }
    public static RegisterDto ToRegisterDto(RegisterViewModel viewModel)
    {
        return new RegisterDto
        {
            Email = viewModel.Email,
            Username = viewModel.Username,
            Password = viewModel.Password,
            ConfirmPassword = viewModel.ConfirmPassword
        };
    }
}
