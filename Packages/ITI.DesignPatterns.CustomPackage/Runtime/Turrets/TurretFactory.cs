using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.CustomPackage.Runtime.Strategy;
using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Turrets
{
    public class TurretFactory : ITurretFactory
    {
        private readonly GameDataModel _gameDataModel;
        private readonly IAttackStrategyProvider _attackStrategyProvider;

        private readonly ITurretProvider _turretProvider;

        public Transform[] BuildSlots;

        public List<UglyTower> Towers = new();

        private int _selectedSlotIndex = -1;

        public TurretFactory(
            ITurretProvider turretProvider,
            GameDataModel gameDataModel,
            IAttackStrategyProvider attackStrategyProvider)
        {
            _turretProvider = turretProvider;
            _gameDataModel = gameDataModel;
            _attackStrategyProvider = attackStrategyProvider;
        }

        public void SelectSlot(int index)
        {
            _selectedSlotIndex = index;
            _gameDataModel.UpdateMessage("Selected Slot: " + _selectedSlotIndex);
            Debug.Log("Selected Slot: " + _selectedSlotIndex);
        }

        public void BuildTower(TowerType id)
        {
            var turret = _turretProvider.GetTurretData(id);

            //if (_selectedSlotIndex < 0 || _selectedSlotIndex >= BuildSlots.Length)
            //{
            //    _gameDataModel.UpdateMessage("Pick A Slot First");
            //    Debug.Log("Pick A Slot First");
            //    return;
            //}

            //var slot = BuildSlots[_selectedSlotIndex];
            //if (slot.childCount > 0)
            //{
            //    _gameDataModel.UpdateMessage("Slot Already Occupied");
            //    Debug.Log("Slot Already Occupied");
            //    return;
            //}

            var cost = turret.Cost;

            if (_gameDataModel.Gold.Value < cost)
            {
                _gameDataModel.UpdateMessage("Not Enough Gold.");
                Debug.Log("Not Enough Gold.");
                return;
            }

            _gameDataModel.UpdateGold(-cost);
            _gameDataModel.UpdateMessage("GoldUpdated.");

            var go = GameObject.Instantiate(turret.TurretPrefab);

            var tower = go.GetComponent<UglyTower>();
            tower.TurretData = turret;

            var strategy = _attackStrategyProvider.GetStrategy(id);
            tower.AttackStrategy = strategy;

            Towers.Add(tower);
        }
    }
}