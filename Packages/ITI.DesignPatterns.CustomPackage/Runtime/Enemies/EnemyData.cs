using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
    public sealed class EnemyData : ScriptableObject
    {
        [field: SerializeField]
        public ImageIdsEnum EnemyImageId { get; private set; }

        [field: SerializeField]
        public EnemyType EnemyType { get; private set; }

        [field: SerializeField]
        public int Hp { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public int GoldReward { get; private set; }

        [field: SerializeField]
        public int DamageToBase { get; private set; }
    }
}