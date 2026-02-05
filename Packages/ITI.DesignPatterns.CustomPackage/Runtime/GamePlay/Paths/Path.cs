using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths
{
    public class Path : MonoBehaviour
    {
        [field: SerializeField]
        public List<Transform> EnemyPath { get; private set; }
    }
}