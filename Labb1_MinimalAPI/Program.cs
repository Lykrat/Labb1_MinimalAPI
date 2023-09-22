using Labb1_MinimalAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb1_MinimalAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<DataContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapPost("/saveBook", async (Books book, DataContext db) =>
			{
				db.Books.Add(book);
				await db.SaveChangesAsync();
				return Results.Created($"save/{book.Id}", book);
			});

			app.MapGet("/GetAllBooks",async(DataContext db)=>await db.Books.ToListAsync());

			app.MapGet("/GetBookById/{id:int}", async (int id, DataContext db) => await db.Books.FindAsync(id) is Books book ? Results.Ok(book) : Results.NotFound());

			app.MapPut("/UpdateBook/{id:int}", async (int id, Books bookInput, DataContext db) =>
			{
				var book = await db.Books.FindAsync(id);
				if (book == null)
				{
					return Results.NotFound();
				}
				book.Title = bookInput.Title;
				book.Author = bookInput.Author;
				book.Year = bookInput.Year;
				book.Description = bookInput.Description;
				book.InStock = bookInput.InStock;

				await db.SaveChangesAsync();
				return Results.Ok(book);
			});

			app.MapDelete("/DeleteBook/{id:int}", async (int id, DataContext db) =>
			{
				if (await db.Books.FindAsync(id) is Books book)
				{
					db.Books.Remove(book);
					await db.SaveChangesAsync();
					return Results.Ok(book);
				}
				return Results.NotFound();
			});

			app.Run();
		}
	}
}