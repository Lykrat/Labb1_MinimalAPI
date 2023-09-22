using Microsoft.EntityFrameworkCore;

namespace Labb1_MinimalAPI.Data
{
	public class DataContext:DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Books> Books { get; set; }
	}
}
