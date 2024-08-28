using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    // Microsoft built-in memory cache managera alternatif olarak => MemCache, Redis
    public class MemoryCacheManager : ICacheManager
    {
        //ADAPTER PATTERN: Var olan bir sistemi kendi sistemimize uyarlamak.
        // Sen benim sistemime göre çalış diyoruz.
        // İleride memory cache sisteminde değişiklik yaparsak patlamamak için.

        // from Microsoft.Extensions.Caching.Memory
        IMemoryCache _memoryCache; // CoreModule.cs -> serviceCollection.AddMemoryCache(); 

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdded(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //out _  => out değerini döndürmek istemediğimizde.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        // Runtime'da bir nesnenin instance'ını bellekten silmek için reflection kullandık.
        public void RemoveByPattern(string pattern)
        {
            var fieldInfo =
                typeof(MemoryCache).GetField("_coherentState", BindingFlags.Instance | BindingFlags.NonPublic);
            var propertyInfo =
                fieldInfo.FieldType.GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            var value = fieldInfo.GetValue(_memoryCache);
            var dict = propertyInfo.GetValue(value) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in dict)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
