using UnityEngine;
using UnityEngine.EventSystems;

public class UglySlotClick : MonoBehaviour, IPointerClickHandler
{
    public int Index;

    public void OnPointerClick(PointerEventData eventData)
    {
        UglyDTGameManager.Instance.SelectSlot(Index);
    }
}