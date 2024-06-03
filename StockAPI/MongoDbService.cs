using MongoDB.Driver;

namespace StockAPI
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            MongoClient client = new(_configuration["MongoDB:Server"]);
            _database = client.GetDatabase(_configuration["MongoDB:DBName"]);
        }
        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
