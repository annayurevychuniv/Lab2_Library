using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIWebApp.Models;

public partial class Author
{
    public Author()
    {
        Books = new List<Book>();
    }

    public int id { get; set; }

    public string fullName { get; set; }

    public string country { get; set; }

    public int birthYear { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}