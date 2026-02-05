using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Strategy
{
    public abstract class AttackStrategyBase : IAttackStrategy
    {
        public virtual void ExecuteStrategy()
        {
            Debug.Log("Strategy Executed");
        }
    }
}