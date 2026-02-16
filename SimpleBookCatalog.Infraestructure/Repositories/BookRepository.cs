using Microsoft.EntityFrameworkCore;
using SimpleBookCatalog.Application.Interfaces;
using SimpleBookCatalog.Domain.Entities;
using SimpleBookCatalog.Infraestructure.Context;

namespace SimpleBookCatalog.Infraestructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly SimpleBookCatalogDbContext _context;

        // Normally, we can inject the context class directly into the constructor,
        // but since our presentation layer will be a Blazor Server application,
        // we need to use IDbContextFactory to create instances of the DbContext,
        // because Blazor web app with support for interactive server rendering (i.e. support for Blazor Server)
        // we need to inject IDBContextFactory, because Blazor Server has a different lifecycle than traditional web applications,
        // and it doesn't support scoped services in the same way.
        // By using IDbContextFactory, we can create a new instance of the DbContext for each operation, which is useful in scenarios like Blazor Server
        // or when using dependency injection in a way that doesn't allow for scoped services.
        // We have to register the IDbContextFactory in the dependency injection container, and then we can use it to create a new instance of the DbContext whenever we need
        // to perform database operations.
        public BookRepository(IDbContextFactory<SimpleBookCatalogDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }
    }
}
