namespace ITI.DesignPatterns.Foundation.Runtime.AssetManagement
{
    public interface IAssetCatalog
    {
        void Init();
        bool TryGetImage(string id, out ImageSchema schema);

        bool TryGetSound(string id, out SoundSchema schema);

        bool TryGetPrefab(string id, out PrefabSchema schema);
    }
}