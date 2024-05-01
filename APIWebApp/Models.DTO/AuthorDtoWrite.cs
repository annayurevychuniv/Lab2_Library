using System.ComponentModel.DataAnnotations;

namespace APIWebApp.Models.DTO
{
    public class AuthorDtoWrite
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Ім'я")]
        public string fullName { get; set; }

        public string country { get; set; }

        public int birthYear { get; set; }

    }
}
