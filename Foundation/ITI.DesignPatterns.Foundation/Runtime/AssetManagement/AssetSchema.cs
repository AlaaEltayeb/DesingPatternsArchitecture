using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ITI.DesignPatterns.Foundation.Runtime.AssetManagement
{
    public abstract class AssetSchema : ScriptableObject
    {
        [field: SerializeField]
        public string Id { get; private set; }

        [field: SerializeField]
        public AssetReference AssetReference { get; set; }
    }
}