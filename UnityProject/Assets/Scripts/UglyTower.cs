using UnityEngine;
using VContainer;

public class UglyTower : MonoBehaviour
{
    [Inject]
    private IGameManager _gameManager;

    public TowerType TowerId;
    public int Level;

    private float _cooldown;

    public float Range;
    public float Rate;
    public int Damage;
    public int Cost;

    public GameObject BulletPrefab;

    protected virtual void Start()
    {
    }

    public void UglyTick()
    {
        _cooldown -= Time.deltaTime;

        if (_cooldown > 0)
            return;

        Damage = Mathf.RoundToInt(Damage * (1f + (Level - 1) * 0.5f));
        Range *= 1f + (Level - 1) * 0.1f;

        var target = FindTarget(Range);

        if (target == null)
            return;

        Shoot(target, Damage);
        _cooldown = Rate;
    }

    private UglyEnemy FindTarget(float range)
    {
        UglyEnemy closestEnemy = null;
        var closestDistance = float.MaxValue;

        var list = EnemyManager.Instance.Enemies;

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
        if (BulletPrefab == null || TowerId == TowerType.Frost)
        {
            return;
        }

        var bgo = Instantiate(
            BulletPrefab,
            transform.position,
            Quaternion.identity,
            _gameManager.BulletParent);

        var b = bgo.GetComponent<UglyBullet>();
        if (b == null)
            b = bgo.AddComponent<UglyBullet>();

        b.Target = target;
        b.Damage = dmg;
        b.IsSplash = TowerId == TowerType.Cannon;
    }
}