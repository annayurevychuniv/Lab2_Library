using Microsoft.EntityFrameworkCore;
using APIWebApp.Models;
using APIWebApp.Controllers;
using APIWebApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Library.Tests
{
    
    public class UnitTest1
    {
        private static APIContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("APILibrary")
                .Options;
            APIContext abc = new APIContext(options);
            return abc;
        }
        [Fact]
        public async Task CreateAuthor()
        {
            using var context = CreateContext();
            AuthorsController authorsController = new AuthorsController(context);

            AuthorDtoWrite author = new AuthorDtoWrite()
            {
                fullName = "fullName",
                country = "country",
                birthYear = 2000
            };
            var result = await authorsController.PostAuthor(author);

            Assert.True(result.Result is RedirectToActionResult);
        }
        [Fact]
        public async Task AuthorValidation()
        {
            using var context = CreateContext();
            AuthorsController authorsController = new AuthorsController(context);

            AuthorDtoWrite author = new AuthorDtoWrite();

            await Assert.ThrowsAnyAsync<Exception>(async () => await authorsController.PostAuthor(author));
        }
    }
}