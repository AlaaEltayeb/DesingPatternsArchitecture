using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Strategy
{
    public sealed class CannonAttackStrategy : AttackStrategyBase
    {
        public override void ExecuteStrategy()
        {
            base.ExecuteStrategy();
            ApplySplash();
        }

        private void ApplySplash()
        {
            Debug.Log("Splash Applied");
        }
    }
}