using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace APIWebApp.Models;

public partial class APIContext : DbContext
{
    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<PublishersBook> PublishersBooks { get; set; }

    public APIContext(DbContextOptions<APIContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}