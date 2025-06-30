using System.Resources;
using _GettingStarted.Utilities;
using UnityEngine;

namespace _GettingStarted.Services {
    [DefaultExecutionOrder(-10)]
    public class ServiceInitializer : Singleton<ServiceInitializer> {
        [SerializeField] private Prefabs prefabs;

        private void Awake() {
            Init();
        }

        public void Init() {
            if (!Services.Has<Prefabs>()) Services.Add(prefabs);
        }
    }
}