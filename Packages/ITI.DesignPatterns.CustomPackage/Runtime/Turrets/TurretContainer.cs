using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    [CreateAssetMenu(fileName = "TurretContainer", menuName = "Turrets/TurretContainer", order = 2)]
    public sealed class TurretContainer : ScriptableObject, ITurretContainer
    {
        [field: SerializeField]
        public List<TurretData> Turrets { get; private set; }
    }
}