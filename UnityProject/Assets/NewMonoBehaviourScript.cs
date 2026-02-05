using ITI.DesignPatterns.CustomPackage.Runtime;
using ITI.DesignPatterns.Foundation.Runtime.AssetManagement;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [Inject]
    private IAssetProvider _assetProvider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _ = GetSpriteFromAddressables();
    }

    private async Task GetSpriteFromAddressables()
    {
        var sprite = await _assetProvider.GetImage(ImageIdsEnum.Bubble.ToString());

        _image.sprite = sprite;
    }
}