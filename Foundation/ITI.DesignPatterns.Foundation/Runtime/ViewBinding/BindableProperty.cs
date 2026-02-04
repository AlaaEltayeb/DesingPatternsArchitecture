using System.Collections.Generic;

namespace ITI.DesignPatterns.Foundation.Runtime.ViewBinding
{
    public sealed class BindableProperty<TValue>
    {
        private TValue _value;
        private HashSet<BindablePropertyChanged<TValue>> _callbacks;

        public bool RaisePropertyChanged { get; set; } = true;

        public TValue Value
        {
            get => _value;
            set => SetValue(value);
        }

        public BindableProperty(TValue value = default)
        {
            Value = value;
            _callbacks = null;
        }

        public void StartObserving(BindablePropertyChanged<TValue> callback, bool invokeOnObserve = true)
        {
            _callbacks ??= new HashSet<BindablePropertyChanged<TValue>>();

            if (!_callbacks.Add(callback))
                return;

            if (!invokeOnObserve)
                return;

            callback(Value);
        }

        public void StopObserving(BindablePropertyChanged<TValue> callback)
        {
            if (_callbacks == null || !_callbacks.Contains(callback))
                return;

            _callbacks.Remove(callback);
        }

        public void ClearObserving()
        {
            if (_callbacks == null)
                return;

            _callbacks.Clear();
            _callbacks = null;
        }

        private void SetValue(TValue value)
        {
            if (RaisePropertyChanged && _callbacks is not null)
            {
                foreach (var callback in _callbacks)
                {
                    callback?.Invoke(value);
                }
            }

            _value = value;
        }
    }
}