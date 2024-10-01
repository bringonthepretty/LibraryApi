namespace Application.UseCases.BookUseCases;

public class BookUseCases(CreateBookUseCase createBookUseCase, BorrowBookUseCase borrowBookUseCase, DeleteBookUseCase deleteBookUseCase, DeleteBookByAuthorIdUseCase deleteBookByAuthorIdUseCase, GetAllBooksByAuthorWithPageAndLimitUseCase getAllBooksByAuthorWithPageAndLimitUseCase, GetAllBooksByGenreWithPageAndLimitUseCase getAllBooksByGenreWithPageAndLimitUseCase, GetAllBooksByNameWithPageAndLimitUseCase getAllBooksByNameWithPageAndLimitUseCase, GetAllBooksByUserWithPageAndLimitUseCase getAllBooksByUserWithPageAndLimitUseCase, GetAllBooksCountUseCase getAllBooksCountUseCase, GetAllBooksPagesCountUseCase getAllBooksPagesCountUseCase, GetAllBooksWithAuthorIdCountUseCase getAllBooksWithAuthorIdCountUseCase, GetAllBooksWithAuthorIdPagesCountUseCase getAllBooksWithAuthorIdPagesCountUseCase, GetAllBooksWithGenreCountUseCase getAllBooksWithGenreCountUseCase, GetAllBooksWithGenrePagesCountUseCase getAllBooksWithGenrePagesCountUseCase, GetAllBooksWithNameCountUseCase getAllBooksWithNameCountUseCase, GetAllBooksWithNamePagesCountUseCase getAllBooksWithNamePagesCountUseCase, GetAllBooksWithPageAndLimitUseCase getAllBooksWithPageAndLimitUseCase, GetAllBooksWithUserIdCountUseCase getAllBooksWithUserIdCountUseCase, GetAllBooksWithUserIdPagesCountUseCase getAllBooksWithUserIdPagesCountUseCase, GetBookByIdUseCase getBookByIdUseCase, GetBookByISBNUseCase getBookByIsbnUseCase, ReturnBookUseCase returnBookUseCase, UpdateBookUseCase updateBookUseCase)
{
    public CreateBookUseCase CreateBookUseCase { get; set; } = createBookUseCase;
    public BorrowBookUseCase BorrowBookUseCase { get; set; } = borrowBookUseCase;
    public DeleteBookUseCase DeleteBookUseCase { get; set; } = deleteBookUseCase;
    public DeleteBookByAuthorIdUseCase DeleteBookByAuthorIdUseCase { get; set; } = deleteBookByAuthorIdUseCase;
    public GetAllBooksByAuthorWithPageAndLimitUseCase GetAllBooksByAuthorWithPageAndLimitUseCase { get; set; } = getAllBooksByAuthorWithPageAndLimitUseCase;
    public GetAllBooksByGenreWithPageAndLimitUseCase GetAllBooksByGenreWithPageAndLimitUseCase { get; set; } = getAllBooksByGenreWithPageAndLimitUseCase;
    public GetAllBooksByNameWithPageAndLimitUseCase GetAllBooksByNameWithPageAndLimitUseCase { get; set; } = getAllBooksByNameWithPageAndLimitUseCase;
    public GetAllBooksByUserWithPageAndLimitUseCase GetAllBooksByUserWithPageAndLimitUseCase { get; set; } = getAllBooksByUserWithPageAndLimitUseCase;
    public GetAllBooksCountUseCase GetAllBooksCountUseCase { get; set; } = getAllBooksCountUseCase;
    public GetAllBooksPagesCountUseCase GetAllBooksPagesCountUseCase { get; set; } = getAllBooksPagesCountUseCase;
    public GetAllBooksWithAuthorIdCountUseCase GetAllBooksWithAuthorIdCountUseCase { get; set; } = getAllBooksWithAuthorIdCountUseCase;
    public GetAllBooksWithAuthorIdPagesCountUseCase GetAllBooksWithAuthorIdPagesCountUseCase { get; set; } = getAllBooksWithAuthorIdPagesCountUseCase;
    public GetAllBooksWithGenreCountUseCase GetAllBooksWithGenreCountUseCase { get; set; } = getAllBooksWithGenreCountUseCase;
    public GetAllBooksWithGenrePagesCountUseCase GetAllBooksWithGenrePagesCountUseCase { get; set; } = getAllBooksWithGenrePagesCountUseCase;
    public GetAllBooksWithNameCountUseCase GetAllBooksWithNameCountUseCase { get; set; } = getAllBooksWithNameCountUseCase;
    public GetAllBooksWithNamePagesCountUseCase GetAllBooksWithNamePagesCountUseCase { get; set; } = getAllBooksWithNamePagesCountUseCase;
    public GetAllBooksWithPageAndLimitUseCase GetAllBooksWithPageAndLimitUseCase { get; set; } = getAllBooksWithPageAndLimitUseCase;
    public GetAllBooksWithUserIdCountUseCase GetAllBooksWithUserIdCountUseCase { get; set; } = getAllBooksWithUserIdCountUseCase;
    public GetAllBooksWithUserIdPagesCountUseCase GetAllBooksWithUserIdPagesCountUseCase { get; set; } = getAllBooksWithUserIdPagesCountUseCase;
    public GetBookByIdUseCase GetBookByIdUseCase { get; set; } = getBookByIdUseCase;
    public GetBookByISBNUseCase GetBookByIsbnUseCase { get; set; } = getBookByIsbnUseCase;
    public ReturnBookUseCase ReturnBookUseCase { get; set; } = returnBookUseCase;
    public UpdateBookUseCase UpdateBookUseCase { get; set; } = updateBookUseCase;
}
