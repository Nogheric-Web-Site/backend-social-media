using MongoDB.Driver;

namespace MongoDBAPI.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://nogheric:Nogheric100@nogheric.gbbwp.mongodb.net/?retryWrites=true&w=majority");

            db = client.GetDatabase("Nogheric-Dev");
        }
    }
}
