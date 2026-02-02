using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public Transform[] Path;

    public Transform EnemyParent;

    public GameObject EnemyRunnerPrefab;
    public GameObject EnemyTankPrefab;
    public GameObject EnemyFlyerPrefab;

    public List<UglyEnemy> Enemies = new();

    public string[] Waves =
    {
        "RRRRRR",
        "RRRTTRR",
        "TTTRRRRR",
        "RFRFRFRF",
        "TTTFFRRRRR",
        "TTTTTT",
        "R",
    };

    public string _spawnSeq;
    public int _spawnIndex;

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }

    public void SpawnFromSequence()
    {
        if (_spawnIndex >= _spawnSeq.Length)
        {
            CancelInvoke(nameof(SpawnFromSequence));
            return;
        }

        var c = _spawnSeq[_spawnIndex++];
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
        UglyDTGameManager.Instance.Lives -= enemy.DamageToBase;
        UIManager.Instance.RefreshUI();

        Instance.Enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void EnemyKilled(UglyEnemy enemy)
    {
        UglyDTGameManager.Instance.Gold += enemy.GoldRewards;
        UIManager.Instance.RefreshUI();
        Instance.Enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}