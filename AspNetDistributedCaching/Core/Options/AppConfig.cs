namespace AspNetDistributedCaching.Core.Options
{
    public class AppConfig
    {
        public MongoOptions Mongo { get; set; }
        public RedisConfig Redis { get; set; }

    }
}
