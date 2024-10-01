namespace Application.UseCases.UserUseCases;

public class UserUseCases(GetUserByIdUseCase getUserByIdUseCase, GetUserByLoginUseCase getUserByLoginUseCase, GetUsersBooksPagesCountUseCase getUsersBooksPagesCountUseCase, GetUsersBooksWithPageAndLimitUseCase getUsersBooksWithPageAndLimitUseCase, LoginUserUseCase loginUserUseCase, RegenerateUserAccessAndRefreshTokensUseCase regenerateUserAccessAndRefreshTokensUseCase, RegisterUserUseCase registerUserUseCase)
{
    public GetUserByIdUseCase GetUserByIdUseCase { get; set; } = getUserByIdUseCase;
    public GetUserByLoginUseCase GetUserByLoginUseCase { get; set; } = getUserByLoginUseCase;
    public GetUsersBooksPagesCountUseCase GetUsersBooksPagesCountUseCase { get; set; } = getUsersBooksPagesCountUseCase;
    public GetUsersBooksWithPageAndLimitUseCase GetUsersBooksWithPageAndLimitUseCase { get; set; } = getUsersBooksWithPageAndLimitUseCase;
    public LoginUserUseCase LoginUserUseCase { get; set; } = loginUserUseCase;
    public RegenerateUserAccessAndRefreshTokensUseCase RegenerateUserAccessAndRefreshTokensUseCase { get; set; } = regenerateUserAccessAndRefreshTokensUseCase;
    public RegisterUserUseCase RegisterUserUseCase { get; set; } = registerUserUseCase;
}