using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime.AssetManagement
{
    [CreateAssetMenu(menuName = "Catalog/Asset Catalog")]
    public sealed class AssetCatalog : ScriptableObject, IAssetCatalog
    {
        [SerializeField]
        private List<ImageSchema> _imageSchemas;

        [SerializeField]
        private List<SoundSchema> _soundSchemas;

        [SerializeField]
        private List<PrefabSchema> _prefabSchemas;

        private Dictionary<string, ImageSchema> _imageSchemasDict;

        private Dictionary<string, SoundSchema> _soundSchemasDict;

        private Dictionary<string, PrefabSchema> _prefabSchemasDict;

        public void Init()
        {
            InitImages();
            InitSounds();
            InitPrefabs();
        }

        private void InitImages()
        {
            if (_imageSchemasDict != null)
                return;

            _imageSchemasDict = new Dictionary<string, ImageSchema>();

            foreach (var imageSchema in _imageSchemas)
            {
                _imageSchemasDict.TryAdd(imageSchema.Id, imageSchema);
            }
        }

        private void InitSounds()
        {
            if (_soundSchemasDict != null)
                return;

            _soundSchemasDict = new Dictionary<string, SoundSchema>();

            foreach (var soundSchema in _soundSchemas)
            {
                _soundSchemasDict.TryAdd(soundSchema.Id, soundSchema);
            }
        }

        private void InitPrefabs()
        {
            if (_prefabSchemasDict != null)
                return;

            _prefabSchemasDict = new Dictionary<string, PrefabSchema>();

            foreach (var prefabSchema in _prefabSchemas)
            {
                _prefabSchemasDict.TryAdd(prefabSchema.Id, prefabSchema);
            }
        }

        public bool TryGetImage(string id, out ImageSchema schema) => _imageSchemasDict.TryGetValue(id, out schema);

        public bool TryGetSound(string id, out SoundSchema schema) => _soundSchemasDict.TryGetValue(id, out schema);

        public bool TryGetPrefab(string id, out PrefabSchema schema) => _prefabSchemasDict.TryGetValue(id, out schema);
    }
}