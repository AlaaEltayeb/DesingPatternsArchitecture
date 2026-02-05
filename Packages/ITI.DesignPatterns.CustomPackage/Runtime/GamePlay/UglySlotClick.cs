using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class UglySlotClick : MonoBehaviour, IPointerClickHandler
    {
        [Inject]
        private ITurretFactory _turretFactory;
        public int Index;

        public void OnPointerClick(PointerEventData eventData)
        {
            _turretFactory.SelectSlot(Index);
        }
    }
}