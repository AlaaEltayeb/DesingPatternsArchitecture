using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class UglyEnemy : MonoBehaviour
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        public string Type;
        public int Hp;
        public float Speed;
        public int GoldRewards;
        public int DamageToBase;

        private Transform[] _path;
        private int _pathIndex;

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
                    _enemiesManager.EnemyReachedBase(this);
                }
            }
        }

        public void TakeDamage(int dmg)
        {
            Hp -= dmg;

            if (Hp <= 0)
            {
                _enemiesManager.EnemyKilled(this);
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