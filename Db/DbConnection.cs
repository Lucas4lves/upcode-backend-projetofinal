using MongoDB.Driver;

namespace PesquisaMongoAPI.Db
{
    public class DbConnection
    {
        private static MongoClient _client;
        private static readonly object _lock = new object();

        public static MongoClient Client
        {
            get
            {
                lock (_lock)
                {
                    if (_client == null)
                    {
                        var connectionString = "mongodb+srv://user:tcRDMKuCYwt4uHOb@cluster0.vsukxef.mongodb.net/?authSource=admin";
                        _client = new MongoClient(connectionString);
                    }
                }
                return _client;
            }
        }
    }
}
