namespace CustomersManagment.Models
{
    public interface ICustomersManagmentDatabaseSettings
    {
        string CompanyCollectionName { get; set; }
        string ContactCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class CustomersManagmentDatabaseSettings : ICustomersManagmentDatabaseSettings
    {
        public string CompanyCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
