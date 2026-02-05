using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Strategy
{
    public sealed class FrostAttackStrategy : AttackStrategyBase
    {
        public override void ExecuteStrategy()
        {
            base.ExecuteStrategy();
            ApplySlow();
        }

        private void ApplySlow()
        {
            Debug.Log("Slow Applied");
        }
    }
}