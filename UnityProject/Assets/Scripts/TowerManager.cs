using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    public Transform TowerParent;

    public GameObject TowerGunnerPrefab;
    public GameObject TowerCannonPrefab;
    public GameObject TowerFrostPrefab;

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
        UIManager.Instance.MessageText.text = "Selected Slot: " + _selectedSlotIndex;
        Debug.Log("Selected Slot: " + _selectedSlotIndex);
    }

    public void BuildTower(string id)
    {
        if (_selectedSlotIndex < 0 || _selectedSlotIndex >= BuildSlots.Length)
        {
            UIManager.Instance.MessageText.text = "Pick A Slot First";
            Debug.Log("Pick A Slot First");
            return;
        }

        var slot = BuildSlots[_selectedSlotIndex];
        if (slot.childCount > 0)
        {
            UIManager.Instance.MessageText.text = "Slot Already Occupied";
            Debug.Log("Slot Already Occupied");
            return;
        }

        var cost = 999;
        GameObject prefab = null;

        if (id == "Gunner")
        {
            cost = 50;
            prefab = TowerGunnerPrefab;
        }
        else if (id == "Cannon")
        {
            cost = 80;
            prefab = TowerCannonPrefab;
        }
        else if (id == "Frost")
        {
            cost = 70;
            prefab = TowerFrostPrefab;
        }

        if (UglyDTGameManager.Instance.Gold < cost)
        {
            UIManager.Instance.MessageText.text = "Not Enough Gold.";
            Debug.Log("Not Enough Gold.");
            return;
        }

        UglyDTGameManager.Instance.Gold -= cost;
        UIManager.Instance.RefreshUI();

        var go = Instantiate(
            prefab,
            slot.position,
            Quaternion.identity,
            slot);

        var t = go.GetComponent<UglyTower>();
        if (t == null)
            t = go.AddComponent<UglyTower>();

        t.TowerId = id;
        t.Level = 1;

        Towers.Add(t);
    }
}