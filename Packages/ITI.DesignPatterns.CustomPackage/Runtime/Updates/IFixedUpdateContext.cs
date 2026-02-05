using System;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Updates
{
    public interface IFixedUpdateContext
    {
        void Add(Action action);
        void Remove(Action action);
    }
}