using UnityEngine;

public class UglyBullet : MonoBehaviour
{
    public UglyEnemy Target;
    public int Damage;
    public bool IsSplash;

    private void Update()
    {
        if (Target == null || Target.Hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            Target.transform.position,
            Time.deltaTime * 10);

        if (Vector3.Distance(transform.position, Target.transform.position) < 0.1f)
        {
            if (!IsSplash)
            {
                Target.TakeDamage(Damage);
            }
            else
            {
                var list = EnemyManager.Instance.Enemies;
                for (var i = 0; i < list.Count; i++)
                {
                    var e = list[i];
                    if (e == null || e.Hp <= 0)
                        continue;

                    if (Vector3.Distance(transform.position, e.transform.position) < 1.2f)
                        e.TakeDamage(Damage);
                }
            }

            Destroy(gameObject);
        }
    }
}