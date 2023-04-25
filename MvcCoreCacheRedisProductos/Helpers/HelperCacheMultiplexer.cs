using StackExchange.Redis;

namespace MvcCoreCacheRedisProductos.Helpers
{
    public class HelperCacheMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> CreateConnection =
            new Lazy<ConnectionMultiplexer>(() =>
            {
                string cnn = "bbddproductosrediscma.redis.cache.windows.net:6380,password=2khG853wCyK8tHmY6hJxITrmL4xj3YBGeAzCaMeH4ik=,ssl=True,abortConnect=False";
                return ConnectionMultiplexer.Connect(cnn);
            });

        public static ConnectionMultiplexer GetConnection
        {
            get
            {
                return CreateConnection.Value;
            }
        }
    }
}
