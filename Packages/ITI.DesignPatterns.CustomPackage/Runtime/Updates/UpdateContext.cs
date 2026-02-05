using System;
using System.Collections.Generic;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Updates
{
    public sealed class UpdateContext : MonoBehaviour, IUpdateContext, IFixedUpdateContext, ILateUpdateContext
    {
        private readonly List<Action> _updateActions = new();
        private readonly List<Action> _fixedUpdateActions = new();
        private readonly List<Action> _lateUpdateActions = new();

        void IUpdateContext.Add(Action action)
        {
            _updateActions.Add(action);
        }

        void IUpdateContext.Remove(Action action)
        {
            _updateActions.Remove(action);
        }

        void IFixedUpdateContext.Add(Action action)
        {
            _fixedUpdateActions.Add(action);
        }

        void IFixedUpdateContext.Remove(Action action)
        {
            _fixedUpdateActions.Remove(action);
        }

        void ILateUpdateContext.Add(Action action)
        {
            _lateUpdateActions.Add(action);
        }

        void ILateUpdateContext.Remove(Action action)
        {
            _lateUpdateActions.Remove(action);
        }

        private void Update()
        {
            foreach (var action in _updateActions)
            {
                action();
            }
        }

        private void FixedUpdate()
        {
            foreach (var action in _fixedUpdateActions)
            {
                action();
            }
        }

        private void LateUpdate()
        {
            foreach (var action in _lateUpdateActions)
            {
                action();
            }
        }
    }
}