using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class TurretFactory : ITurretFactory
    {
        [Inject]
        private IGameManager _gameManager;

        [Inject]
        private IUIManager _uiManager;

        private readonly ITurretProvider _turretProvider;

        public Transform[] BuildSlots;

        public List<UglyTower> Towers = new();

        private int _selectedSlotIndex = -1;

        public TurretFactory(ITurretProvider turretProvider)
        {
            _turretProvider = turretProvider;
        }

        public void SelectSlot(int index)
        {
            _selectedSlotIndex = index;
            _uiManager.UpdateInGameMessage("Selected Slot: " + _selectedSlotIndex);
            Debug.Log("Selected Slot: " + _selectedSlotIndex);
        }

        public void BuildTower(TowerType id)
        {
            var turret = _turretProvider.GetTurretData(id);

            if (_selectedSlotIndex < 0 || _selectedSlotIndex >= BuildSlots.Length)
            {
                _uiManager.UpdateInGameMessage("Pick A Slot First");
                Debug.Log("Pick A Slot First");
                return;
            }

            var slot = BuildSlots[_selectedSlotIndex];
            if (slot.childCount > 0)
            {
                _uiManager.UpdateInGameMessage("Slot Already Occupied");
                Debug.Log("Slot Already Occupied");
                return;
            }

            var cost = turret.Cost;

            if (_gameManager.Gold < cost)
            {
                _uiManager.UpdateInGameMessage("Not Enough Gold.");
                Debug.Log("Not Enough Gold.");
                return;
            }

            _gameManager.UpdateGold(-cost);
            _uiManager.RefreshUI();

            var go = GameObject.Instantiate(
                turret.TurretPrefab,
                slot.position,
                Quaternion.identity,
                slot);

            var tower = go.AddComponent<UglyTower>();
            tower.TurretData = turret;

            Towers.Add(tower);
        }
    }

    public enum TowerType
    {
        Gunner,
        Cannon,
        Frost,
    }
}