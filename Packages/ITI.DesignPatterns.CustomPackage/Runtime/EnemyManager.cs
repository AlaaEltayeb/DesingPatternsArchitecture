using ITI.DesignPatterns.Foundation.Runtime.Event;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class EnemyManager : MonoBehaviour, IEnemiesManager
    {
        [Inject]
        private IGameManager _gameManager;

        [Inject]
        private IUIManager _uiManager;

        [Inject]
        private IEventSystem _eventSystem;

        public Transform[] Path;

        public Transform EnemyParent;

        public GameObject EnemyRunnerPrefab;
        public GameObject EnemyTankPrefab;
        public GameObject EnemyFlyerPrefab;

        public List<UglyEnemy> Enemies { get; } = new();

        public string[] Waves { get; } =
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

        private void Start()
        {
            _eventSystem.Subscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            DestroyEnemy(evt.Enemy);
        }

        private void DestroyEnemy(UglyEnemy enemy)
        {
            Enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        private void OnEnemyReachedBase(EnemyReachedBaseEvent evt)
        {
            DestroyEnemy(evt.Enemy);
        }

        private void OnDestroy()
        {
            _eventSystem.Unsubscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
        }

        public void ResetWave(int wave)
        {
            var seq = Waves[Mathf.Min(wave - 1, Waves.Length - 1)];

            if (wave > Waves.Length)
                seq = "RRTTRFRRFT";

            _spawnSeq = seq;
            _spawnIndex = 0;
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
    }
}