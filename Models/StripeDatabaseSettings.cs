namespace ModustaAPI.Models
{
    public class StripeDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string StripeCollectionName { get; set; } = null!;
    }
}
