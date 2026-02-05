using ITI.DesignPatterns.CustomPackage.Runtime.Enemy;
using ITI.DesignPatterns.CustomPackage.Runtime.Enemy.EnemyStateMachine;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class UglyEnemy : MonoBehaviour
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        [Inject]
        private IEventSystem _eventSystem;

        private Dictionary<EnemyStateId, IEnemyStrategy> _strategies = new()
        {
            { EnemyStateId.Walking, new WalkingState() },
            { EnemyStateId.Frozen, new EnemyFrozenState() },
            { EnemyStateId.PushBack, new EnemyPushBackState() },
        };

        public string Type;
        public int Hp;
        public float Speed;
        public int GoldRewards;
        public int DamageToBase;

        private Transform[] _path;
        private int _pathIndex;

        private void Start()
        {
            _eventSystem.Subscribe<EnemyStateChanged>(OnEnemyStateChanged);
        }

        private void OnEnemyStateChanged(EnemyStateChanged evt)
        {
            var strategy = _strategies[evt.EnemyState];

            strategy.Execute();
        }

        public void Init(Transform[] path)
        {
            _path = path;
            _pathIndex = 0;

            if (Type == "Runner")
            {
                Hp = 25;
                Speed = 3.5f;
                GoldRewards = 5;
                DamageToBase = 1;
            }

            if (Type == "Tank")
            {
                Hp = 90;
                Speed = 1.4f;
                GoldRewards = 12;
                DamageToBase = 3;
            }

            if (Type == "Flyer")
            {
                Hp = 90;
                Speed = 1.4f;
                GoldRewards = 12;
                DamageToBase = 3;
            }
        }

        private void OnDestroy()
        {
            _eventSystem.Unsubscribe<EnemyStateChanged>(OnEnemyStateChanged);
        }

        private void Update()
        {
            if (Hp <= 0)
                return;

            if (_path == null || _path.Length == 0)
                return;

            var targetPoint = _path[_pathIndex].position;

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint) < 0.05f)
            {
                _pathIndex++;
                if (_pathIndex >= _path.Length)
                {
                    _eventSystem.Publish(new EnemyReachedBaseEvent(this));
                }
            }
        }

        public void TakeDamage(int dmg)
        {
            Hp -= dmg;

            if (Hp <= 0)
            {
                _eventSystem.Publish(new EnemyKilledEvent(this));
            }
        }

        public void ApplySlow(float factor, float time)
        {
            Speed *= factor;
            Invoke(nameof(UndoSlow), time);
        }

        private void UndoSlow()
        {
            Speed += 0.5f;
        }
    }
}