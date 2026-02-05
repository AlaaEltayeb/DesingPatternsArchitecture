using System;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Updates
{
    public interface ILateUpdateContext
    {
        void Add(Action action);
        void Remove(Action action);
    }
}