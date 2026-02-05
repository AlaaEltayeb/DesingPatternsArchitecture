using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    [CreateAssetMenu(fileName = "EnemyContainer", menuName = "Enemies/EnemyContainer", order = 2)]
    public sealed class EnemyContainer : ScriptableObject, IEnemyContainer
    {
        [field: SerializeField]
        public List<EnemyData> Enemies { get; private set; }
    }
}