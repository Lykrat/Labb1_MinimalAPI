using System.ComponentModel.DataAnnotations;

namespace Labb1_MinimalAPI
{
	public class Books
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Author { get; set; }
		public int Year { get; set; }
		public string Description { get; set; }
		public int InStock { get; set; }
	}
}
