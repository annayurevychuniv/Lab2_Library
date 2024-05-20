using Microsoft.EntityFrameworkCore;
using APIWebApp.Models;
using APIWebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Library.Tests
{
    
    public class UnitTest1
    {
        private static APIContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<APIContext>()
                .UseInMemoryDatabase("APILibrary")
                .Options;
            return new APIContext(options);
        }

        [Fact]
        public async Task GetAuthors()
        {
            using var context = CreateContext();

            context.Authors.Add(new Author { fullName = "Author1", country = "Country1", birthYear = 1 });
            context.Authors.Add(new Author { fullName = "Author2", country = "Country2", birthYear = 2 });
            context.Authors.Add(new Author { fullName = "Author3", country = "Country3", birthYear = 3 });

            await context.SaveChangesAsync();

            var controller = new AuthorsController(context);
            var result = await controller.GetAuthors();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Author>>>(result);
            var authors = Assert.IsType<List<Author>>(actionResult.Value);

            Assert.Equal(3, authors.Count);
        }

        [Fact]
        public async Task GetAuthor()
        {
            using var context = CreateContext();

            var author = new Author 
            { 
                fullName = "Author", 
                country = "Country", 
                birthYear = 2024 
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var controller = new AuthorsController(context);
            var result = await controller.GetAuthor(author.id);

            var actionResult = Assert.IsType<ActionResult<Author>>(result);
            var returnedAuthor = Assert.IsType<Author>(actionResult.Value);

            Assert.Equal(author.id, returnedAuthor.id);
            Assert.Equal(author.fullName, returnedAuthor.fullName);
        }

        [Fact]
        public async Task GetAuthor_Error()
        {
            using var context = CreateContext();

            var controller = new AuthorsController(context);
            var result = await controller.GetAuthor(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostAuthor()
        {
            using var context = CreateContext();
            var controller = new AuthorsController(context);

            var author = new Author 
            { 
                fullName = "Author", 
                country = "Country", 
                birthYear = 2024 
            };

            var result = await controller.PostAuthor(author);

            var actionResult = Assert.IsType<ActionResult<Author>>(result);
            var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdAuthor = Assert.IsType<Author>(createdResult.Value);

            Assert.Equal(author.fullName, createdAuthor.fullName);
        }

        [Fact]
        public async Task PutAuthor()
        {
            using var context = CreateContext();

            var author = new Author 
            {
                fullName = "Author", 
                country = "Country", 
                birthYear = 2024 
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var controller = new AuthorsController(context);
            author.fullName = "Updated author";
            var result = await controller.PutAuthor(author.id, author);

            Assert.IsType<NoContentResult>(result);

            var updatedAuthor = await context.Authors.FindAsync(author.id);
            Assert.Equal("Updated author", updatedAuthor.fullName);
        }

        [Fact]
        public async Task PutAuthor_Error()
        {
            using var context = CreateContext();

            var author = new Author {fullName = "Author", country = "Country", birthYear = 2024 };
            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var controller = new AuthorsController(context);
            author.fullName = "Updated author";
            var result = await controller.PutAuthor(author.id + 1, author);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAuthor()
        {
            using var context = CreateContext();

            var author = new Author 
            { 
                fullName = "Author", 
                country = "Country", 
                birthYear = 2024 
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();

            var controller = new AuthorsController(context);
            var result = await controller.DeleteAuthor(author.id);

            Assert.IsType<NoContentResult>(result);

            var deletedAuthor = await context.Authors.FindAsync(author.id);
            Assert.Null(deletedAuthor);
        }

        [Fact]
        public async Task DeleteAuthor_Error()
        {
            using var context = CreateContext();

            var controller = new AuthorsController(context);
            var result = await controller.DeleteAuthor(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}