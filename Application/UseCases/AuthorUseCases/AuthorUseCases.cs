using Application.DependencyInjectionExtensions;

namespace Application.UseCases.AuthorUseCases;

[Service]
public class AuthorUseCases(CreateAuthorUseCase createAuthorUseCase, DeleteAuthorUseCase deleteAuthorUseCase, 
    GetAllAuthorsPagesCountUseCase getAllAuthorsPagesCountUseCase, 
    GetAllAuthorsUseCase getAllAuthorsUseCase, 
    GetAuthorByIdUseCase getAuthorByIdUseCase, UpdateAuthorUseCase updateAuthorUseCase)
{
    public CreateAuthorUseCase CreateAuthorUseCase { get; set; } = createAuthorUseCase;
    public DeleteAuthorUseCase DeleteAuthorUseCase { get; set; } = deleteAuthorUseCase;
    public GetAllAuthorsPagesCountUseCase GetAllAuthorsPagesCountUseCase { get; set; } = getAllAuthorsPagesCountUseCase;
    public GetAllAuthorsUseCase GetAllAuthorsUseCase { get; set; } = getAllAuthorsUseCase;
    public GetAuthorByIdUseCase GetAuthorByIdUseCase { get; set; } = getAuthorByIdUseCase;
    public UpdateAuthorUseCase UpdateAuthorUseCase { get; set; } = updateAuthorUseCase;
}