using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace APIWebApp.Models;

public partial class Publisher
{
    public Publisher()
    {
        PublishersAuthors = new List<PublishersBook>();
    }
    public int id { get; set; }

    public string name { get; set; }

    public virtual ICollection<PublishersBook> PublishersAuthors { get; set; }
}
