namespace web_app_repository
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = "mongodb://energia:1234@localhost:27017/?authSource=admin&authMechanism=SCRAM-SHA-256";  // Ajuste para incluir authSource
        public string DatabaseName { get; set; } = "EnergyMonitorDB";
    }
}
