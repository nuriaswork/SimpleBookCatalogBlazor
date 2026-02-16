using SimpleBookCatalog.Domain.Entities;

namespace SimpleBookCatalog.Application.Interfaces
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);

        Task<List<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);

        Task UpdateAsync(Book book);
    }
}
