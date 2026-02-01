using ITI.DesignPatterns.Foundation.Runtime.Event;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class TestMonoBehaviour : MonoBehaviour
    {
        //[Inject]
        private IEventSystem _eventSystem;

        [Inject]
        protected IEventSystem EventSystem { get; set; }

        //[Inject]
        private void Inject(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public TestMonoBehaviour(IEventSystem eventSystem)
        {
        }

        private void Start()
        {
            //EventSystem.PrintMe();
        }
    }
}