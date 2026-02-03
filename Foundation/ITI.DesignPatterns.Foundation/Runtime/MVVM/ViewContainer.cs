using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public abstract class ViewContainer : ScriptableObject, IViewContainer
    {
        [field: SerializeField]
        protected GameObject[] Prefabs { get; set; }

        private IReadOnlyDictionary<Type, GameObject> _mapping;

        private void OnEnable()
        {
            CreateViewPrefabMapping();
        }

        private void CreateViewPrefabMapping()
        {
            var allPrefabs = Prefabs;

            if (allPrefabs == null)
                return;

            _mapping = allPrefabs
                .ToDictionary(prefab => prefab.GetComponent<IView>().GetType(), prefab => prefab);
        }

        public GameObject GetView<TView>()
        {
            var view = typeof(TView);

            if (!typeof(IView).IsAssignableFrom(view))
                throw new ArgumentException($"{view.Name} is not an {typeof(IView)}");

            if (_mapping == null)
                throw new InvalidOperationException();

            if (!_mapping.TryGetValue(view, out var prefab))
                throw new KeyNotFoundException($"No prefab found for {view.Name}");

            return prefab;
        }
    }
}