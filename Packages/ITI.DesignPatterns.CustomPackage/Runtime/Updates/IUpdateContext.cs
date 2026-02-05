using System;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Updates
{
    public interface IUpdateContext
    {
        void Add(Action action);
        void Remove(Action action);
    }
}