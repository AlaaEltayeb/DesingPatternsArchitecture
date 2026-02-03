using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class UglyTower : MonoBehaviour
    {
        [Inject]
        private IGameManager _gameManager;

        [Inject]
        private IEnemiesManager _enemiesManager;

        private int _level = 1;
        private float _cooldown;

        public TurretData TurretData { get; set; }

        private void Update()
        {
            UglyTick();
        }

        public void UglyTick()
        {
            _cooldown -= Time.deltaTime;

            if (_cooldown > 0)
                return;

            TurretData.Damage = Mathf.RoundToInt(TurretData.Damage * (1f + (_level - 1) * 0.5f));
            TurretData.Range *= 1f + (_level - 1) * 0.1f;

            var target = FindTarget(TurretData.Range);

            if (target == null)
                return;

            Shoot(target, TurretData.Damage);
            _cooldown = TurretData.Rate;
        }

        private UglyEnemy FindTarget(float range)
        {
            UglyEnemy closestEnemy = null;
            var closestDistance = float.MaxValue;

            var list = _enemiesManager.Enemies;

            for (var i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e == null || e.Hp <= 0)
                    continue;

                var d = Vector3.Distance(transform.position, e.transform.position);

                if (d < range && d < closestDistance)
                {
                    closestEnemy = e;
                    closestDistance = d;
                }
            }

            return closestEnemy;
        }

        protected virtual void Shoot(UglyEnemy target, int dmg)
        {
            if (TurretData.TurretPrefab == null || TurretData.TowerId == TowerType.Frost)
            {
                return;
            }

            var bgo = Instantiate(
                TurretData.TurretPrefab,
                transform.position,
                Quaternion.identity,
                _gameManager.BulletParent);

            var b = bgo.GetComponent<UglyBullet>();
            if (b == null)
                b = bgo.AddComponent<UglyBullet>();

            b.Target = target;
            b.Damage = dmg;
            b.IsSplash = TurretData.TowerId == TowerType.Cannon;
        }
    }
}