using Labb1_MinimalAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb1_MinimalAPI.Services
{
	public class BooksRepo : IBooksRepo
	{
		private DataContext _dbContext;

		public BooksRepo(DataContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Books> CreateBookAsync(Books book)
		{
			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync();
			return book;
		}

		public async Task<Books> DeleteBookAsync(int id)
		{
			var book = await _dbContext.Books.FirstOrDefaultAsync();
			_dbContext.Remove(book);
			await _dbContext.SaveChangesAsync();
			return book;
		}

		public async Task<IEnumerable<Books>> GetAllAsync()
		{
			return await _dbContext.Books.ToListAsync();
		}

		public async Task<Books> GetBookAsync(int id)
		{
			return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<Books> UpdateBook(Books book, int id)
		{
			var oldBook=await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
			if(oldBook != null)
			{
				oldBook.Title = book.Title;
				oldBook.Author = book.Author;
				oldBook.Year = book.Year;
				oldBook.Description = book.Description;
				oldBook.InStock = book.InStock;
				await _dbContext.SaveChangesAsync();
				return oldBook;
			}
			return null;
		}
	}
}
