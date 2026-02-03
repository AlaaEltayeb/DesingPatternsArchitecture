using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    [CreateAssetMenu(fileName = "Turret", menuName = "Turrets/Turret", order = 1)]
    public class TurretData : ScriptableObject
    {
        [field: SerializeField]
        public TowerType TowerId { get; private set; }

        [field: SerializeField]
        public float Range { get; set; }

        [field: SerializeField]
        public float Rate { get; set; }

        [field: SerializeField]
        public int Damage { get; set; }

        [field: SerializeField]
        public int Cost { get; set; }

        [field: SerializeField]
        public GameObject TurretPrefab { get; private set; }
    }
}