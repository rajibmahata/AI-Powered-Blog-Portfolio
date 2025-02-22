using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace AIPoweredBlogPortfolio.Admin.Services
{
    public class LocalStorageHelper
    {
        private readonly ILocalStorageService _localStorageService;

        public LocalStorageHelper(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task SetItemAsync<T>(string key, T value)
        {
            await _localStorageService.SetItemAsync(key, value);
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            return await _localStorageService.GetItemAsync<T>(key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _localStorageService.RemoveItemAsync(key);
        }

        public async Task SetItemAsyncWithExpiry<T>(string key, TimeSpan expiry, T data)
        {
            StorageItem<T> storageItem = new StorageItem<T>
            {
                Data = data,
                Expiry = DateTime.UtcNow.Add(expiry)
            };

            await _localStorageService.SetItemAsync(key, storageItem);
        }

        public async Task<T?> GetItemAsyncWithExpiry<T>(string key)
        {
            var storageItem = await _localStorageService.GetItemAsync<StorageItem<T>>(key);

            if (storageItem is null)
            {
                return default;
            }

            if (storageItem.Expiry < DateTime.UtcNow)
            {
                await _localStorageService.RemoveItemAsync(key);
                return default;
            }
            return storageItem.Data;
        }
    }

    public class StorageItem<T>
    {
        public T Data { get; set; }
        public DateTime Expiry { get; set; }
    }
}
