using System;
using System.Collections.Generic;

namespace APIWebApp.Models;

public partial class PublishersBook
{
    public int id { get; set; }

    public int publisherId { get; set; }

    public int bookId { get; set; }

    public virtual Book Book { get; set; }

    public virtual Publisher Publisher { get; set; }
}
