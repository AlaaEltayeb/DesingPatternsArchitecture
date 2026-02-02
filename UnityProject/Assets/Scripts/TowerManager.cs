using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class TowerManager : MonoBehaviour, ITowerManager
{
    [Inject]
    private IGameManager _gameManager;

    [Inject]
    private IUIManager _uiManager;

    public static TowerManager Instance { get; private set; }

    public Transform TowerParent;

    public List<TowerPrefab> TowersPrefabs;

    public Transform[] BuildSlots;

    public List<UglyTower> Towers = new();

    private int _selectedSlotIndex = -1;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    private void Update()
    {
        for (var i = 0; i < Towers.Count; i++)
        {
            var t = Towers[i];
            if (t != null)
                t.UglyTick();
        }
    }

    public void SelectSlot(int index)
    {
        _selectedSlotIndex = index;
        _uiManager.UpdateInGameMessage("Selected Slot: " + _selectedSlotIndex);
        Debug.Log("Selected Slot: " + _selectedSlotIndex);
    }

    public void BuildTower(TowerType id)
    {
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

        var tower = TowersPrefabs.FirstOrDefault(towerPrefab => towerPrefab.Type == id)?.Prefab;
        var cost = tower.Cost;

        if (_gameManager.Gold < cost)
        {
            _uiManager.UpdateInGameMessage("Not Enough Gold.");
            Debug.Log("Not Enough Gold.");
            return;
        }

        _gameManager.UpdateGold(-cost);
        _uiManager.RefreshUI();

        var go = Instantiate(
            tower,
            slot.position,
            Quaternion.identity,
            slot);

        Towers.Add(go);
    }
}

public enum TowerType
{
    Gunner,
    Cannon,
    Frost,
}

[Serializable]
public class TowerPrefab
{
    [field: SerializeField]
    public TowerType Type { get; set; }

    [field: SerializeField]
    public UglyTower Prefab { get; set; }
}