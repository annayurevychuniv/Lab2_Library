using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIWebApp.Models;

public partial class Book
{
    public Book()
    {
        PublishersBooks = new List<PublishersBook>();
    }

    public int id { get; set; }

    public string title { get; set; }

    public int authorId { get; set; }

    public int genreId { get; set; }

    public int categoryId { get; set; }

    public string description { get; set; }

    public int publicationYear { get; set; }

    public virtual Author Author { get; set; }

    public virtual Category Category { get; set; }

    public virtual Genre Genre { get; set; }
    public virtual ICollection<PublishersBook> PublishersBooks { get; set; }
}
