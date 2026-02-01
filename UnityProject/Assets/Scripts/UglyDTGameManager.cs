using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UglyDTGameManager : MonoBehaviour
{
    public static UglyDTGameManager Instance;

    public Transform[] Path;
    public Transform EnemyParent;
    public Transform TowerParent;
    public Transform BulletParent;

    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI LivesText;

    public TextMeshProUGUI MessageText;
    public Button StartWaveButton;

    public GameObject EnemyRunnerPrefab;
    public GameObject EnemyTankPrefab;
    public GameObject EnemyFlyerPrefab;

    public GameObject TowerGunnerPrefab;
    public GameObject TowerCannonPrefab;
    public GameObject TowerFrostPrefab;

    public GameObject BulletPrefab;

    public Button BuildGunnerButton;
    public Button BuildCannonButton;
    public Button BuildFrostButton;

    public Transform[] BuildSlots;

    public int Gold = 200;
    public int Lives = 20;
    public int Wave;
    public bool WaveInProgress;

    public List<UglyEnemy> Enemies = new();
    public List<UglyTower> Towers = new();

    private string[] Waves =
    {
        "RRRRRR",
        "RRRTTRR",
        "TTTRRRRR",
        "RFRFRFRF",
        "TTTFFRRRRR",
        "TTTTTT",
        "R",
    };

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    private void Start()
    {
        RefreshUI();

        if (StartWaveButton != null)
            StartWaveButton.onClick.AddListener(() => StartNextWave());

        if (BuildGunnerButton != null)
            BuildGunnerButton.onClick.AddListener(() => BuildTower("Gunner"));
        if (BuildCannonButton != null)
            BuildCannonButton.onClick.AddListener(() => BuildTower("Cannon"));
        if (BuildFrostButton != null)
            BuildFrostButton.onClick.AddListener(() => BuildTower("Frost"));
    }

    private void Update()
    {
        if (Lives <= 0)
        {
            Lives = 0;
            RefreshUI();
            if (WaveInProgress)
            {
                WaveInProgress = false;
                MessageText.text = "You Lose, (Reload Scene Manually)";
                Debug.Log("You Lose, (Reload Scene Manually)");
            }
        }

        for (var i = 0; i < Towers.Count; i++)
        {
            var t = Towers[i];
            if (t != null)
                t.UglyTick();
        }

        if (WaveInProgress)
        {
            var anyAlive = false;
            for (var i = 0; i < Enemies.Count; i++)
            {
                var e = Enemies[i];
                if (e != null && Enemies[i].Hp > 0)
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            {
                WaveInProgress = false;
                MessageText.text = "Wave Completed!";
                Debug.Log("Wave Completed!");
                RefreshUI();
            }
        }
    }

    public void StartNextWave()
    {
        if (WaveInProgress)
            return;

        Wave++;
        WaveInProgress = true;
        RefreshUI();

        var seq = Waves[Mathf.Min(Wave - 1, Waves.Length - 1)];

        if (Wave > Waves.Length)
            seq = "RRTTRFRRFT";

        CancelInvoke(nameof(SpawnFromSequence));
        _spawnSeq = seq;
        _spawmIndex = 0;
        InvokeRepeating(nameof(SpawnFromSequence), 0.25f, 0.6f);
    }

    private string _spawnSeq;
    private int _spawmIndex;

    private void SpawnFromSequence()
    {
        if (_spawmIndex >= _spawnSeq.Length)
        {
            CancelInvoke(nameof(SpawnFromSequence));
            return;
        }

        var c = _spawnSeq[_spawmIndex++];
        var prefab = EnemyRunnerPrefab;
        if (c == 'T')
            prefab = EnemyTankPrefab;
        else if (c == 'F')
            prefab = EnemyFlyerPrefab;

        var go = Instantiate(
            prefab,
            Path[0].position,
            Quaternion.identity,
            EnemyParent);

        var enemy = go.GetComponent<UglyEnemy>();
        if (enemy == null)
            enemy = go.AddComponent<UglyEnemy>();

        enemy.Type = c == 'T' ? "Tank" : c == 'F' ? "Flyer" : "Runner";
        enemy.Init(Path);

        Enemies.Add(enemy);
    }

    public void EnemyReachedBase(UglyEnemy enemy)
    {
        Lives -= enemy.DamageToBase;
        RefreshUI();

        Enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void EnemyKilled(UglyEnemy enemy)
    {
        Gold += enemy.GoldRewards;
        RefreshUI();
        Enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void RefreshUI()
    {
        if (GoldText != null)
            GoldText.text = $"Gold: {Gold}";
        if (WaveText != null)
            WaveText.text = "Wave: " + Wave + (WaveInProgress ? " (Running)" : "");
        if (LivesText != null)
            LivesText.text = $"Lives: {Lives}";
    }

    private int _selectedSlotIndex = -1;

    public void SelectSlot(int index)
    {
        _selectedSlotIndex = index;
        MessageText.text = "Selected Slot: " + _selectedSlotIndex;
        Debug.Log("Selected Slot: " + _selectedSlotIndex);
    }

    public void BuildTower(string id)
    {
        if (_selectedSlotIndex < 0 || _selectedSlotIndex >= BuildSlots.Length)
        {
            MessageText.text = "Pick A Slot First";
            Debug.Log("Pick A Slot First");
            return;
        }

        var slot = BuildSlots[_selectedSlotIndex];
        if (slot.childCount > 0)
        {
            MessageText.text = "Slot Already Occupied";
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

        if (Gold < cost)
        {
            MessageText.text = "Not Enough Gold.";
            Debug.Log("Not Enough Gold.");
            return;
        }

        Gold -= cost;
        RefreshUI();

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