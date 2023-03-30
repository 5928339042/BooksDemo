namespace BooksDemo.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
