namespace ModustaAPI.Models
{
    public class ModustaDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string BooksCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}
