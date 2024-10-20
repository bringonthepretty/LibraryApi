using Application.DependencyInjectionExtensions;
using Application.Exceptions;
using Application.Requests.Abstractions;
using Application.Requests.Implementations.AuthorRequests;
using Application.Requests.Implementations.BookRequests;
using Application.Requests.Implementations.UserRequests;

namespace Application.UseCases.Boundaries;

/*
 * You stated in your feedback that "use case should contain request data and interactor should process this data".
 * I'm not sure what does this mean, considering that author of the video thinks about interactor and use case as same thing.
 * As far as I understood, the only difference between his approach and mine is that he used boundary class to choose appropriate use case using polymorphism.
 * If I'm wrong, please correct me in your next feedback
 */

[Service]
public class Boundary(AuthorUseCases.AuthorUseCases authorUseCases, BookUseCases.BookUseCases bookUseCases, 
    UserUseCases.UserUseCases userUseCases)
{
    public async Task<object> InvokeAsync(IRequest request)
    {
        return request switch
        {
            CreateAuthorRequest authorRequest => await authorUseCases.CreateAuthorUseCase.InvokeAsync(authorRequest),
            DeleteAuthorRequest authorRequest => await authorUseCases.DeleteAuthorUseCase.InvokeAsync(authorRequest),
            GetAllAuthorsPagesCountRequest authorRequest => await authorUseCases.GetAllAuthorsPagesCountUseCase.InvokeAsync(authorRequest),
            GetAllAuthorsRequest authorRequest => await authorUseCases.GetAllAuthorsUseCase.InvokeAsync(authorRequest),
            GetAuthorByIdRequest authorRequest => await authorUseCases.GetAuthorByIdUseCase.InvokeAsync(authorRequest),
            UpdateAuthorRequest authorRequest => await authorUseCases.UpdateAuthorUseCase.InvokeAsync(authorRequest),
            
            BorrowBookRequest bookRequest => await bookUseCases.BorrowBookUseCase.InvokeAsync(bookRequest),
            CreateBookRequest bookRequest => await bookUseCases.CreateBookUseCase.InvokeAsync(bookRequest),
            DeleteBookRequest bookRequest => await bookUseCases.DeleteBookUseCase.InvokeAsync(bookRequest),
            GetAllBooksPageCountRequest bookRequest => await bookUseCases.GetAllBooksPagesCountUseCase.InvokeAsync(bookRequest),
            GetAllBooksRequest bookRequest => await bookUseCases.GetAllBooksUseCase.InvokeAsync(bookRequest),
            GetBookByIdRequest bookRequest => await bookUseCases.GetBookByIdUseCase.InvokeAsync(bookRequest),
            GetBookByIsbnRequest bookRequest => await bookUseCases.GetBookByIsbnUseCase.InvokeAsync(bookRequest),
            ReturnBookRequest bookRequest => await bookUseCases.ReturnBookUseCase.InvokeAsync(bookRequest),
            UpdateBookRequest bookRequest => await bookUseCases.UpdateBookUseCase.InvokeAsync(bookRequest),
            
            LoginUserRequest userRequest => await userUseCases.LoginUserUseCase.InvokeAsync(userRequest),
            RegenerateUserAccessAndRefreshTokensRequest userRequest => await userUseCases.RegenerateUserAccessAndRefreshTokensUseCase.InvokeAsync(userRequest),
            RegisterUserRequest userRequest => await userUseCases.RegisterUserUseCase.InvokeAsync(userRequest),
            
            _ => throw new LibraryApplicationException(ExceptionCode.ImpossibleData)
        };
    }
}