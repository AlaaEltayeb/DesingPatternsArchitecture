using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths
{
    [CreateAssetMenu(fileName = "EnemyPath", menuName = "Path")]
    public class Path : ScriptableObject
    {
        [field: SerializeField]
        public List<Vector2> EnemyPath { get; private set; } = new();
    }
}