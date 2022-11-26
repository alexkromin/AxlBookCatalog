using AutoMapper;
using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Business.Models.Book;
using AxlBookCatalog.DataAccess.Abstractions.Repositories;
using AxlBookCatalog.Domain;

namespace AxlBookCatalog.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IMapper mapper,
            IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public Task<IEnumerable<Book>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandOperationResponse<Book>> AddAsync(AddBookRequest addBookRequest)
        {
            var book = _mapper.Map<Book>(addBookRequest);
            var operatedObject = await _bookRepository.CreateAsync(book);

            return new CommandOperationResponse<Book>()
            {
                IsSuccess = true,
                OperatedObject = operatedObject
            };
        }

        public async Task<CommandOperationResponse<Book>> RemoveAsync(string bookId)
        {
            var operatedObject = await _bookRepository.RemoveAsync(bookId);
            if (operatedObject == null)
                return new CommandOperationResponse<Book>()
                {
                    IsSuccess = false,
                    Message = "The deleted book doesn't exist"
                };

            return new CommandOperationResponse<Book>()
            {
                IsSuccess = true,
                OperatedObject = operatedObject
            };
        }

        public async Task<CommandOperationResponse<Book>> EditAsync(EditBookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            var operatedObject = await _bookRepository.UpdateAsync(book);

            if (operatedObject == null)
                return new CommandOperationResponse<Book>()
                {
                    IsSuccess = false,
                    Message = "The edited book doesn't exist"
                };

            return new CommandOperationResponse<Book>()
            {
                IsSuccess = true,
                OperatedObject = operatedObject
            };
        }
    }
}
