using Application.DependencyInjectionExtensions;

namespace Application.UseCases.BookUseCases;

[Service]
public class BookUseCases(CreateBookUseCase createBookUseCase, BorrowBookUseCase borrowBookUseCase, 
    DeleteBookUseCase deleteBookUseCase, GetAllBooksPagesCountUseCase getAllBooksPagesCountUseCase,
    GetBookByIdUseCase getBookByIdUseCase, GetBookByIsbnUseCase getBookByIsbnUseCase,
    ReturnBookUseCase returnBookUseCase, UpdateBookUseCase updateBookUseCase, GetAllBooksUseCase getAllBooksUseCase)
{
    public CreateBookUseCase CreateBookUseCase { get; set; } = createBookUseCase;
    public BorrowBookUseCase BorrowBookUseCase { get; set; } = borrowBookUseCase;
    public DeleteBookUseCase DeleteBookUseCase { get; set; } = deleteBookUseCase;
    public GetAllBooksUseCase GetAllBooksUseCase { get; set; } = getAllBooksUseCase;
    public GetAllBooksPagesCountUseCase GetAllBooksPagesCountUseCase { get; set; } = getAllBooksPagesCountUseCase;
    public GetBookByIdUseCase GetBookByIdUseCase { get; set; } = getBookByIdUseCase;
    public GetBookByIsbnUseCase GetBookByIsbnUseCase { get; set; } = getBookByIsbnUseCase;
    public ReturnBookUseCase ReturnBookUseCase { get; set; } = returnBookUseCase;
    public UpdateBookUseCase UpdateBookUseCase { get; set; } = updateBookUseCase;
}
