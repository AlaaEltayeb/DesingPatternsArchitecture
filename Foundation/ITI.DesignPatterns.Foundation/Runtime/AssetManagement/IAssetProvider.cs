using System.Threading.Tasks;
using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime.AssetManagement
{
    public interface IAssetProvider
    {
        Task<Sprite> GetImage(string id);

        Task<AudioClip> GetSound(string id);

        Task<GameObject> GetPrefab(string id);
    }
}