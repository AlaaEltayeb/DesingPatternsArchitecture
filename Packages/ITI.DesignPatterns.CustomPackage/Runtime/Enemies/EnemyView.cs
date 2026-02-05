using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _enemyImage;

        private Vector3 _targetPosition;

        protected Enemy Enemy;

        private void Start()
        {
            Enemy.Position.StartObserving(OnEnemyPositionChanged);
            Enemy.IsDeadOrReachedBase.StartObserving(OnEnemyDeadOrReachedBase);
            Enemy.EnemyImage.StartObserving(OnEnemyImageChanged);
        }

        private void OnEnemyImageChanged(Sprite newValue)
        {
            if (newValue == null)
                return;

            _enemyImage.sprite = newValue;
        }

        public void SetEnemy(Enemy enemy)
        {
            Enemy = enemy;
        }

        private void OnEnemyDeadOrReachedBase(bool newValue)
        {
            if (!newValue)
                return;

            Destroy(gameObject);
        }

        private void OnEnemyPositionChanged(Vector2 newValue)
        {
            _targetPosition = new Vector3(newValue.X, newValue.Y, 0);
            transform.position =
                Vector3.MoveTowards(transform.position, _targetPosition, Enemy.Speed * Time.deltaTime);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _targetPosition) < 0.05f)
            {
                Enemy.EnemyReachedDestination();
            }
        }

        private void OnDestroy()
        {
            Enemy.Position.StopObserving(OnEnemyPositionChanged);
            Enemy.Dispose();
        }
    }
}