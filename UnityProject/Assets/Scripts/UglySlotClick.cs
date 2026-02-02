using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class UglySlotClick : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    private ITowerManager _towerManager;
    public int Index;

    public void OnPointerClick(PointerEventData eventData)
    {
        _towerManager.SelectSlot(Index);
    }
}