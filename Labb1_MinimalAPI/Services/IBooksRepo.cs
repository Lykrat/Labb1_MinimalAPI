namespace Labb1_MinimalAPI.Services
{
	public interface IBooksRepo
	{
		Task<IEnumerable<Books>> GetAllAsync();
		Task<Books>GetBookAsync(int id);
		Task<Books> CreateBookAsync(Books book);
		Task<Books> UpdateBook(Books book,int id);
		Task<Books> DeleteBookAsync(int id);
	}
}
