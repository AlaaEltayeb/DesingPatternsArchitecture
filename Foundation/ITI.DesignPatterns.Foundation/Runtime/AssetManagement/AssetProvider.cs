using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ITI.DesignPatterns.Foundation.Runtime.AssetManagement
{
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly IAssetCatalog _catalog;

        private readonly Dictionary<string, Sprite> _imagesCache = new();
        private readonly Dictionary<string, AudioClip> _soundsCache = new();
        private readonly Dictionary<string, GameObject> _prefabsCache = new();

        public AssetProvider(IAssetCatalog catalog)
        {
            _catalog = catalog;
            _catalog.Init();
        }

        public async Task<Sprite> GetImage(string id)
        {
            var found = _imagesCache.TryGetValue(id, out var image);

            if (found)
                return image;

            var result = await LoadImageAsync(id);

            return result;
        }

        private async Task<Sprite> LoadImageAsync(string id)
        {
            if (!_catalog.TryGetImage(id, out var schema))
                return null;

            var assetReference = schema.AssetReference;

            var handle = assetReference.LoadAssetAsync<Sprite>();

            var result = await handle.Task;

            _imagesCache.Add(id, result);
            Addressables.Release(handle);

            return result;
        }

        public async Task<AudioClip> GetSound(string id)
        {
            var found = _soundsCache.TryGetValue(id, out var audioClip);

            if (found)
                return audioClip;

            var result = await LoadSoundAsync(id);

            return result;
        }

        private async Task<AudioClip> LoadSoundAsync(string id)
        {
            if (!_catalog.TryGetSound(id, out var schema))
                return null;

            var assetReference = schema.AssetReference;

            var handle = assetReference.LoadAssetAsync<AudioClip>();

            var result = await handle.Task;

            _soundsCache.Add(id, result);
            Addressables.Release(handle);

            return result;
        }

        public async Task<GameObject> GetPrefab(string id)
        {
            var found = _prefabsCache.TryGetValue(id, out var prefab);

            if (found)
                return prefab;

            var result = await LoadPrefabAsync(id);

            return result;
        }

        private async Task<GameObject> LoadPrefabAsync(string id)
        {
            if (!_catalog.TryGetPrefab(id, out var schema))
                return null;

            var assetReference = schema.AssetReference;

            var handle = assetReference.LoadAssetAsync<GameObject>();

            var result = await handle.Task;

            _prefabsCache.Add(id, result);
            Addressables.Release(handle);

            return result;
        }
    }
}