using Labb1_MinimalAPI.Data;
using Labb1_MinimalAPI.Services;
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
			builder.Services.AddScoped<IBooksRepo, BooksRepo>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapPost("/saveBook", async (IBooksRepo bookRepo, Books book) =>
			{
				var result = await bookRepo.CreateBookAsync(book);
				return Results.Ok(result);
			});

			app.MapGet("/GetAllBooks",async (IBooksRepo bookRepo) =>
			{
				var result=await bookRepo.GetAllAsync();
				return Results.Ok(result);
			});

			app.MapGet("/GetBookById/{id:int}", async (IBooksRepo bookRepo,int id) =>
			{
				var result = await bookRepo.GetBookAsync(id);
				if(result==null)
				{
					return Results.BadRequest(result);
				}
				return Results.Ok(result);
			});

			app.MapPut("/UpdateBook/{id:int}", async (IBooksRepo bookRepo,Books book,int id) =>
			{
				var result = await bookRepo.UpdateBook(book,id);
				if (result == null)
				{
					return Results.BadRequest(result);
				}
				return Results.Ok(result);
			});

			app.MapDelete("/DeleteBook/{id:int}", async (IBooksRepo bookRepo, int id) =>
			{
				var result=await bookRepo.DeleteBookAsync(id);
				if (result == null)
				{
					return Results.BadRequest(result);
				}
				return Results.Ok(result);
			});

			app.Run();
		}
	}
}