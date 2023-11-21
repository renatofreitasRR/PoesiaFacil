using MongoDB.Driver;

namespace PoesiaFacil.Data.Context
{
    public class DataContext
    {
        protected readonly IMongoDatabase _mongoDatabase;
        protected readonly string _collectionName;

        public DataContext(string collection)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = client.GetDatabase("PoesiaFacil"); ;
            _collectionName = collection;
        }
    }
}
