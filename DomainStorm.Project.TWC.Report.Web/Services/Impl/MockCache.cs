using DomainStorm.Framework.Caching;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl
{
    public class MockCache : ICache
    {
        private static Dictionary<string, object> Dictionary = new();

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult((T)Dictionary[key]);
        }

        public Task SaveAsync<T>(string key, T value)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary.Remove(key);

            Dictionary.Add(key, value);

            return Task.CompletedTask;
        }

        public Task SaveAsync<T>(string key, T value, int ttlInSeconds)
        {
            if (Dictionary.ContainsKey(key))
                Dictionary.Remove(key);

            Dictionary.Add(key, value);

            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(string key, T value)
        {
            Dictionary.Remove(key);
            return Task.CompletedTask;
        }
    }
}
