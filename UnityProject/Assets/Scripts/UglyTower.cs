using UnityEngine;

public class UglyTower : MonoBehaviour
{
    public string TowerId;
    public int Level;

    private float _cooldown;

    public void UglyTick()
    {
        _cooldown -= Time.deltaTime;

        if (_cooldown > 0)
            return;

        var range = 3.5f;
        var rate = 0.4f;
        var dmg = 10;

        if (TowerId == "Gunner")
        {
            range = 4.0f;
            rate = 0.25f;
            dmg = 7;
        }

        if (TowerId == "Cannon")
        {
            range = 3.0f;
            rate = 0.9f;
            dmg = 22;
        }

        if (TowerId == "Frost")
        {
            range = 3.5f;
            rate = 0.6f;
            dmg = 6;
        }

        dmg = Mathf.RoundToInt(dmg * (1f + (Level - 1) * 0.5f));
        range *= 1f + (Level - 1) * 0.1f;

        var target = FindTarget(range);

        if (target == null)
            return;

        Shoot(target, dmg);
        _cooldown = rate;
    }

    private UglyEnemy FindTarget(float range)
    {
        UglyEnemy closestEnemy = null;
        var closestDistance = float.MaxValue;

        var list = UglyDTGameManager.Instance.Enemies;

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

    private void Shoot(UglyEnemy target, int dmg)
    {
        if (UglyDTGameManager.Instance.BulletPrefab == null || TowerId == "Frost")
        {
            target.TakeDamage(dmg);
            if (TowerId == "Frost")
                target.ApplySlow(0.7f, 1.5f);

            return;
        }

        var bgo = Instantiate(
            UglyDTGameManager.Instance.BulletPrefab,
            transform.position,
            Quaternion.identity,
            UglyDTGameManager.Instance.BulletParent);

        var b = bgo.GetComponent<UglyBullet>();
        if (b == null)
            b = bgo.AddComponent<UglyBullet>();

        b.Target = target;
        b.Damage = dmg;
        b.IsSplash = TowerId == "Cannon";
    }
}