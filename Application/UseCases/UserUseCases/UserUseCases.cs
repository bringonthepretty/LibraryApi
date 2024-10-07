using Application.DependencyInjectionExtensions;

namespace Application.UseCases.UserUseCases;

[Service]
public class UserUseCases(LoginUserUseCase loginUserUseCase, RegisterUserUseCase registerUserUseCase,
    RegenerateUserAccessAndRefreshTokensUseCase regenerateUserAccessAndRefreshTokensUseCase)
{
    public LoginUserUseCase LoginUserUseCase { get; set; } = loginUserUseCase;
    public RegenerateUserAccessAndRefreshTokensUseCase RegenerateUserAccessAndRefreshTokensUseCase { get; set; } = regenerateUserAccessAndRefreshTokensUseCase;
    public RegisterUserUseCase RegisterUserUseCase { get; set; } = registerUserUseCase;
}