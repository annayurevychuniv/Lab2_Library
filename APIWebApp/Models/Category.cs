﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIWebApp.Models;

public partial class Category
{
    public Category()
    {
        Books = new List<Book>();
    }
    public int id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва")]
    public string name { get; set; }

    public string? description { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
