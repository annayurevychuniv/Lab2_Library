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

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Назва")]
    public string name { get; set; }

    public virtual ICollection<PublishersBook> PublishersAuthors { get; set; }
}
