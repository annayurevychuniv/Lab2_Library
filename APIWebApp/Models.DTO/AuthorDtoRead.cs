namespace APIWebApp.Models.DTO
{
    public class AuthorDtoRead
    {
        public int id { get; set; }
        public string fullName { get; set; }

        public string country { get; set; }

        public int birthYear { get; set; }
    }
}
